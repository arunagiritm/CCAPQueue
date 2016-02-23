using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;

namespace CCAPQUI
{
    public partial class ManageQueue : Form
    {

        public string StartupFolder { get; set; }

        public string CCAPJobFile { get; set; }

        public string QueueName { get; set; }

        public ManageQueue()
        {
            InitializeComponent();
        }


        private void ManageQueue_Load(object sender, EventArgs e)
        {
            treeViewJobs.ExpandAll();
            QueueName = string.Empty;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                CCAPJobFile = string.Empty;
                if (MessageBox.Show("Are you sure you want to close this form?", "Close", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }


        
        private void treeViewJobs_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                if (e.Node.Text == "Jobs")
                {
                    return;
                }
                string folderloc = string.Format(@"{0}\{1}", StartupFolder, e.Node.Text);
                listBox1.Items.Clear();
                string[] dnames = Directory.GetFiles(folderloc,"*.xml");

                foreach (string dname in dnames)
                {
                    listBox1.Items.Add(Path.GetFileName(dname));
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedItems.Count >1)
                {
                    MessageBox.Show("Cannot load jobs for multiple selected items");
                    return;
                }
                if (listBox1.SelectedIndex >=0)
                {
                    QueueName = treeViewJobs.SelectedNode.Text;
                    CCAPJobFile =string.Format(@"{0}\{1}\{2}",StartupFolder,treeViewJobs.SelectedNode.Text,listBox1.SelectedItem.ToString());
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No Job file is available / selected", "", MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (listBox1.SelectedIndex >= 0)
                {
                    if (MessageBox.Show("Are you sure you want to delete this job", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        for(int i=0;i<listBox1.SelectedItems.Count;i++)
                        {
                            CCAPJobFile = string.Format(@"{0}\{1}\{2}", StartupFolder, treeViewJobs.SelectedNode.Text, listBox1.SelectedItems[i].ToString());
                            File.Delete(CCAPJobFile);
                        }
                        TreeViewEventArgs enode = new TreeViewEventArgs(treeViewJobs.SelectedNode);
                        treeViewJobs_AfterSelect(treeViewJobs, enode);
                    }
                }
                else
                {
                    MessageBox.Show("No Job file is available / selected", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            btnLoad_Click(btnLoad, new EventArgs());
        }

       

       
       
      
    }
}
