using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api.Models.Offer.offerComponents.delivery
{
    public class Delivery
    {
        /// <summary>
        /// PT0S for immediately, PT24H, P2D, P3D, P4D, P5D, P7D, P10D, P14D, P21D, P30D, P60D.s
        /// </summary>
        public string handlingTime { get; set; }

        /// <summary>
        /// fill with id of shipping rates
        /// </summary>
        public Base shippingRates { get; set; }
    }
}
