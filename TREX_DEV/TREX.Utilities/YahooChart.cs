using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using TREX.Common;

namespace TREX.Utilities
{
    public static class YahooChart
    {
        private static readonly string _urlPrefix = @"http://chart.finance.yahoo.com/z?";

        public enum ChartTimeSpan
        {
            c1Day_1d,
            c5Days_5d,
            c3Months_3m,
            c6Months_6m,
            c1Year_1y,
            c2Years_2y,
            c5Years_5y,
            cMaximum_my
        }

        public enum ChartType
        {
            Line_l,
            Bar_b,
            Candle_c
        }

        public enum ChartImageSize
        {
            Small_s,
            Middle_m,
            Large_l
        }

        public enum MovingAverageIndicator
        {
            m0_0,
            m5_5,
            m10_10,
            m20_20,
            m50_50,
            m100_100,
            m200_200
        }


        #region Static method to return an image for Yahoo charts

        /**
         * Configurable attributes:
         *      s: stock symbol
         *      t: timespan
         *      q: chart type
         *      z: size
         *      p: moving average indicator
         * 
         */ 
        public static byte[] getStockChart(string stock, 
                                          ChartTimeSpan timespan = ChartTimeSpan.c1Day_1d, 
                                          ChartType type = ChartType.Candle_c,  
                                          List<MovingAverageIndicator> mvIndicators = null, 
                                          ChartImageSize size = ChartImageSize.Large_l,
                                          bool showVol = false)
        {
            StringBuilder strBuilder = new StringBuilder();

            strBuilder.Append(_urlPrefix);

            // Stock symbol
            strBuilder.Append("s=" + stock);

            // Timespan
            string t = timespan.ToString().Substring(timespan.ToString().IndexOf("_") + 1);
            strBuilder.Append("&t=" + t);

            // Chart type
            string q = type.ToString().Substring(type.ToString().IndexOf("_") + 1);
            strBuilder.Append("&q=" + q);

            // Size
            string z = size.ToString().Substring(size.ToString().IndexOf("_") + 1);
            strBuilder.Append("&z=" + z);

            // Moving average
            if (mvIndicators != null || mvIndicators.Count() != 0)
            {
                string p = string.Join(",", mvIndicators.ToArray());
                strBuilder.Append("&p=" + p);
            }

            // Vol graph for Strategy view
            if (showVol)
            {
                strBuilder.Append("&a=v,p12");
            }

            // Finally, yayyy
            string url = strBuilder.ToString();

            try
            {
                return getImageFromUrl(url);

            }
            catch (Exception)
            {
                Logger.Out("There was a problem downloading the chart image from " + url);
                return null;
            }

        }

        
        private static byte[] getImageFromUrl(string url)
        {
            byte[] downloadedData = new byte[0];

            try 
            {
                //Get a data stream from the url
                WebRequest req = WebRequest.Create(url);
                WebResponse response = req.GetResponse();
                Stream stream = response.GetResponseStream();

                //Download in chuncks
                byte[] buffer = new byte[1024];

                //Get Total Size
                int dataLength = (int)response.ContentLength;

                //Download to memory
                //Note: adjust the streams here to download directly to the hard drive
                using (MemoryStream memStream = new MemoryStream())
                {
                    while (true)
                    {
                        //Try to read the data
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);

                        if (bytesRead == 0)
                        {
                            break;
                        }
                        else
                        {
                            //Write the downloaded data
                            memStream.Write(buffer, 0, bytesRead);
                        }
                    }

                    //Convert the downloaded stream to a byte array
                    downloadedData = memStream.ToArray();

                    //Clean up
                    stream.Close();
                }

                //// Load the byte stream and return image
                //using (MemoryStream memStream2 = new MemoryStream(downloadedData))
                //{
                //    Image img = Image.FromStream(memStream2);
                //    memStream2.Close();
                //    return img;
                //}

                return downloadedData;

            }
            catch (Exception)
            {
                //May not be connected to the internet
                //Or the URL might not exist
                Logger.Out("There was an error accessing the URL.");
                return null;
            }
        }

        #endregion
    }
}
