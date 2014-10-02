using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TREX.Utilities
{
    public static class DateTimeManager
    {
        /**
         * Input format: yyyyMMddHHmmss (Note: 24-h format)
         * 
         * Output format: array(yyyy, MM, dd, HH, mm, ss)
         */ 
        public static string[] factorizeDateTime(string dateTime)
        {
            return new string[6] {
                dateTime.Substring(0, 4),
                dateTime.Substring(4, 2),
                dateTime.Substring(6, 2),
                dateTime.Substring(8, 2),
                dateTime.Substring(10, 2),
                dateTime.Substring(12, 2)
            };
        }

        /**
         * Input format: yyyyMMdd
         * 
         * Output format: array(yyyy, MM, dd)
         */ 
        public static string[] factorizeDate(string date)
        {
            return new string[3] {
                date.Substring(0, 4),
                date.Substring(4, 2),
                date.Substring(6, 2)                
            };
        }


        /**
         * Input format yyyymmdd
         */
        public static DateTime getDateTimeObj(string dateTime)
        {
            return new DateTime(int.Parse(dateTime.Substring(0, 4)),
                                int.Parse(dateTime.Substring(4, 2)),
                                int.Parse(dateTime.Substring(6, 2)));
        }

        /**
         * Output format: yyyyMMddHHmmss
         */
        public static string getDateTimeNowToString()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        /**
        * Output format: dd MMM yyyy, HH:mm:ss SGT
        */
        public static string getDateTimeNowToSGT()
        {
            return DateTime.Now.ToString("dd MMM yyyy, HH:mm:ss") + " SGT";
        }
    }
}
