﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AteneumAPI
{
    internal class PriceWholeSale
    {
        public string AteneumID { get; set; }
        public float cena_detaliczna_brutto { get; set; }
        public float cena_hurtowa_netto { get; set; }
        public int vat_procentowy { get; set; }
    }
}
