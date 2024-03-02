using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AteneumAPI
{
    public class MagazinAndPriceRecord
    {
        public string ident_ate { get; set; }
        public string Stan_magazynowy { get; set; }
        public string Cena_detaliczna_netto { get; set; }
        public string Cena_detaliczna_brutto { get; set; }
        public string Stawka_VAT { get; set; }
    }
}
