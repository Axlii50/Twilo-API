using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libre_API
{
    public class Book
    {
        /// <summary>
        ///  kod kreskowy,
        /// </summary>
        public string EAN;

        /// <summary>
        ///  kod kreskowy,
        /// </summary>
        public string ISBN;

        /// <summary>
        ///  ilość stanu magazynowego,
        /// </summary>
        public int MagazineCount;

        /// <summary>
        ///  nazwa grupy towarów,
        /// </summary>
        public string Group;

        /// <summary>
        /// nazwa towaru
        /// </summary>
        public string Title;

        /// <summary>
        /// tytuł oryginału
        /// </summary>
        public string OriginalTitle;

        /// <summary>
        /// autor książki (nazwisko i imię) lub autorzy rozdzieleni przecinkiem
        /// </summary>
        public string Author;

        /// <summary>
        /// tłumacz książki (nazwisko i imię) lub tłumacze rozdzieleni przecinkiem
        /// </summary>
        public string Transaltor;

        /// <summary>
        /// nazwa wydawcy
        /// </summary>
        public string Publisher;

        /// <summary>
        /// nazwa serii towaru
        /// </summary>
        public string Series;

        /// <summary>
        /// – cena zakupu brutto po rabacie dla klienta
        /// </summary>
        public float PriceBruttoAferDiscount;

        /// <summary>
        /// – cena zakupu netto po rabacie dla klienta
        /// </summary>
        public float PriceNettoAferDiscount;

        /// <summary>
        /// – rok wydania książki
        /// </summary>
        public float PriceBrutto;

        /// <summary>
        /// – stawka podatku VAT (np. 5%, 23%, 8%, 8%)
        /// </summary>
        public int Vat;

        /// <summary>
        /// –data premiery rynkowej, format RRRR-MM-DD
        /// </summary>
        public DateOnly PublishDate;

        /// <summary>
        /// – data rozpoczęcia dystrybucji, czyli moment, w którym towar fizycznie pojawia się 
        ///   w magazynie Liber SA, format RRRR-MM-DD
        /// </summary>
        public DateOnly SaleDate;

        /// <summary>
        /// rok wydania książki
        /// </summary>
        public int YearOfPublish;

        /// <summary>
        /// – numer wydania książk
        /// </summary>
        public int NumberOfPublish;

        /// <summary>
        /// – kategoria towaru wraz z podkategorią, rozdzielona znakiem /
        /// </summary>
        public string Category;

        /// <summary>
        /// rodzaj oprawy
        /// </summary>
        public string Cover;

        /// <summary>
        /// liczba stron
        /// </summary>
        public int NumberOfPages;

        /// <summary>
        /// liczba stron
        /// </summary>
        public int Height;

        /// <summary>
        ///  szerokość towaru w mm
        /// </summary>
        public int Width;

        /// <summary>
        /// – grubość książki lub długość towaru w mm
        /// </summary>
        public float Thickness;

        /// <summary>
        /// – waga towaru w kg
        /// </summary>
        public float Weight;
    }
}
