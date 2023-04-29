using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api.Models.Offer.offerComponents
{
    public class SellingMode
    {
        public string format { get; set; }

        public PriceModel price { get; set; }
        public PriceModel minimalPrice { get; set; }
        public PriceModel startingPrice { get; set; }
    }
}
