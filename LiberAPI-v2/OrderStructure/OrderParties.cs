using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Libre_API.OrderStructure
{
    public class OrderParties
    {

        [XmlElement("Buyer")]
        public Base Buyer { get; set; }

        [XmlElement("Seller")]
        public Base Seller { get; set; }

        [XmlElement("DeliveryPoint")]
        public Base DeliveryPoint { get; set; }
    }
}
