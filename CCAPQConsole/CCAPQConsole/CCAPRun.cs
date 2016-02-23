using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using CCAPModel;
using CCAPHelper;
using System.Threading;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Diagnostics;


namespace CCAPQConsole
{
    public partial class CCAPRun : Form
    {
        //{0}-Solution Path
        //{1} CCAP Analyzer
        //{2} User Id
        //{3} Password

        const string CCAPCMD = @" /C cd /d ""{0}"" & {1}sonar-runner.bat -X -Dsonar.login={2} -Dsonar.password={3} > ""{4}"" 2>&1";
        //const string CCAPCMD = @" /C cd /d ""{0}"" & {1}sonar-runner.bat -X -Dsonar.login={2} -Dsonar.password={3}";
        //const string LOCALANALYZER=@"C:\Cognizant\CCAP\CCAPLocalOnlyAnalyzer\bin\";
        //const string CCAPANALYZER = @"C:\Cognizant\CCAP\CCAPAnalyzer\bin\";

        const string ANALYSISSUCCESS = "ANALYSIS SUCCESSFUL";

        const int LABELLOCX=48;
        const int LABELLOCY=7;
        const int LABELWIDTH=35;
        const int LABELHEIGHT=13;

        const int COMBOLOCX = 357;
        const int COMBOLOCY = 7;
        const int COMBOWIDTH = 224;
        const int COMBOHEIGHT = 21;

        const int PBARLOCX=51;// 185;
        const int PBARLOCY=35;
        const int PBARWIDTH=377;
        const int PBARHEIGHT=23;

        const int PBOXLOCX = 446; //568;
        const int PBOXLOCY = 35;
        const int PBOXWIDTH = 26;
        const int PBOXHEIGHT = 26;
                
        const int FORMHEIGHT = 272;
        const int FORMNEWHEIGHT = 452;
        const int CONTROLGAPY=50;

        private const string EncrytKey = "CCAPQueue";

  
        public CCAPJobs oJobs { get; set; }
        public Boolean IsWorkerCompleted{ get; set; }
        public int MaxJobExecution { get; set; }
        public Dictionary<int,string> ProcessStack { get; set; }
        public string StartupFolder { get; set; }
        public CCAPQTray oTray { get; set; }

        Dictionary<string, string> oAnalyzer = new Dictionary<string, string>();
        Dictionary<int, BackgroundWorker> bwd = new Dictionary<int, BackgroundWorker>();
        private const int CP_NOCLOSE_BUTTON = 0x200;
        private FileSystemWatcher fsw;
        delegate void AddComboBoxValue(string cbxname, string logfile);

