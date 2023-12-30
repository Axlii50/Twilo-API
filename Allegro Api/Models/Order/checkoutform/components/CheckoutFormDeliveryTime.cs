using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api.Models.Order.checkoutform.components
{
    public class CheckoutFormDeliveryTime
    {
        public string from {  get; set; }   

        public string to { get; set; }

        public CheckoutFormDeliveryTimeDispatch dispatch { get; set; }
    }
}
