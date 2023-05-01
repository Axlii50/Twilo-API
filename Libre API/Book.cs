using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Libre_API
{
    
    public class Book
    {
        [XmlAttribute("id")]
        public string ID;

        /// <summary>
        ///  kod kreskowy,
        /// </summary>
        [XmlElement("ean", IsNullable = true)]
        public string EAN;

        /// <summary>
        ///  kod kreskowy,
        /// </summary>
        [XmlElement("isbn", IsNullable = true)]
        public string ISBN;

        /// <summary>
        ///  ilość stanu magazynowego,
        /// </summary>
        /// 
        [XmlElement("stock")]
        public int MagazineCount;

        /// <summary>
        ///  nazwa grupy towarów,
        /// </summary>
        
        public string Group;

        /// <summary>
        /// nazwa towaru
        /// </summary>
        [XmlElement("title", IsNullable = true)]
        public string Title;

        /// <summary>
        /// tytuł oryginału
        /// </summary>
        [XmlElement("originaltitle", IsNullable = true)]
        public string OriginalTitle;

        /// <summary>
        /// autor książki (nazwisko i imię) lub autorzy rozdzieleni przecinkiem
        /// </summary>
        [XmlElement("author", IsNullable = true)]
        public string Author;

        /// <summary>
        /// tłumacz książki (nazwisko i imię) lub tłumacze rozdzieleni przecinkiem
        /// </summary>
        [XmlElement("translator", IsNullable = true)]
        public string Transaltor;

        /// <summary>
        /// nazwa wydawcy
        /// </summary>
        [XmlElement("publisher", IsNullable = true)]
        public string Publisher;

        /// <summary>
        /// nazwa serii towaru
        /// </summary>
        [XmlElement("series", IsNullable = true)]
        public string Series;

        /// <summary>
        /// – cena zakupu brutto po rabacie dla klienta
        /// </summary>
        [XmlElement("purchasegrossprice")]
        public float PriceBruttoAferDiscount;

        /// <summary>
        /// – cena zakupu netto po rabacie dla klienta
        /// </summary>
        public float PriceNettoAferDiscount;

        /// <summary>
        /// – rok wydania książki
        /// </summary>
        [XmlElement("grossprice")]
        public float PriceBrutto;

        /// <summary>
        /// – stawka podatku VAT (np. 5%, 23%, 8%, 8%)
        /// </summary>
        [XmlElement("vat", IsNullable = true)]
        public string Vat;

        /// <summary>
        /// –data premiery rynkowej, format RRRR-MM-DD
        /// </summary>
        [XmlElement("releasedate", IsNullable = true)]
        public string PublishDate;

        /// <summary>
        /// – data rozpoczęcia dystrybucji, czyli moment, w którym towar fizycznie pojawia się 
        ///   w magazynie Liber SA, format RRRR-MM-DD
        /// </summary>
        [XmlElement("dateofsale", IsNullable = true)]
        public string SaleDate;

        /// <summary>
        /// rok wydania książki
        /// </summary>
        [XmlElement("issueyear", IsNullable = true)]
        public string YearOfPublish;

        /// <summary>
        /// – numer wydania książk
        /// </summary>
        [XmlElement("issuenumber", IsNullable = true)]
        public string NumberOfPublish;

        /// <summary>
        /// – kategoria towaru wraz z podkategorią, rozdzielona znakiem /
        /// </summary>
        [XmlElement("category", IsNullable = true)]
        public string Category;

        /// <summary>
        /// opis produktu
        /// </summary>
        [XmlElement("description", IsNullable = true)]
        public string Description;

        /// <summary>
        /// rodzaj oprawy
        /// </summary>
        [XmlElement("cover", IsNullable = true)]
        public string Cover;

        /// <summary>
        /// liczba stron
        /// </summary>
        [XmlElement("sites", IsNullable = true)]
        public string NumberOfPages;

        /// <summary>
        /// liczba stron
        /// </summary>
        [XmlElement("height")]
        public string Height;

        /// <summary>
        ///  szerokość towaru w mm
        /// </summary>
        [XmlElement("width")]
        public string Width;

        /// <summary>
        /// – grubość książki lub długość towaru w mm
        /// </summary>
        [XmlElement("thickness", IsNullable = true)]
        public string Thickness;

        /// <summary>
        /// – waga towaru w kg
        /// </summary>
        [XmlElement("weight", IsNullable = true)]
        public string Weight;
    }
}
