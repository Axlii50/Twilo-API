using Allegro_Api.Models.Offer.offerComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api.Models.Order.checkoutform.components
{
    public class CheckoutFormLineItem
    {
        public string id { get; set; }

        public int quantity { get; set; } 

        public  OfferReference offer { get; set; }

        public PriceModel originalPrice { get; set; }

        public PriceModel price { get; set; }

        public string boughtAt { get; set; }
    }
}
