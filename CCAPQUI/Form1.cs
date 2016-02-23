using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using Microsoft.Win32.TaskScheduler;
using CCAPModel;
using CCAPHelper;

namespace CCAPQUI
{
    public partial class Form1 : Form
    {
        
        #region Fields
            private DataTable dt;
            private Boolean AddStatus;
            private Boolean NewJob;
            private string ExistingJobFileName;
            private string StartupFolder = string.Empty;
            private const string EncrytKey = "CCAPQueue";
            enum GridItemName
            {
                SolutionName
                ,SolutionPath
                ,ProjectName
                ,ProjectId
                ,ArtifactId
                , UserId
                , Password
                , ProjectKey
                
            }

        #endregion Fields

        #region Public Methods
            public Form1()
        {
            InitializeComponent();
                
        }
        #endregion Public Methods

        #region Private Methods

            private void Form1_Load(object sender, EventArgs e)
        {
            if (! SingleInstance.EnsureSingleInstance())
            {
                this.Close();
            }
            
            comboBoxLocation.SelectedIndex = 0;
            comboBoxLocation_SelectedIndexChanged(comboBoxLocation, new EventArgs());
            comboBoxAnalyis.SelectedIndex = 0; //Local Analysis
            comboBoxLang.SelectedIndex = 0;
            //comboBoxLang_SelectedIndexChanged(comboBoxLang, new EventArgs());
            NewJob = true;
            
            ScheduledDate.MinDate = DateTime.Now;
            LoadProjectDetails();

        }
        
            private void Add_Click(object sender, EventArgs e)
            {
                PanelVisible(true, false);
                AddStatus = true;
            }

            private void Save_Click(object sender, EventArgs e)
            {
                if(ValidateControls())
                {
                    AddToGrid();
                    PanelVisible(false, true);
                    ResetValues();
                }
     
            }

            
            private void Cancel_Click(object sender, EventArgs e)
            {
                PanelVisible(false, true);
                ResetValues();
            }

