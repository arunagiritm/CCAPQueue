using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using CCAPHelper;
using Microsoft.VisualBasic.ApplicationServices;

namespace CCAPQConsole
{
    public class Program : WindowsFormsApplicationBase
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
       
            ProcessCommandLineArgs(args);
            var prg = new Program();
            prg.EnableVisualStyles = true;
            prg.IsSingleInstance = true;
            prg.MainForm = new CCAPQTray();
            prg.Run(args);  
        }

        protected override void OnStartupNextInstance(StartupNextInstanceEventArgs eventArgs)
        {
            //string[] args = Environment.GetCommandLineArgs();
            string[] args = eventArgs.CommandLine.ToArray<string>();
            ProcessCommandLineArgs(args);
            var main = this.MainForm as CCAPQTray;
            main.WindowState = FormWindowState.Normal;
            main.BringToFront();
            main.InvokeShownEvent();
        }

        protected static void ProcessCommandLineArgs(string[] args)
        {
            if (args.Length > 0)
            {
                if (!args[0].Equals(string.Empty))
                {
                    string qf = ConfigurationManager.AppSettings["QueueFiles"];
                    CCAPHelper.CCAPJobHelper.WriteAppsettingToConfig("QueueFiles", string.Format("{0}{1};", qf, args[0]));
                }
            }
        }

        
    }
}
