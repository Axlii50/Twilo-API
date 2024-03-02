using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Libre_API.OrderStructure
{
    public class OrderHead
    {
        [XmlElement("Remarks")]
        public string Remarks { get; set; }

        /// <summary>
        /// format yyyy MM dd H m but without spaces
        /// </summary>
        [XmlElement("OrderNumber")]
        public string OrderNumber { get; set; }

        /// <summary>
        /// Format YYYY-MM-DD
        /// </summary>
        [XmlElement("OrderDate")]
        public string OrderDate { get; set; }

        /// <summary>
        /// Format YYYY-MM-DD
        /// skipped field
        /// </summary>
        [XmlElement("ExpectedDeliveryDate")]
        public string ExpectedDeliveryDate { get; set; }

        [XmlElement("DocumentFunctionCode")]
        public string DocumentFunctionCode { get; set; } = "O";

    }
}
