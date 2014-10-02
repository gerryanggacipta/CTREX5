using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TREX.Entities
{
    [XmlType(TypeName = "trade")]
    [Serializable]
    public class Trade
    {
        [XmlElement("buy")]
        public bool Buy { get; set; }

        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("stock")]
        public string Stock { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("size")]
        public int Size { get; set; }

        [XmlElement("whenAsDate")]
        public string WhenAsDate { get; set; }

        public bool Short { get; set; }

        //[XmlIgnoreAttribute]
        public string StrategyID { get; set; }

        //[XmlIgnoreAttribute]
        public string Position { get; set; }

        //[XmlIgnoreAttribute]
        public bool Auto { get; set; }

        public decimal PnL { get; set; }

    }
}
