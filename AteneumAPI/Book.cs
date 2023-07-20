using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AteneumAPI
{
    public class Book
    {
        public string ident_ate { get; set; }

        public BookRecord BookData { get; set; }

        public int MagazinCount { get; set; }
        public float Cena_detaliczna_netto { get; set; }
        public float Cena_detaliczna_brutto { get; set; }
    }
}
