using Allegro_Api.Models.Offer.offerComponents;
using Allegro_Api.Models.Offer.offerComponents.publications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api.Models.Offer
{
    public class PublicationModel
    {
        public Criteria[] offerCriteria { get; set; }

        public ModifiPublication publication { get; set; }
    }
}
