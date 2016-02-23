using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace CCAPHelper
{
    public class SingleInstance
    {
        public static bool EnsureSingleInstance()
        {
            Process currentProcess = Process.GetCurrentProcess();

            var runningProcess = (from process in Process.GetProcesses()
                                  where
                                    process.Id != currentProcess.Id &&
                                    process.ProcessName.Equals(
                                      currentProcess.ProcessName,
                                      StringComparison.Ordinal)
                                  select process).FirstOrDefault();

            if (runningProcess != null)
            {
                IntPtr handle=runningProcess.MainWindowHandle;
                //if (handle == IntPtr.Zero)
                //{
                //   handle= (IntPtr) Int32.Parse(CCAPJobHelper.GetAppsettingFromConfig("windowhandle"));             
                //}
                ShowWindow(handle, SW_RESTORE);
                SetForegroundWindow(runningProcess.MainWindowHandle);
                return false;
            }

            return true;
        }

        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern Boolean ShowWindow(IntPtr hWnd, Int32 nCmdShow);

        private const int SW_SHOWMAXIMIZED = 3;
        private const int SW_RESTORE = 9;
        private const int SHOW = 5;
        private const int SW_FORCEMINIMIZE = 11;
    }
}
