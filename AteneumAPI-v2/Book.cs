using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WholesalerApiCommons;

namespace AteneumAPI
{
    public class Book : IProduct
    {
        [Description("ident_ate")]
        public string ExternalId { get; set; }
        public string EAN { get; set; }
        public string ISBN { get; set; }
        [Description("Tytuł")]
        public string Title { get; set; }
        [Description("autor")]
        public string Author { get; set; }
        [Description("wydawnictwo")]
        public string Publisher { get; set; }
        public string opis_wydania { get; set; }
        [Description("rok_wydania")]
        public string IssueYear { get; set; }
        public string krótka_charakterystyka { get; set; }
        public string cena_detal_netto { get; set; }
        public string cena_detal_brutto { get; set; }
        public string kategoria_poziom_1 { get; set; }
        public string kategoria_poziom_2 { get; set; }
        public string kategoria_poziom_3 { get; set; }
        public string plik_zdjecia { get; set; }
        public string hash { get; set; }
        public int MagazinCount { get; set; }
        public float PriceWholeSaleBrutto { get; set; }
        public float PriceWholeSaleNetto { get; set; }
        public int VAT { get; set; }
        public string ImageName { get; set; }
        public string stawka_vat { get; set; }
    }
}
