using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api.Models.Offer.offerComponents
{
    public class AfterSalesServices
    {
        /// <summary>
        /// The implied warranty information.
        /// </summary>
        public Base impliedWarranty { get; set; }

        /// <summary>
        /// The return policy information.
        /// </summary>
        public Base returnPolicy { get; set; }

        /// <summary>
        /// The warranty information.
        /// </summary>
        public Base warranty { get; set; }
    }
}
