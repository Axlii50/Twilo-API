using Allegro_Api.Models.Offer.offerComponents;
using Allegro_Api.Models.Offer.offerComponents.publications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api.Models.Offer
{
    public class OfferModel
    {
        public string name { get; set; }

        public Payments payments { get; set; }

        /// <summary>
        /// id z systemu poza allegro
        /// </summary>
        public Base external { get;set; }

       // public string description { get; set; }

        public ProductItem[] productset { get; set;  }

        public Base category { get; set; }

        public Publication publication { get; set; }

        //b2b

        //attachments

        //fundrasingCampaing

        //additionalServices

        public Stock stock { get; set; }

        public SellingMode sellingMode { get; set; }

        public string[] images { get; set; }

        //TaxSetttings

        //messageTosellerSettings

    }
}
