using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AteneumAPI.OrderStructure
{
    [XmlRoot("atesapiorder")]
    public class AtesApiOrder
    {
        [XmlElement("Auth")]
        public Auth Auth { get; set; }

        [XmlElement("header")]
        public Header header { get; set; }

        [XmlElement("lines")]
        public Line[] lines { get; set; }
    }
}
