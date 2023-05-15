using Allegro_Api.Models.Offer.offerComponents;
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

        public Stock stock { get; set; }

        public SellingMode sellingMode { get; set; }





    }
}
