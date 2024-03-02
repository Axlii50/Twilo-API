using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AteneumAPI
{
    public class BookRecord
    {
        public string ident_ate { get; set; }
        public string EAN { get; set; }
        public string ISBN { get; set; }
        public string Tytuł { get; set; }
        public string autor { get; set; }
        public string wydawnictwo { get; set; }
        public string opis_wydania { get; set; }
        public string rok_wydania { get; set; }
        public string krótka_charakterystyka { get; set; }
        public string cena_detal_netto { get; set; }
        public string stawka_vat { get; set; }
        public string cena_detal_brutto { get; set; }
        public string kategoria_poziom_1 { get; set; }
        public string kategoria_poziom_2 { get; set; }
        public string kategoria_poziom_3 { get; set; }
        public string plik_zdjecia { get; set; }
        public string hash { get; set; }
    }
}
