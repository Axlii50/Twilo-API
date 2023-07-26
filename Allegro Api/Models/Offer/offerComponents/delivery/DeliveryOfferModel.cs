using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api.Models.Offer.offerComponents.delivery
{
    public class DeliveryOfferModel
    {
        /// <summary>
        /// PT0S for immediately, PT24H, P2D, P3D, P4D, P5D, P7D, P10D, P14D, P21D, P30D, P60D.
        /// it cant be here bcs this information can be obtained only from detailed offer request not from basic one
        /// </summary>
        //public string handlingTime { get; set; }

        public Base shippingRates { get; set; }
    }
}
