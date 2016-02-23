using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CCAPHelper;
using System.Configuration;
using System.IO;
using CCAPModel;


namespace CCAPQConsole
{
    public partial class CCAPQTray : Form
    {
        CCAPRun occapRun;
        public CCAPQTray()
        {
            InitializeComponent();
        }

        private void CCAPQTray_Shown(object sender, EventArgs e)
        {
            notifyIcon1.ShowBalloonTip(10);
            CheckQueueFiles();
            this.Hide();
        }

        public void InvokeShownEvent()
        {
            CCAPQTray_Shown(this, new EventArgs() );
        }
        private void CheckQueueFiles()
        {
            try
            {
                GetStatus();
                ConfigurationManager.RefreshSection("appSettings");
                string jobfile =ConfigurationManager.AppSettings["QueueFiles"];
                if (!jobfile.Equals(string.Empty))
                {
                    occapRun.Show();
                    occapRun.ProcessJob();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void CCAPQTray_Load(object sender, EventArgs e)
        {
            occapRun = new CCAPRun();
            GetStatus();
            notifyIcon1.ShowBalloonTip(100);
            occapRun.oTray = (CCAPQTray)this;
            occapRun.Show();
            occapRun.Hide();
            Application.DoEvents();
        }

        private void GetStatus()
        {
            string StartupFolder = string.Empty;
            string completed = "Completed", inprogress = "In Progress", inqueue = "In Queue";

            try
            {

                if (localQueueToolStripMenuItem.Text == "Server Queue")
                {
                    StartupFolder = string.Format(@"{0}\{1}", ConfigurationManager.AppSettings["ServerPath"].ToString(), "Jobs");
                }
                else
                {
                    StartupFolder = string.Format(@"{0}\{1}", Application.StartupPath, "Jobs");
                }

                CompletedMenuItem.Text = string.Format("{0} : {1}", completed, Directory.GetFiles(string.Format(@"{0}\{1}", StartupFolder, CCAPJobFolders.PROCESSED), "*.xml").Length);
                InProgressMenuitem.Text = string.Format("{0} : {1}", inprogress, occapRun.ProcessStack.Count);
                toolStripMenuItemInQueue.Text = string.Format("{0} : {1}", inqueue, Directory.GetFiles(string.Format(@"{0}\{1}", StartupFolder, CCAPJobFolders.INQUEUE), "*.xml").Length);
            }
            catch (Exception)
            {
                CompletedMenuItem.Text = completed;
                InProgressMenuitem.Text = inprogress;
                toolStripMenuItemInQueue.Text = inqueue;
                //throw; can ignore
            }
        }

        private void localQueueToolStripMenuItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetStatus();
        }

        private void InProgressMenuitem_Click(object sender, EventArgs e)
        {
            try
            {
                if (occapRun != null)
                {
                    occapRun.Show();
                }
                else
                {
                    MessageBox.Show("No. Job is currently running");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void CompletedMenuItem_Click(object sender, EventArgs e)
        {
            InvokeUI();
        }

        private void toolStripMenuItemInQueue_Click(object sender, EventArgs e)
        {
            InvokeUI();
        }

        
        private void InvokeUI()
        {
            string arg;
            arg = string.Format(@"{0}", "Tray");
            CCAPJobHelper.ExecuteProcess(string.Format(@"{0}\CCAPQUI.exe", Application.StartupPath), arg);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int pcount = occapRun.ProcessStack.Count;
            

            if (pcount > 0)
            {
                MessageBox.Show(string.Format("There are {0} Job(s) currently being executed",pcount));
                if (MessageBox.Show("Are you sure you want to close this application ? ", "Close", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    occapRun.Show();
                    return;
                }
            }
            else if (MessageBox.Show("Are you sure you want to close this application ? ", "Close", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
                
            }
            
            this.Close();

        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            GetStatus();
        }

        

     

       
    }
}
