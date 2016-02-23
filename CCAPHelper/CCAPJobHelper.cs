using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using CCAPModel;
using System.Configuration;
using System.Security.Cryptography;
using Microsoft.Win32.TaskScheduler;
using System.Windows.Forms;

namespace CCAPHelper
{
    public static class CCAPJobHelper
    {

        //const String ExeConfiguration = "CCAPQUI.exe.config";
        public static Boolean SaveJob(CCAPJobs jobs, string filename)
        {
            try
            {
                
                XmlSerializer xs = new XmlSerializer(jobs.GetType());
                using (TextWriter writer = new StreamWriter(filename))
                {
                    xs.Serialize(writer, jobs);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            return true;
        }

        public static CCAPJobs RetrieveJob(string filename)
        {
            CCAPJobs jobs  = new CCAPJobs();
            try
            {
                XmlSerializer xs = new XmlSerializer(jobs.GetType());
                using (TextReader reader = new StreamReader(filename))
                {
                    jobs = (CCAPJobs)xs.Deserialize(reader);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            return jobs;
        }

        public static string EncryptText(string input, string password)
        {
            // Get the bytes of the string
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            // Hash the password with SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            
            byte[] bytesEncrypted = AESEncryption.AES_Encrypt(bytesToBeEncrypted, passwordBytes);

            string result = Convert.ToBase64String(bytesEncrypted);

            return result;
        }

        public static string DecryptText(string input, string password)
        {
            // Get the bytes of the string
            byte[] bytesToBeDecrypted = Convert.FromBase64String(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesDecrypted = AESEncryption.AES_Decrypt(bytesToBeDecrypted, passwordBytes);

            string result = Encoding.UTF8.GetString(bytesDecrypted);

            return result;
        }

        public static void WriteAppsettingToConfig(string key,string value)
        {
            try
            {
                Configuration conf= ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                conf.AppSettings.Settings[key].Value = value;
                conf.Save(ConfigurationSaveMode.Modified, true);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public static string GetAppsettingFromConfig(string key)
        {
            try
            {
                Configuration conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                return conf.AppSettings.Settings[key].Value;
              
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int ExecuteProcess(string filename, string arguments)
        {
           return ExecuteProcess(filename, arguments, false);
        }
        
        public static int ExecuteProcess(string filename, string arguments,bool waitforexit)
        {
            try
            {
                System.Diagnostics.Process oProcess = new System.Diagnostics.Process();
                string outputContent=string.Empty;
                oProcess = new System.Diagnostics.Process();
                oProcess.StartInfo.UseShellExecute = false;
                oProcess.StartInfo.FileName = filename;
                oProcess.StartInfo.Arguments = arguments;
                oProcess.StartInfo.CreateNoWindow = true;
                oProcess.Start();
                oProcess.WaitForExit();
                return oProcess.ExitCode;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void CreateScheduledTask(string filename,DateTime scheduleddate, string location,string encryptkey)
        {
            try
            {
                Configuration conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
		        TaskService ts;
                string TrayApp = string.Format(@"{0}\{1}",Application.StartupPath, conf.AppSettings.Settings["CCAPTrayApp"].Value);
                if (!location.Equals("Local"))
                {
                    string servername = CCAPJobHelper.DecryptText(conf.AppSettings.Settings["ServerName"].Value, encryptkey);
                    string userid = CCAPJobHelper.DecryptText(conf.AppSettings.Settings["ServerUserid"].Value, encryptkey);
                    string pwd = CCAPJobHelper.DecryptText(conf.AppSettings.Settings["ServerPwd"].Value, encryptkey);

                    ts = new TaskService(servername, userid, "cts", pwd, false);
                }
                else
                {
                    ts = new TaskService();
                }
                TaskDefinition td = ts.NewTask();
                
                td.RegistrationInfo.Description = "CCAP Execution Task";
                td.Principal.LogonType = TaskLogonType.InteractiveToken;
                Trigger tgr = new TimeTrigger();
                tgr.StartBoundary = scheduleddate;
                td.Triggers.Add(tgr);
                td.Actions.Add(TrayApp,string.Format(@"""{0}""", filename), null);
                ts.RootFolder.RegisterTaskDefinition(Path.GetFileNameWithoutExtension(filename) , td);
                ts = null;
            }
	        catch (Exception)
            {
        		
		        throw;
	        }
        }

        public static void RemoveScheduledTask(string filename, string joblocation,string encryptkey)
        {
            try
            {
                filename = Path.GetFileNameWithoutExtension(filename);
                TaskService ts=null;
                Configuration conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (joblocation == "Local")
                {
                    ts= new TaskService();
                }
                else if (joblocation == "Publish")
                {
                    string servername = CCAPJobHelper.DecryptText(conf.AppSettings.Settings["ServerName"].Value, encryptkey);
                    string userid = CCAPJobHelper.DecryptText(conf.AppSettings.Settings["ServerUserid"].Value, encryptkey);
                    string pwd = CCAPJobHelper.DecryptText(conf.AppSettings.Settings["ServerPwd"].Value, encryptkey);

                    ts = new TaskService(servername, userid, "cts", pwd, false);
                    //ts.FindTask(filename, true).Dispose();
                    
                }
                if (ts!=null)
                {
                    ts.RootFolder.DeleteTask(filename, false);
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }
       
    }
}
