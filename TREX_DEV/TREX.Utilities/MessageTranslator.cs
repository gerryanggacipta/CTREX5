using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using TREX.Entities;

namespace TREX.Utilities
{
    public static class MessageTranslator
    {
        public static string ConvertTradeToXML(Trade t)
        {
            try
            {
                XmlSerializer x = new XmlSerializer(t.GetType());
                StringWriter strWriter = new StringWriter();
                x.Serialize(strWriter, t);
                return strWriter.ToString();
            } 
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ConvertStrategyToXML(Strategy s)
        {
            try
            {
                XmlSerializer x = new XmlSerializer(s.GetType());
                StringWriter strWriter = new StringWriter();
                x.Serialize(strWriter, s);
                return strWriter.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Trade ConvertXMLToTrade(string xml)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Trade));
                StringReader stringReader = new StringReader(xml);
                var obj = xmlSerializer.Deserialize(stringReader);
                return (Trade)obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
