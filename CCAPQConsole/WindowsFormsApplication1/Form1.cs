using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CCAPHelper;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //if (!SingleInstance.EnsureSingleInstance())
            //{
            //    this.Close();
            //}
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            string handle = Process.GetCurrentProcess().MainWindowHandle.ToString(); ;
            CCAPHelper.CCAPJobHelper.WriteAppsettingToConfig("windowhandle", handle);
            MessageBox.Show("Shown event fired");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            MessageBox.Show(WindowState.ToString());
            
        }
    }
}
