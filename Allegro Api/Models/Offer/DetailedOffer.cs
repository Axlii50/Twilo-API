using Allegro_Api.Models.Offer.offerComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api.Models.Offer
{
    /// <summary>
    /// na razie tylko zawarte produkty
    /// </summary>
    public class DetailedOffer
    {

        public ProductItem[] productSet { get; set; }


        public string name { get; set; }

    }
}
