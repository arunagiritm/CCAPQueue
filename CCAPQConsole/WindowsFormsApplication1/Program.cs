using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;

namespace WindowsFormsApplication1
{
    public class Program : WindowsFormsApplicationBase
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            var prg = new Program();
            prg.EnableVisualStyles = true;
            prg.IsSingleInstance = true;
            prg.MainForm = new Form1();
            prg.Run(args);

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
        protected override void OnStartupNextInstance(StartupNextInstanceEventArgs eventArgs)
        {
            var main = this.MainForm as Form1;
            main.WindowState = FormWindowState.Normal;
            main.BringToFront();
            
            
        }
    }
}
