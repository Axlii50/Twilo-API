using System.Xml.Serialization;

namespace FakturaXLApi
{
    public class Nabywca
    {
        [XmlElement("firma_lub_osoba_prywatna")]
        public int FirmaLubOsobaPrywatna { get; set; }

        [XmlElement("nazwa")]
        public string Nazwa { get; set; }

        // inne właściwości...
    }
}
