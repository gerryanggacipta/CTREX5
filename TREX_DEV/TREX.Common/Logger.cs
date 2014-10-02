using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TREX.Common
{
    public class Logger
    {
        private static string LOG_PATH;

        public static void Out(string message) {

            if (LOG_PATH == null)
            {
                string dateTime = DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year;
                var logPathSetting = System.Configuration.ConfigurationManager.AppSettings["LOG_PATH"];
                LOG_PATH = string.Format(@"{0}{1}.txt", logPathSetting, dateTime);


                if (!System.IO.Directory.Exists(logPathSetting))
                    System.IO.Directory.CreateDirectory(logPathSetting);

            }
                System.IO.File.AppendAllText(LOG_PATH, DateTime.Now + "\t: " + message + Environment.NewLine);
        }

    }
}