            private void Edit_Click(object sender, EventArgs e)
            {
                if (CCAPJobGridView.SelectedCells.Count > 0)
                {
                    AddStatus = false;
                    PanelVisible(true, false);
                    PopulateValues();
                    
                }
                else
                {
                    MessageBox.Show("No item selected to edit","Edit",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }

            private void Delete_Click(object sender, EventArgs e)
            {
                if (CCAPJobGridView.SelectedCells.Count > 0)
                {
                    if (MessageBox.Show("Do you want to really delete this row?", "Delete Message", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        //PanelVisible(true, false);
                        foreach (DataGridViewCell dvcell in CCAPJobGridView.SelectedCells)
                        {
                            CCAPJobGridView.CurrentCell = dvcell;

                            CCAPJobGridView.Rows.Remove(CCAPJobGridView.CurrentRow);
                        }
                        
                    }
                }
                else
                {
                    MessageBox.Show("No item selected to Delete","Delete",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }

            private void buttonFolder_Click(object sender, EventArgs e)
            {
                openFileDialog1.ShowDialog();      
            }

            private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
            {
                textBoxSolutionName.Text = openFileDialog1.FileName;
            }

            private void textBoxProjectName_TextChanged(object sender, EventArgs e)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = string.Format(@"project_name like '%{0}%'", textBoxProjectName.Text);
                gvProjectkey.DataSource = dv;
                gvProjectkey.BringToFront();
                gvProjectkey.Columns[0].Width = 150;
                //gvProjectkey.Columns[1].Width = 500;
                gvProjectkey.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                
            }

            private void textBoxProjectName_Enter(object sender, EventArgs e)
            {
                textBoxProjectName.SelectAll();
                gvProjectkey.Visible = true;
            }

            private void textBoxProjectName_Leave(object sender, EventArgs e)
            {
                gvProjectkey.Visible = false;
            }

            private void btnClose_Click(object sender, EventArgs e)
            {
                if (MessageBox.Show("Are you sure you want to close this form?", "Close", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.Close();
                }

            }

            private void exitToolStripMenuItem_Click(object sender, EventArgs e)
            {
                btnClose_Click(btnClose,new EventArgs());
            }

            private void loadToolStripMenuItem_Click(object sender, EventArgs e)
            {
                try
                {
                    CCAPJobs jobs = new CCAPJobs();
                    ManageQueue mq = new ManageQueue();
                    mq.StartupFolder = this.StartupFolder;
                    mq.ShowDialog();
                    if (!mq.CCAPJobFile.Equals(string.Empty))
                    {
                        jobs =CCAPJobHelper.RetrieveJob(mq.CCAPJobFile);
                        LoadFromJob(jobs);
                    }
                    ExistingJobFileName = mq.CCAPJobFile;
                    if ( mq.QueueName.Equals(CCAPJobFolders.INQUEUE))
                    {
                        NewJob = false;
                    }
                    else
                    {
                        NewJob = true;
                    }
                    
                   
                    
                    mq = null;
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }

            private void LoadFromJob(CCAPJobs jobs)
            {
                try
                {
                    CCAPJobGridView.Rows.Clear();
                    if (jobs.ExecutionDate.Ticks < DateTime.Now.Ticks)
                    {
                        ScheduledDate.Value = DateTime.Now;
                    }
                    else
                    {
                        ScheduledDate.Value = jobs.ExecutionDate;
                    }
                    comboBoxAnalyis.Text = jobs.AnalysisType;
                    comboBoxLocation.Text = jobs.JobQLocation;
                    comboBoxLang.Text = jobs.ProjectLang;
                    DataGridViewRow rowitems;
                    int rowindex = 0;
                    
                                        
                    foreach (CCAPJob job in jobs.Jobs)
                    {
                        rowindex = CCAPJobGridView.Rows.Add();
                        rowitems = CCAPJobGridView.Rows[rowindex];
                        //rowitems = (DataGridViewRow)CCAPJobGridView.Rows[rowindex].Clone();
                        rowitems.Cells["SolutionName"].Value = job.SolutionName;
                        rowitems.Cells["SolutionPath"].Value = job.SolutionPath;
                        rowitems.Cells["ProjectId"].Value = job.ProjectId;
                        rowitems.Cells["ArtifactId"].Value = job.ArtifactId;
                        rowitems.Cells["ProjectName"].Value = job.ProjectName;
                        rowitems.Cells["UserId"].Value = job.UserId;
                        rowitems.Cells["Password"].Value = string.Empty;//  CCAPJobHelper.DecryptText(job.Password, EncrytKey);
                        rowitems.Cells["ProjectKey"].Value = job.ProjectKey;
                        rowitems.Cells["JavaSource"].Value = job.JavaSource;
                        rowitems.Cells["JavaBinary"].Value = job.JavaBinary;
                        rowitems.Cells["OptionalProperties"].Value = job.OptionalProperties;
                        rowitems.Cells["Status"].Value=job.Status;
                        rowitems.Cells["Log"].Value = job.Log;
                        
                        //CCAPJobGridView.Rows.Add(rowitems);
                    }
                }
                catch (Exception)
                {
                    
                    throw;
                }
            }
            private void LoadToJob(string Jobfile)
            {
                try
                {
                    CCAPJobs jobs = new CCAPJobs();
                    jobs.Jobs = new List<CCAPJob>();
                    if (ScheduledDate.Value.Ticks < DateTime.Now.Ticks)
                    {
                        ScheduledDate.Value = DateTime.Now.AddMinutes(5);
                    }
                    jobs.ExecutionDate = ScheduledDate.Value;
                    jobs.AnalysisType = comboBoxAnalyis.Text;
                    jobs.JobQLocation = comboBoxLocation.Text;
                    jobs.ProjectLang = comboBoxLang.Text;
                    
                    CCAPJob job;
                    int idcount = 0;
                    foreach (DataGridViewRow row in CCAPJobGridView.Rows)
                    {
                        string pkey = string.Empty;
                        string pname = row.Cells["ProjectName"].Value.ToString();
                        idcount++;
                            
                        job = new CCAPJob();
                        job.id = idcount;
                        job.SolutionName = row.Cells["SolutionName"].Value.ToString();
                        job.SolutionPath = row.Cells["SolutionPath"].Value.ToString();
                        job.ProjectName = row.Cells["ProjectName"].Value.ToString();
                        job.ArtifactId = row.Cells["ArtifactId"].Value.ToString();
                        job.ProjectId = row.Cells["ProjectId"].Value.ToString();
                        job.UserId = row.Cells["UserId"].Value.ToString();
                        job.Password = string.Empty;// CCAPJobHelper.EncryptText(row.Cells[GridItemName.Password"].Value.ToString(), EncrytKey);
                        job.ProjectKey = row.Cells["ProjectKey"].Value.ToString();
                        job.Language = job.ProjectKey.Substring(job.ProjectKey.LastIndexOf('.') + 1);
                        job.TestPattern = string.Format("*{0}Test", job.Language.ToUpper());
                        job.Status = "Queued";

                        job.JavaBinary = row.Cells["JavaBinary"].Value.ToString();
                        job.JavaSource = row.Cells["JavaSource"].Value.ToString();
                        job.OptionalProperties = row.Cells["OptionalProperties"].Value.ToString();
                        
                        jobs.Jobs.Add(job);
                    }
                    CCAPJobHelper.SaveJob(jobs, Jobfile);
                    MessageBox.Show("CCAP job is created and queued sucessfully");
                }
                catch (Exception)
                {
                    
                    throw;
                }
            }

            private DataTable GetProjectLanguages( string projkey)
            {
                DataTable dtfiltered = new DataTable();
                try
                {
                    DataView dv = new DataView(dt);
                    string pkey=string.Empty;
                    dv.RowFilter = string.Format(@"project_kee like '%{0}%'", string.Format(@"{0}:{1}",txtProjectId.Text,txtArtifactId.Text));
                    dtfiltered = dv.ToTable(); ;
                }
                catch (Exception)
                {
                    
                    throw;
                }
                return dtfiltered;
            }


            private void LoadProjectDetails()
            {
                string constring = CCAPJobHelper.DecryptText(ConfigurationManager.ConnectionStrings["CCAPMetlife"].ConnectionString, EncrytKey);
                string sql = "SELECT distinct project_name,project_kee FROM [CCAPMetlife].[dbo].[project_details]";
                AutoCompleteStringCollection acoln = new AutoCompleteStringCollection();
                SqlConnection con = new SqlConnection(constring);
                con.Open();
                SqlCommand command = new SqlCommand(sql, con);
                dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dt);
                SqlDataReader datreader = command.ExecuteReader(CommandBehavior.CloseConnection);
                while (datreader.Read())
                {
                    acoln.Add(datreader.GetString(0).ToString());
                }
                textBoxProjectName.AutoCompleteMode = AutoCompleteMode.Append;
                textBoxProjectName.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBoxProjectName.AutoCompleteCustomSource = acoln;
            }

            private void AddToGrid()
            {
                try
                {
                    //string pname = textBoxProjectName.Text;
                    string pkey = txtProjectKey.Text;
                    foreach (DataRow drow in GetProjectLanguages(pkey).Rows)
                    {
                        pkey = drow["project_kee"].ToString();
                        //string[] rowitems = new string[8];
                        DataGridViewRow rowitems;
                        int rowindex = 0;
                        if (AddStatus)
                        {
                            rowindex = CCAPJobGridView.Rows.Add();
                            rowitems = (DataGridViewRow)CCAPJobGridView.Rows[rowindex];
                        }
                        else
                        {
                            rowitems = CCAPJobGridView.Rows.Cast<DataGridViewRow>().Where(k => k.Cells["ProjectKey"].Value.ToString().Equals(pkey)).FirstOrDefault();
                            if (rowitems == null)
                            {
                                rowindex = CCAPJobGridView.Rows.Add();
                                rowitems = (DataGridViewRow)CCAPJobGridView.Rows[rowindex];
                            }
                        }
                        if (comboBoxLang.Text == ".Net")
                        {
                            rowitems.Cells["SolutionName"].Value = Path.GetFileName(textBoxSolutionName.Text);
                            rowitems.Cells["SolutionPath"].Value = textBoxSolutionName.Text;
                            rowitems.Cells["JavaSource"].Value = string.Empty;
                            rowitems.Cells["JavaBinary"].Value = string.Empty;
                            rowitems.Cells["OptionalProperties"].Value = string.Empty;

                        }
                        else
                        {
                            rowitems.Cells["SolutionName"].Value = textBoxProjectName.Text;
                            rowitems.Cells["SolutionPath"].Value = textBoxSource.Text;
                            rowitems.Cells["JavaSource"].Value = GetJavaSource();
                            rowitems.Cells["JavaBinary"].Value = textBoxBinary.Text.Replace(GetRootFolder(textBoxSource.Text), ""); //GetJavaBinary();
                            rowitems.Cells["OptionalProperties"].Value = textBoxOptional.Text;
                        }
                        rowitems.Cells["ProjectId"].Value = txtProjectId.Text;
                        rowitems.Cells["ArtifactId"].Value = txtArtifactId.Text;
                        rowitems.Cells["ProjectName"].Value = textBoxProjectName.Text;
                        rowitems.Cells["UserId"].Value = textBoxUserId.Text;
                        rowitems.Cells["Password"].Value = string.Empty;//  textBoxPassword.Text;
                        rowitems.Cells["ProjectKey"].Value = pkey; //txtProjectKey.Text; // Hidden in the grid
                       


                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            private string GetJavaBinary()
            {
                if (textBoxSource.Text.Equals(textBoxBinary.Text))
                {
                    return string.Empty;   
                }
                else
                {
                    return textBoxBinary.Text.Replace(GetRootFolder(textBoxSource.Text), "");
                }
                
            }

            private string GetJavaSource()
            {
                string jsource=GetRootFolder(textBoxSource.Text);
                jsource=textBoxSource.Text.Replace(jsource,"");
                if (jsource.StartsWith(@"\"))
                {
                    jsource=jsource.Substring(1);
                }
                return jsource;
            }

            private string GetRootFolder(string Sourcedir)
            {
                int loc = Sourcedir.LastIndexOf(@"\");
                if (loc >0)
                {
                    return Sourcedir.Substring(0, loc);
                }
                return Sourcedir;
            }

            
           

            private bool ValidateControls()
            {
                bool noerrval = true;
                errorProvider1.Clear();
               
                if (comboBoxLang.Text == ".Net")
                {
                    if (textBoxSolutionName.Text.Trim().Equals(string.Empty))
                    {
                        errorProvider1.SetError(textBoxSolutionName, "Solution Name cannot be blank");
                        noerrval = false;
                    }
                }
                else
                {
                    if (textBoxSource.Text.Trim().Equals(string.Empty) ||  (Directory.Exists(textBoxSource.Text)== false))
                    {

                        errorProvider1.SetError(textBoxSource, "Source directory cannot be blank");
                        noerrval = false;
                    }
                    else if (textBoxBinary.Text.Trim().Equals(string.Empty) || (Directory.Exists(textBoxBinary.Text) == false))
                    {
                        errorProvider1.SetError(textBoxBinary, "Binary directory cannot be blank");
                        noerrval = false;
                    }
                }

                if (comboBoxLocation.Text.Trim().Equals(string.Empty))
                {
                    errorProvider1.SetError(comboBoxLocation, "Location cannot be blank");
                    noerrval = false;
                }
                if (comboBoxAnalyis.Text.Trim().Equals(string.Empty))
                {
                    errorProvider1.SetError(comboBoxAnalyis, "Analysis Type  cannot be blank");
                    noerrval = false;
                }
                if (comboBoxLang.Text.Trim().Equals(string.Empty))
                {
                    errorProvider1.SetError(comboBoxLang, "Language Type  cannot be blank");
                    noerrval = false;
                }

                if (textBoxProjectName.Text.Trim().Equals(string.Empty))
                {
                    errorProvider1.SetError(textBoxProjectName, "Project Name  cannot be blank");
                    noerrval = false;
                }
                if (textBoxProjectName.Text.Trim().Equals(string.Empty))
                {
                    errorProvider1.SetError(textBoxProjectName, "Project Id cannot be blank");
                    noerrval = false;
                }
                if (txtProjectId.Text.Trim().Equals(string.Empty))
                {
                    errorProvider1.SetError(txtProjectId, "Project Id cannot be blank");
                    noerrval = false;
                }
                if (txtArtifactId.Text.Trim().Equals(string.Empty))
                {
                    errorProvider1.SetError(txtArtifactId, "Artifact Id cannot be blank");
                    noerrval = false;
                }
                if (textBoxUserId.Text.Trim().Equals(string.Empty))
                {
                    errorProvider1.SetError(textBoxUserId, "User Id Name cannot be blank");
                    noerrval = false;
                }
                    
                if(noerrval)
                {
                    errorProvider1.Clear();
                }

                return noerrval;
            }

            private void ResetValues()
            {
                try
                {
                    textBoxSource.Text = string.Empty;
                    textBoxBinary.Text = string.Empty;
                    textBoxOptional.Text = string.Empty;
                    textBoxSolutionName.Text = string.Empty;
                    textBoxProjectName.Text = string.Empty;
                    txtProjectId.Text = string.Empty;
                    txtArtifactId.Text = string.Empty;
                    txtProjectKey.Text = string.Empty;
                    if (!checkBoxRetainCredentials.Checked)
                    {
                        textBoxUserId.Text = string.Empty;
                        textBoxPassword.Text = string.Empty;
                    }
                    DataGridViewColumn dg = new DataGridViewColumn();
                 
                    
                }
                catch (Exception)
                {

                    throw;
                }
            }

            private void PanelVisible(bool top, bool bottom)
            {
                panelTop.Enabled = top;
                panelBottom.Enabled = bottom;
                errorProvider1.Clear();
            }

            private void PopulateValues()
            {
                try
                {
                    textBoxSolutionName.Text = CCAPJobGridView.CurrentRow.Cells["SolutionPath"].Value.ToString();
                    textBoxProjectName.Text = CCAPJobGridView.CurrentRow.Cells["ProjectName"].Value.ToString();
                    txtProjectId.Text = CCAPJobGridView.CurrentRow.Cells["ProjectId"].Value.ToString();
                    txtArtifactId.Text = CCAPJobGridView.CurrentRow.Cells["ArtifactId"].Value.ToString();
                    textBoxUserId.Text = CCAPJobGridView.CurrentRow.Cells["UserId"].Value.ToString();
                    textBoxPassword.Text = string.Empty;// CCAPJobGridView.CurrentRow.Cells["Password"].Value.ToString();
                    txtProjectKey.Text = CCAPJobGridView.CurrentRow.Cells["ProjectKey"].Value.ToString();
                    textBoxSource.Text = CCAPJobGridView.CurrentRow.Cells["SolutionPath"].Value.ToString();
                    if (CCAPJobGridView.CurrentRow.Cells["JavaBinary"].Value.ToString().Trim().Equals(string.Empty))
                    {
                        textBoxBinary.Text = textBoxSource.Text;
                    }
                    else
                    {
                        textBoxBinary.Text = GetRootFolder(textBoxSource.Text) + CCAPJobGridView.CurrentRow.Cells["JavaBinary"].Value.ToString();
                    }
                    textBoxOptional.Text = CCAPJobGridView.CurrentRow.Cells["OptionalProperties"].Value.ToString();


                }
                catch (Exception)
                {

                    // Ignore this error 
                    //throw;
                }

            }

            private void GetProjectDetails()
            {
                string pkee;
                int loc;
                int ext;
                pkee = gvProjectkey.CurrentRow.Cells["project_kee"].Value.ToString();
                loc = pkee.IndexOf(':');
                ext = pkee.LastIndexOf('.');
                txtProjectId.Text = pkee.Substring(0, loc);
                txtArtifactId.Text = pkee.Substring(loc + 1, ext - 1 - loc);
                txtProjectKey.Text = pkee;
            }

            private void toolStripMenuItemNew_Click(object sender, EventArgs e)
            {
                try
                {
                    NewJob = true;
                    ScheduledDate.MinDate = DateTime.Today;
                    if (MessageBox.Show("This will clear any values available in the grid. Are you sure you want create a new job ?","New Job",MessageBoxButtons.YesNo)== DialogResult.Yes)
                    {
                        CCAPJobGridView.Rows.Clear();
                        ResetValues();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);                    
                }
            }

        #endregion Private Methods

            private void AddToQueue_Click(object sender, EventArgs e)
            {
                try
                {
                    if (CCAPJobGridView.Rows.Count > 0)
                    {
                        
                                               
                        string jobfile;
                        if (NewJob)
                        {
                            string timestamp = DateTime.Now.ToString().Replace('/', '_').Replace(":", "").Replace(" ", "");
                            jobfile = string.Format(@"{0}\{1}\CCAP_Queue_{2}_{3}.xml", StartupFolder, CCAPJobFolders.INQUEUE, Environment.UserName, timestamp);
                        }
                        else
                        {
                            jobfile = ExistingJobFileName;
                        }
                        LoadToJob(jobfile);
                        CreateTask(jobfile);
                        CCAPJobGridView.Rows.Clear();

                    }
                    else
                    {
                        MessageBox.Show("No records to save");
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }

            private void CreateTask(string jobfile)
            {
                try
                {
                    CCAPJobHelper.CreateScheduledTask(jobfile, ScheduledDate.Value, comboBoxLocation.Text, EncrytKey);
                }
                catch (Exception)
                {
                    
                    throw;
                }
            }

            private void comboBoxLocation_SelectedIndexChanged(object sender, EventArgs e)
            {
                try
                {

                    if (comboBoxLocation.Text.Equals("Local"))
                    {
                        StartupFolder = string.Format(@"{0}\{1}", Application.StartupPath, "Jobs");
                    }
                    else
                    {
                        StartupFolder = string.Format(@"{0}\{1}", ConfigurationManager.AppSettings["ServerPath"].ToString(), "Jobs");
                    }

                    //if (!Directory.Exists(StartupFolder))
                    //{
                    //    CreateJobsDirectory(StartupFolder);
                    CreateJobsDirectory(StartupFolder);
                    CreateJobsDirectory(string.Format(@"{0}\{1}", StartupFolder, CCAPJobFolders.INQUEUE));
                    CreateJobsDirectory(string.Format(@"{0}\{1}", StartupFolder, CCAPJobFolders.LOG));
                    CreateJobsDirectory(string.Format(@"{0}\{1}", StartupFolder, CCAPJobFolders.PROCESSED));
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }

            private void CreateJobsDirectory(string strpath)
            {
                try
                {
                    if (!Directory.Exists(strpath))
                    {
                        Directory.CreateDirectory(strpath);
                    }
                }
                catch (Exception)
                {

                    throw;
                }

            }

            private void Form1_Resize(object sender, EventArgs e)
            {
                if (this.WindowState == FormWindowState.Maximized || this.WindowState== FormWindowState.Normal)
                {
                    CheckSystemTrayCall();    
                }
            }

            private void CheckSystemTrayCall()
            {
                ConfigurationManager.RefreshSection("appSettings");
                if (ConfigurationManager.AppSettings["ManageQueue"].ToString().Equals("True"))
                {
                    CCAPJobHelper.WriteAppsettingToConfig("ManageQueue", "");
                    loadToolStripMenuItem_Click(this, new EventArgs());
                }
            }

            private void Form1_Shown(object sender, EventArgs e)
            {
                CheckSystemTrayCall();
                
            }

            private void CCAPJobGridView_CellClick(object sender, DataGridViewCellEventArgs e)
            {
                try
                {
                    string colname = CCAPJobGridView.Columns[e.ColumnIndex].Name;
                    string colvalue = CCAPJobGridView[e.ColumnIndex, e.RowIndex].Value.ToString();
                    string filename = colvalue;
                    if (colname == "Log")
                    {
                        if (File.Exists(filename))
                        {
                            CCAPJobHelper.ExecuteProcess("Notepad.exe", filename);
                        }
                    }
                }
                catch (Exception)
                {

                    //throw;
                }
            }

            private void gvProjectkey_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {
                textBoxProjectName.Text = gvProjectkey.CurrentRow.Cells["project_name"].Value.ToString();
                GetProjectDetails();
                textBoxUserId.Focus();
            }

          

            private void buttonBinary_Click(object sender, EventArgs e)
            {
                if (textBoxBinary.Text.Trim().Length > 0)
                {
                    folderBrowserDialog1.Reset();
                    folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
                    folderBrowserDialog1.SelectedPath = textBoxBinary.Text;
                }
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    textBoxBinary.Text = folderBrowserDialog1.SelectedPath;
                }
            }

            private void buttonSource_Click(object sender, EventArgs e)
            {
                if (textBoxSource.Text.Trim().Length>0)
                {
                    folderBrowserDialog1.Reset();
                    folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
                    folderBrowserDialog1.SelectedPath = textBoxSource.Text;
                   
                }
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    textBoxSource.Text = folderBrowserDialog1.SelectedPath;
                }
            }

            private void comboBoxLang_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (CCAPJobGridView.Rows.Count > 0)
                {
                    MessageBox.Show("This will clear any values available in the grid.", "New Job", MessageBoxButtons.OK);
                    CCAPJobGridView.Rows.Clear();
                }
                EnableDisableControls((comboBoxLang.Text == ".Net"));
                
            }

            private void EnableDisableControls(bool status)
            {
                labelSource.Visible = !status;
                labelBinary.Visible = !status;
                labelOptional.Visible = !status;
                labelSolutionName.Visible = status;

                textBoxSource.Visible = !status;
                textBoxBinary.Visible = !status;
                textBoxOptional.Visible = !status;
                buttonSource.Visible = !status;
                buttonBinary.Visible = !status;
                buttonFolder.Visible = status;
                textBoxSolutionName.Visible = status;

                CCAPJobGridView.Columns["SolutionName"].Visible = status;
                CCAPJobGridView.Columns["SolutionPath"].Visible = status;
                CCAPJobGridView.Columns["JavaSource"].Visible = !status;
                CCAPJobGridView.Columns["JavaBinary"].Visible = !status;
                CCAPJobGridView.Columns["OptionalProperties"].Visible = !status;

              
                
                
            }

          

           
           
    }
}
