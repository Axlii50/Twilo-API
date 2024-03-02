using System.Xml.Serialization;

namespace Libre_API.OrderStructure
{
    public class LineItem
    {
        [XmlElement("LineNumber")]
        public int LineNumber { get; set; }

        [XmlElement("EAN")]
        public string EAN { get; set; }

        [XmlElement("BuyerItemCode")]
        public string BuyerItemCode { get; set; }

        [XmlElement("ItemDescription")]
        public string ItemDescription { get; set; }

        [XmlElement("OrderedQuantity")]
        public int OrderedQuantity { get; set; }

        [XmlElement("OrderUnitNetPrice")]
        public float OrderUnitNetPrice { get; set; }
    }
}