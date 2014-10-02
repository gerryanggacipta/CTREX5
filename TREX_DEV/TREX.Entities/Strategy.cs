using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TREX.Entities
{
    [XmlType(TypeName = "strategy")]
    [Serializable]
    public class Strategy
    {
        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("xmlConfig")]
        public string XmlConfig { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }

        [XmlElement("activated")]
        public bool Activated { get; set; }

        [XmlElement("stock")]
        public string Stock { get; set; }

        [XmlElement("buy")]
        public bool Buy { get; set; }

        [XmlElement("short")]
        public bool Short { get; set; }

        [XmlElement("size")]
        public long Size { get; set; }

        private bool _sell;
        public bool Sell { 
            get { return !Buy; }
            set { _sell = value;  } 
        }
    }
}
