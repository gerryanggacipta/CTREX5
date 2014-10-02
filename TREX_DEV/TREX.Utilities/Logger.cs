using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TREX.Utilities
{
    public class Logger
    {
        private static string LOG_PATH;

        public static void Out(string message) {
          
            if (LOG_PATH == null)
            {
                
                LOG_PATH = System.Configuration.ConfigurationSettings.AppSettings["LOG_PATH"];

                if (!System.IO.Directory.Exists(LOG_PATH))
                    System.IO.Directory.CreateDirectory(LOG_PATH);

                string dateTime = DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year;
                LOG_PATH = string.Format("{0}{1}.txt", LOG_PATH ,dateTime);
            }

                System.IO.File.AppendAllText(LOG_PATH, DateTime.Now + "\t: " + message + Environment.NewLine);
        }

    }
}