        public CCAPRun()
        {
            InitializeComponent();
        }

        
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        private void CCAPRun_Load(object sender, EventArgs e)
        {
            StartupFolder = Application.StartupPath;
            MaxJobExecution = 4;
            ProcessStack = new Dictionary<int, string>();
            oAnalyzer.Add("Local Analysis",ConfigurationManager.AppSettings["LOCALANALYZER"]);
            oAnalyzer.Add("Publish", ConfigurationManager.AppSettings["CCAPANALYZER"]);
            fsw = new FileSystemWatcher(string.Format(@"{0}\{1}\{2}\", StartupFolder, CCAPJobFolders.JOBS, CCAPJobFolders.LOG));
            fsw.Filter = "*.log";
            fsw.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            fsw.IncludeSubdirectories = true;
            fsw.Changed += new FileSystemEventHandler(fsw_Changed);
            fsw.Created += new FileSystemEventHandler(fsw_Changed);
            fsw.EnableRaisingEvents = true;
            this.Height = FORMNEWHEIGHT;
            comboBoxLocation.SelectedIndex = 0;
        }

        

     
     
     
        private void CreateNewControls(int processstatckid)
        {
            int i=processstatckid;
            try
            {
                    BackgroundWorker bw = new BackgroundWorker();
                    bw.WorkerReportsProgress = true;
                    bw.WorkerSupportsCancellation = true;
                    bw.DoWork += new DoWorkEventHandler(bw_DoWork);
                    bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                    bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
                    bwd.Add(i, bw);
                    bw.RunWorkerAsync(i);
                
            }
            catch(Exception)
            {
                throw;
            }
        }

        

        public void ProcessJob()
        {
            string[] JobinQueue;

            try
            {
                comboBoxLocation.SelectedIndex = 0; // Always local execution happens
                if (ProcessStack.Count>=4)
                {
                    return;
                }
                this.Show();
                JobinQueue = ConfigurationManager.AppSettings["QueueFiles"].Split(new char[]{';'}  , StringSplitOptions.RemoveEmptyEntries);
                listBoxInQueue.Items.Clear();
                
                foreach (string jf in JobinQueue)
                {
                    if (!CheckInProcessStack(jf))
                    {
                        int id = GetAvailableKey(); //check the empty stack
                        if (id > 0)
                        {
                            ProcessStack.Add(id, jf);
                            listBoxInProgress.Items.Add(jf);
                            CreateNewControls(id);
                        }
                        else
                        {
                            listBoxInQueue.Items.Add(jf);
                        }
                    }
                    else
                    {
                        listBoxInQueue.Items.Add(jf);
                    }
                }
               
            }
            catch (Exception )
            {

                throw;
            }
            
        }

        private bool CheckInProcessStack(string jobfile)
        {
            bool availability = true;
            int id = 0;
            try
            {
               id= ProcessStack.Where(v => v.Value == jobfile).Select(k => k.Key).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            if (id==0)
            {
                availability = false;
            }
            return availability;
        }

        private int GetAvailableKey()
        {
            int id = 0;
            try
            {
               for (int i = 1; i < MaxJobExecution; i++)
                {
                    if (!ProcessStack.ContainsKey(i))
                    {
                        id = i;
                        break;
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            return id;
        }

       
        private Boolean ExecuteJob(CCAPJob job,string AnalysisType,string logfile)
        {
            try
            {
                string drivecmd = string.Empty;
                if (job.SolutionPath.StartsWith(@"\\"))
                {
                    drivecmd = "pushd";
                }
                else
                {
                    drivecmd = "cd";
                }
                ClearPDBs(Path.GetDirectoryName(job.SolutionPath));
                string args = string.Format(CCAPCMD, Path.GetDirectoryName(job.SolutionPath),AnalysisType , job.UserId,string.Empty ,logfile);
                CCAPJobHelper.ExecuteProcess("cmd.exe", args, true);
                
                
                if (File.Exists(logfile))
                {
                    if (File.ReadAllText(logfile).Contains(ANALYSISSUCCESS))
                    {
                        return true;
                    }
                }
                return false;
                     
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void ClearPDBs(string solutionpath)
        {
            try
            {
                foreach (string pdbfile in Directory.GetFiles(solutionpath,"*.pdb", SearchOption.AllDirectories))
                {
                    File.Delete(pdbfile);
                }
            }
            catch (Exception)
            {
                
                //throw; ignore errors
            }
        }

        private void CreateConfig(CCAPJob job)
        {
            try
            {
                //create sonar-project.properties
                
                string Propfile = string.Empty;
                string solpath = Path.GetDirectoryName(job.SolutionPath);
                string projectversion=string.Format("{0}-{1}-CTS-{2}",DateTime.Today.ToString("yyyymmdd"),DateTime.Now.ToString("hh:mm"),job.UserId);
                if (job.Language.ToUpper()=="JAVA")
                {
                    Propfile = File.ReadAllText(string.Format(@"{0}\{1}", StartupFolder, "sonar-projectJava.properties"));
                    Propfile = string.Format(Propfile, job.ProjectKey, projectversion, job.ProjectName, job.JavaSource,job.JavaBinary.Replace(@"\","/"));
                    if (job.OptionalProperties.Trim().Length>0)
                    {
                        Propfile += job.OptionalProperties;    
                    }
                }
                else
                {
                    Propfile = File.ReadAllText(string.Format(@"{0}\{1}",StartupFolder,"sonar-project.properties"));
                    Propfile = string.Format(Propfile, job.ProjectKey, projectversion, job.ProjectName, job.Language, job.SolutionName, job.TestPattern);
                }
                File.WriteAllText(string.Format(@"{0}\sonar-project.properties", solpath), Propfile);

                //Create ccap.config file

                Propfile = File.ReadAllText(string.Format(@"{0}\{1}", StartupFolder, "ccap.config"));
                Propfile = string.Format(Propfile, job.ProjectKey, job.ProjectName );
                File.WriteAllText(string.Format(@"{0}\ccap.config", solpath), Propfile);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            CCAPJobs QueuedJobs = new CCAPJobs();
            CCAPJobs CurrentJobs = new CCAPJobs();
            CCAPJob  CurrentJob=new CCAPJob();

            QueuedJobs.Jobs = new List<CCAPJob>();
            

            int id = (int) e.Argument;
            string jobfile = ProcessStack[id];
            string logfile = string.Empty;
           
            int loopcount=0;
            try
            {
                BackgroundWorker bw = sender as BackgroundWorker;
                Boolean ExecStatus;

                
                CurrentJobs = CCAPJobHelper.RetrieveJob(jobfile);
                QueuedJobs.ExecutionDate = CurrentJobs.ExecutionDate;
                QueuedJobs.JobQLocation = CurrentJobs.JobQLocation;
                QueuedJobs.AnalysisType = CurrentJobs.AnalysisType;
                QueuedJobs.ProjectLang = CurrentJobs.ProjectLang;
                oTray.notifyIcon1.BalloonTipText = string.Format("Processing job {0}", jobfile);
                oTray.notifyIcon1.ShowBalloonTip(10);
                            
                foreach (CCAPJob job in CurrentJobs.Jobs)
                {
                    logfile = jobfile.Replace(CCAPJobFolders.INQUEUE, CCAPJobFolders.LOG).Replace(".xml", string.Format("_{0}_{1}{2}",job.id, job.Language, ".log")).Replace(";", string.Empty);

                    oTray.notifyIcon1.BalloonTipText = string.Format("Execution in progress for \n Solution Name : {0} \n Language : {1}",job.SolutionName,job.Language);
                    oTray.notifyIcon1.ShowBalloonTip(1000);

                    Application.DoEvents();
                    loopcount++;
                    if (bw.CancellationPending==true)
                    {
                        e.Cancel = true;
                        RemoveProcessedJobs(CurrentJobs, QueuedJobs);
                        SaveJobStatus(QueuedJobs,CCAPJobFolders.PROCESSED, jobfile);
                        SaveJobStatus(CurrentJobs,CCAPJobFolders.INQUEUE, jobfile);
                        return;
                    }
                    CurrentJob=job;
                    CreateConfig(job);
                    ExecStatus = ExecuteJob(job, oAnalyzer[CurrentJobs.AnalysisType], logfile);
                    if (ExecStatus)
                    {
                        job.Status = CCAPJobFolders.COMPLETED;
                    }
                    else
                    {
                        job.Status = CCAPJobFolders.FAILED;
                       
                    }
                    oTray.notifyIcon1.BalloonTipText = string.Format("Task {0} for \n Solution Name : {1} \n Language : {2}",job.Status, job.SolutionName, job.Language);
                    oTray.notifyIcon1.ShowBalloonTip(1000);
                    if (job.Status == CCAPJobFolders.FAILED)
                    {
                        Thread.Sleep(new TimeSpan(0, 0, 3));
                        if (File.Exists(logfile))
                        {
                            Process.Start("Notepad.exe", logfile);
                        }
                    }
                    QueuedJobs.Jobs.Add(job);
                    job.Log = logfile;
                    bw.ReportProgress((loopcount / CurrentJobs.Jobs.Count) * 100);
                    Application.DoEvents();
                }
                RemoveScheduledTask(QueuedJobs.JobQLocation, jobfile);
                RemoveProcessedJobs(CurrentJobs, QueuedJobs);
                SaveJobStatus(CurrentJobs, CCAPJobFolders.INQUEUE, jobfile);
                SaveJobStatus(QueuedJobs, CCAPJobFolders.PROCESSED ,jobfile);
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void RemoveScheduledTask(string joblocation, string jobfile)
        {
            try
            {
                CCAPJobHelper.RemoveScheduledTask(jobfile, joblocation, EncrytKey);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private CCAPJobs RemoveProcessedJobs(CCAPJobs CurrentJobs, CCAPJobs QueuedJobs)
        {
            try
            {
                CurrentJobs.Jobs.RemoveAll(x => QueuedJobs.Jobs.Contains(x));
            }
            catch (Exception)
            {
                
                throw;
            }
            return CurrentJobs;
        }

        private void SaveJobStatus(CCAPJobs JobCollection, string foldername, string jobfile)
        {
            if (JobCollection.Jobs.Count > 0)
            {

                CCAPJobHelper.SaveJob(JobCollection, jobfile.Replace(CCAPJobFolders.INQUEUE, foldername.ToString()));
            }
            else
            {
                File.Delete(jobfile.Replace(CCAPJobFolders.INQUEUE, foldername.ToString()));
            }
        }

     

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string jobfiles = ConfigurationManager.AppSettings["QueueFiles"];
            string curjob;
            int id = bwd.Where(v => v.Value == sender).Select(k => k.Key).FirstOrDefault();
            bwd.Remove(id);
            if (e.Cancelled==true)
            {

            }
            else
            {
                //curjob=ProcessStack.Where(k=> k.Key==id).Select(v=> v.Value).FirstOrDefault();

                curjob = ProcessStack[id];
                ProcessStack.Remove(id);
                listBoxInProgress.Items.Remove(curjob);
                CCAPJobHelper.WriteAppsettingToConfig("QueueFiles", jobfiles.Replace(curjob + ";", string.Empty));
            }
            if (bwd.Count==0)
            {
                if (jobfiles.Length > 1)
                {
                    ProcessJob();
                }
                else
                {
                    this.Hide();
                }
                
            }
            
        }

        void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                int id = bwd.Where(v => v.Value == sender).Select(k => k.Key).FirstOrDefault();
                Control pbar = this.Controls.Find(string.Format(@"progressBar{0}", id), true).FirstOrDefault();
                pbar.Text = (e.ProgressPercentage.ToString() + "%");
            }
            catch (Exception)
            {

                //ignore error
            }

        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CCAPRun_Shown(object sender, EventArgs e)
        {
            //CreateNewControls();
        }

        private void CCAPRun_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
            
        }

        void fsw_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                if (e.Name == string.Empty || this.Height == FORMHEIGHT)
                {
                    return;
                }
                if (e.ChangeType == WatcherChangeTypes.Changed || e.ChangeType== WatcherChangeTypes.Created)
                {
                    labelExecutedJobName.Text = string.Format("Job Name : {0}", GetJobNameFrmLogName(e.Name));
                    labelExecutedLogName.Text = string.Format("Log Name : {0} ", e.Name);
                    PopulateLogContent(e.FullPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private string GetJobNameFrmLogName(string p)
        {
            string jobname = string.Empty;
            try
            {
                int plen = p.Length;
                int loc = p.LastIndexOf("_");
                loc = p.LastIndexOf("_", plen-loc)-1;
                return p.Substring(1, plen - loc);
            }
            catch (Exception)
            {

                return jobname;
            }
            
        }

        private void PopulateLogContent(string p)
        {
            try
            {
                if (this.Height== FORMHEIGHT)
                {
                    this.Height = FORMNEWHEIGHT;
                }
                richTextBoxLog.Text = string.Empty;
                if (File.Exists(p))
                {
                    using (var fileStream = new FileStream(p, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (var textReader = new StreamReader(fileStream))
                    {
                        var content = textReader.ReadToEnd();
                        richTextBoxLog.Text = content;
                    }
                    if (comboBoxLocation.Text.Equals("Server"))
                    {
                        PopulateListBoxForServer();
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void comboBoxLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxLocation.Text.Equals("Local"))
            {
                StartupFolder = Application.StartupPath;
            }
            else if (comboBoxLocation.Text.Equals("Server"))
            {
                StartupFolder = ConfigurationManager.AppSettings["ServerPath"].ToString();
                PopulateListBoxForServer();
                
            }
            fsw.Path = string.Format(@"{0}\{1}\{2}\", StartupFolder,CCAPJobFolders.JOBS, CCAPJobFolders.LOG);
        }

        private void PopulateListBoxForServer()
        {
            string dirname = string.Empty;
            string extn = string.Empty;
            string[] inqueueFiles;
            string[] inProgFiles;
            listBoxInProgress.Items.Clear();
            listBoxInQueue.Items.Clear();
            try
            {
                dirname = string.Format(@"{0}\{1}\{2}", StartupFolder, CCAPJobFolders.JOBS, CCAPJobFolders.PROCESSED);
                extn = "*.xml";
                inqueueFiles=Directory.GetFiles(dirname,extn);
                listBoxInQueue.Items.AddRange(inqueueFiles);

                dirname = string.Format(@"{0}\{1}\{2}", StartupFolder, CCAPJobFolders.JOBS, CCAPJobFolders.INQUEUE);
                extn = "*.log";
                inProgFiles= Directory.GetFiles(dirname,extn);
                listBoxInProgress.Items.AddRange(inqueueFiles.Where(v => inProgFiles.Contains(v)).ToArray<string>());
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void listBoxInQueue_MouseMove(object sender, MouseEventArgs e)
        {
            showTooltip(sender,e);
        }

        private void listBoxInProgress_MouseMove(object sender, MouseEventArgs e)
        {
            showTooltip(sender,e);
        }

        private void showTooltip(object sender, MouseEventArgs e)
        {
            try
            {
                ListBox lb = (ListBox)sender;
                int index = lb.IndexFromPoint(e.Location);
                if (index >= 0 && index < lb.Items.Count)
                {
                    string toolTipString = lb.Items[index].ToString();
                    // check if tooltip text coincides with the current one,
                    // if so, do nothing 
                    if (toolTip1.GetToolTip(lb) != toolTipString)
                        toolTip1.SetToolTip(lb, toolTipString);
                }
                else
                    toolTip1.Hide(lb); 
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
