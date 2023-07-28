using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api.Models.Order.checkoutform.components
{
    public class CheckoutFormFulfillment
    {
        /// <summary>
        /// Enum: "NEW" "PROCESSING" "READY_FOR_SHIPMENT" "READY_FOR_PICKUP" "SENT" "PICKED_UP" "CANCELLED" "SUSPENDED"
        /// </summary>
        public string status { get; set; }
    }
}
