using Allegro_Api.Models.Order.checkoutform.components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api.Models.Order.checkoutform
{
    public class DetailedCheckOutForm
    {
        public string id {  get; set; }

        /// <summary>
        /// Enum: "BOUGHT" "FILLED_IN" "READY_FOR_PROCESSING" "CANCELLED"
        /// </summary>
        public string status { get; set; }

        public CheckoutFormFulfillment fulfillment { get; set; }
    }
}
