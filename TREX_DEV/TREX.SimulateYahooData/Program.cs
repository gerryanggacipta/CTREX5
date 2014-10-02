using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREX.Common;
using TREX.Utilities;
using System.Net;
using System.IO;
using System.Threading;


namespace TREX.SimulateYahooData
{
    class Program
    {
        static int SLEEP_INTERVAL = 2000;   // seconds
        static string rootFolder = @"C:\SimulateYahooData";
        static string prefixURL = @"http://finance.yahoo.com/d/quotes.csv?s={0}&f={1}";
        static string url = string.Format(prefixURL, string.Join(",",CompanyListManager.SymbolList.ToArray()), YahooConstants.RequestPropertyString);
        static long currentFileCount = 1;

        static void Main(string[] args)
        {
            // Create root directory   
            if (!Directory.Exists(rootFolder))
            {
                Directory.CreateDirectory(rootFolder);
                Console.WriteLine(">> Folder " + rootFolder + " created.");
            }

            // Create current directory                          
            string currentFolder = rootFolder + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss");
            Directory.CreateDirectory(currentFolder);              
            Console.WriteLine(">> Folder " + currentFolder + " created.");

            string csvData;
            while (true)
            {
                csvData = "";

                using (WebClient webClient = new WebClient())
                {
                    Console.Write("[" + currentFileCount + "] Start downloading.. ");

                    // Get all csv data
                    csvData = webClient.DownloadString(url);
                    Console.Write("Data received.. ");

                    // Create file
                    string currentFilename = currentFolder + "\\" + currentFileCount + ".csv";
                    TextWriter tw = File.CreateText(currentFilename);
                    tw.WriteLine(csvData);
                    Console.WriteLine("File created & recorded");
                    tw.Close();

                }

                currentFileCount += 1;

                Thread.Sleep(SLEEP_INTERVAL);
            }
        }

    }
}
