using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api.Models.Order.checkoutform.components
{
    public class CheckoutFormDeliveryReference
    {
        public CheckoutFormDeliveryTime time { get; set; }

        public CheckoutFormDeliveryMethod method { get; set; }

        public CheckoutFormDeliveryAddress address { get;set; }
    }
}
