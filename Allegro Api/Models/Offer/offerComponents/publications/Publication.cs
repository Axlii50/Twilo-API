using Allegro_Api.Models.Offer.offerComponents.publications.MarketPlace;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api.Models.Offer.offerComponents.publications
{
    public class Publication
    {
        /// <summary>
        /// PT0S for immediately, PT24H, P2D, P3D, P4D, P5D, P7D, P10D, P14D, P21D, P30D, P60D.
        /// </summary>
        public string duration { get; set; }

        /// <summary>
        ///         INACTIVE - a draft offer
        ///         ACTIVATING - the offer is planned for listing or is during the process of activation
        ///         ACTIVE - the offer is active
        ///         ENDED - the offer was active and is now ended(for whatever reason)
        /// </summary>
        public string status { get; set; }
        public string startingAt { get; set; }
        public string startedAt { get; set; }
        public string endingAt { get; set; }

        /// <summary>
        /// Enum: "USER" "ADMIN" "EXPIRATION" "EMPTY_STOCK" "ERROR"
        /// </summary>
        public string endedBy { get; set; }
        
        public bool republish { get; set; }

        public MarketPlaces marketplaces { get; set; }
    }
}
