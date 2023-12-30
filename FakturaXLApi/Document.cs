using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FakturaXLApi
{
    [XmlRoot("dokument")]
    public class Dokument
    {
        [XmlElement("api_token")]
        public string ApiToken { get; set; }

        [XmlElement("typ_faktury")]
        public int TypFaktury { get; set; }

        [XmlElement("typ_faktur_podtyp")]
        public int TypFakturPodtyp { get; set; }

        [XmlElement("obliczaj_sume_wartosci_faktury_wg")]
        public int ObliczajSumeWartosciFakturyWg { get; set; }

        [XmlElement("nabywca")]
        public Nabywca Nabywca { get; set; }

        [XmlElement("faktura_pozycje")]
        public List<FakturaPozycja> FakturaPozycje { get; set; }

        [XmlElement("tag")]
        public List<string> Tagi { get; set; }
    }
}
