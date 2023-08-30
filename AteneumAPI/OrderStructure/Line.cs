using System.Xml.Serialization;

namespace AteneumAPI.OrderStructure
{
    public class Line
    {
        /// <summary>
        /// Identyfikator z bazy Ateneum zamawianego towaru.
        /// </summary>
        [XmlElement("ateid")]
        public int ateid { get; set; }

        /// <summary>
        /// Kod ean zamawianego towaru, jeżeli podajemy EAN, nie ma potrzeby podawania 
        ///ateid.Ateid ma priorytet przed EAN’em(jeżeli podamy ateid, ean nie będzie
        /// sprawdzany)
        /// </summary>
        [XmlElement("ean")]
        public int ean { get; set; }

        /// <summary>
        /// Zamawiana ilość towaru
        /// </summary>
        [XmlElement("quantity")]
        public int quantity { get; set; }

        /// <summary>
        /// Dowolny ciąg znaków, identyfikujący towar po stronie zamawiającego. Nie jest 
        /// wymagany, aczkolwiek może ułatwić identyfikacje towaru w wypadku gdy towar
        /// nie zostanie odnaleziony w bazie Ateneum.
        /// </summary>
        [XmlElement("buyerproductreference")]
        public string buyerproductreference { get; set; }
    }
}