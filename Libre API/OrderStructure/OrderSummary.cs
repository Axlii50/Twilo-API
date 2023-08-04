using System.Xml.Serialization;

namespace Libre_API.OrderStructure
{
    public class OrderSummary
    {
        [XmlElement("TotalLines")]
        public int TotalLines { get; set; }

        [XmlElement("TotalOrderedAmount")]
        public int TotalOrderedAmount { get; set; }
    }
}