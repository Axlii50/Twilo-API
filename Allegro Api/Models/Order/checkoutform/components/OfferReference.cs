﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api.Models.Order.checkoutform.components
{
    public class OfferReference
    {
        public string id { get; set; }
        public string name { get; set; }
        public Base external { get; set; }
    }
}
