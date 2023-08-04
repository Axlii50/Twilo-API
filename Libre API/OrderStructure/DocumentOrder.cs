using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Libre_API.OrderStructure
{
    [XmlRoot("Document-Order")]
    public class DocumentOrder
    {
        [XmlElement("Order-Header")]
        public OrderHead Head { get; set; }

        [XmlElement("Order-Parties")]
        public OrderParties parties { get; set; }

        [XmlElement("Order-Lines")]
        public OrderLines products { get; set; }

        [XmlElement("Order-Summary")]
        public OrderSummary summary { get; set; }
    }
}
