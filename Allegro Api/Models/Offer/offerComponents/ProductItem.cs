using Allegro_Api.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api.Models.Offer.offerComponents
{
    public class ProductItem
    {
        public AllegroOfferProduct product { get; set; }

        public BaseValue quantity { get; set; }

    }
}
