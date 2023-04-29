using Allegro_Api.Models.Offer.offerComponents.publications.MarketPlace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api.Models.Offer.offerComponents.publications
{
    public class Publication
    {
        public string status { get; set; }
        public string startingAt { get; set; }
        public string startedAt { get; set; }
        public string endingAt { get; set; }
        
        public MarketPlaces marketplaces { get; set; }
    }
}
