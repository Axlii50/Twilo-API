using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WholesalerApiCommons;

namespace Libre_API
{

    public class Book : IProduct
    {
        [XmlAttribute("id")]
        public string ExternalId { get; set; }

        /// <summary>
        ///  kod kreskowy,
        /// </summary>
        [XmlElement("ean", IsNullable = true)]
        public string EAN { get; set; }

        /// <summary>
        ///  kod kreskowy,
        /// </summary>
        [XmlElement("isbn", IsNullable = true)]
        public string ISBN { get; set; }

        /// <summary>
        ///  ilość stanu magazynowego,
        /// </summary>
        [XmlElement("stock")]
        public int MagazineCount { get; set; }

        /// <summary>
        ///  nazwa grupy towarów,
        ///  pole dostępne przy pobraniu z dane3.aspx (dokumentacja)
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// nazwa towaru
        /// </summary>
        [XmlElement("title", IsNullable = true)]
        public string Title { get; set; }

        /// <summary>
        /// tytuł oryginału
        /// </summary>
        [XmlElement("originaltitle", IsNullable = true)]
        public string OriginalTitle { get; set; }

        /// <summary>
        /// autor książki (nazwisko i imię) lub autorzy rozdzieleni przecinkiem
        /// </summary>
        [XmlElement("author", IsNullable = true)]
        public string Author { get; set; }

        /// <summary>
        /// tłumacz książki (nazwisko i imię) lub tłumacze rozdzieleni przecinkiem
        /// </summary>
        [XmlElement("translator", IsNullable = true)]
        public string Translator { get; set; }

        /// <summary>
        /// nazwa wydawcy
        /// </summary>
        [XmlElement("publisher", IsNullable = true)]
        public string Publisher { get; set; }

        /// <summary>
        /// nazwa serii towaru
        /// </summary>
        [XmlElement("series", IsNullable = true)]
        public string Series { get; set; }

        /// <summary>
        /// – cena zakupu hurtowa brutto po rabacie (dla Twilo)
        /// </summary>
        [XmlElement("purchasegrossprice")]
        public float PriceWholeSaleBrutto { get; set; }

        /// <summary>
        /// – cena zakupu netto po rabacie (dla Twilo)
        /// pole dostępne przy pobraniu z dane3.aspx (dokumentacja)
        /// </summary>
        public float PriceNettoAferDiscount { get; set; }

        /// <summary>
        /// – cena detaliczna brutto
        /// </summary>
        [XmlElement("grossprice")]
        public float PriceBrutto { get; set; }

        /// <summary>
        /// – stawka podatku VAT (np. 5%, 23%, 8%, 8%)
        /// </summary>
        [XmlElement("vat", IsNullable = true)]
        public string Vat { get; set; }

        /// <summary>
        /// –data premiery rynkowej, format RRRR-MM-DD
        /// </summary>
        [XmlElement("releasedate", IsNullable = true)]
        public string PublishDate { get; set; }

        /// <summary>
        /// – data rozpoczęcia dystrybucji, czyli moment, w którym towar fizycznie pojawia się
        ///   w magazynie Liber SA, format RRRR-MM-DD
        /// </summary>
        [XmlElement("dateofsale", IsNullable = true)]
        public string SaleDate { get; set; }

        /// <summary>
        /// rok wydania książki
        /// </summary>
        [XmlElement("issueyear", IsNullable = true)]
        public string IssueYear { get; set; }

        /// <summary>
        /// – numer wydania książk
        /// </summary>
        [XmlElement("issuenumber", IsNullable = true)]
        public string NumberOfPublish { get; set; }

        /// <summary>
        /// – kategoria towaru wraz z podkategorią, rozdzielona znakiem /
        /// </summary>
        [XmlElement("category", IsNullable = true)]
        public string Category { get; set; }

        /// <summary>
        /// opis produktu
        /// </summary>
        [XmlElement("description", IsNullable = true)]
        public string Description { get; set; }

        /// <summary>
        /// rodzaj oprawy
        /// </summary>
        [XmlElement("cover", IsNullable = true)]
        public string Cover { get; set; }

        /// <summary>
        /// liczba stron
        /// </summary>
        [XmlElement("sites", IsNullable = true)]
        public string NumberOfPages { get; set; }

        /// <summary>
        /// liczba stron
        /// </summary>
        [XmlElement("height")]
        public string Height { get; set; }

        /// <summary>
        ///  szerokość towaru w mm
        /// </summary>
        [XmlElement("width")]
        public string Width { get; set; }

        /// <summary>
        /// – grubość książki lub długość towaru w mm
        /// </summary>
        [XmlElement("thickness", IsNullable = true)]
        public string Thickness { get; set; }

        /// <summary>
        /// – waga towaru w kg
        /// </summary>
        [XmlElement("weight", IsNullable = true)]
        public string Weight { get; set; }

        /// <summary>
        /// ścieżka do zdjęcia
        /// </summary>
        public string ImageName { get; set; }
    }
}
