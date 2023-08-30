using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AteneumAPI.OrderStructure
{
    public class Header
    {
        [XmlElement("deliveryaddresscode")]
        public string deliveryaddresscode { get; set; }
        
        [XmlElement("buyerorderreference")]
        public string buyerorderreference { get; set; }
        
        [XmlElement("remarks")]
        public string remarks { get; set; }
    }
}
