﻿using System;
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
        [XmlElement("auth")]
        public Auth auth { get; set; }

        [XmlElement("header")]
        public Header header { get; set; }

       // [XmlElement("lines")]
        [XmlArrayItem("line")]
        public Line[] lines { get; set; }
    }
}
