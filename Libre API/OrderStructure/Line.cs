using System.Xml.Serialization;

namespace Libre_API.OrderStructure
{
    public class Line
    {
        [XmlElement("Line-Item")]
        public LineItem item { get; set; }
    }
}