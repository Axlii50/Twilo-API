using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Libre_API.OrderStructure
{
    public class Base
    {
        /// <summary>
        /// kod zamawiającego w systemie liber
        /// </summary>
        [XmlElement("ILN")]
        public string ILN { get; set; }
    }
}
