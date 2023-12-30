using System.Xml.Serialization;

namespace FakturaXLApi
{
    public class FakturaPozycja
    {
        [XmlElement("nazwa")]
        public string Nazwa { get; set; }

        [XmlElement("kod_produktu")]
        public string KodProduktu { get; set; }

        // inne właściwości...
    }
}
