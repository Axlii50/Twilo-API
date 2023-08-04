using System.Xml.Serialization;

namespace Libre_API.OrderStructure
{
    public class OrderLines
    {
        [XmlElement("Line")]
        public Line[] Line { get; set; }
    }
}