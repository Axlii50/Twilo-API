using System.Xml.Serialization;

namespace AteneumAPI.OrderStructure
{
    public class Auth
    {
        [XmlElement("login")]
        public string Login { get; set; }

        [XmlElement("salt")]
        public string salt { get; set; }

        [XmlElement("passfingerprint")]
        public string passfingerprint { get; set; }
    }
}