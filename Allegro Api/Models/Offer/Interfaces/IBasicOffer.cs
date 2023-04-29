using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allegro_Api.Models.Offer.offerComponents;
using Allegro_Api.Models.Offer.offerComponents.delivery;
using Allegro_Api.Models.Offer.offerComponents.publications;

namespace Allegro_Api.Models.Offer.Interfaces
{
    /// 
    /// <summary>
    ///  https://developer.allegro.pl/documentation#operation/searchOffersUsingGET
    /// <list type="bullet">
    /// <item>id</item> 
    /// <item>name</item>
    /// <item>category</item>
    /// <item>primaryImage</item>
    /// <item>sellingMode</item>
    /// <item>SaleInfo</item>
    /// <item>stock</item>
    /// <item>stats</item>
    /// <item>publication</item>
    /// <item>aferSalesServices</item>
    /// <item>AdditionalServices</item>
    /// <item>extrenal</item>
    /// <item>delivery</item>
    /// <item>b2b</item>
    /// <item>fundrasingCampaing</item>
    /// <item>additonalMarketplaces</item>
    /// </list>
    /// </summary>
    public interface IBasicOffer
    {
        /// <summary>
		/// Offer ID.
		/// </summary>
		public string id { get; set; }

        /// <summary>
        /// The text to search in the offer title.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// The category to which the offer is listed for sale. (value)
        /// </summary>
        public Dictionary<string, string> category { get; set; }

        /// <summary>
        /// The image used as a thumbnail on the listings.
        /// </summary>
        public Dictionary<string, string> primaryImage { get; set; }

        /// <summary>
        /// Information on the offer's selling mode.
        /// </summary>
        public SellingMode sellingMode { get; set; }

        /// <summary>
        /// The current highest bid in auction format.
        /// </summary>
        public SaleInfo saleInfo { get; set; }

        /// <summary>
        /// The offer's statistics on the base marketplace.
        /// </summary>
        public Stats stats { get; set; }

        /// <summary>
        /// nformation on the offer's stock.
        /// </summary>
        public Dictionary<string, int> stock { get; set; }

        /// <summary>
        /// Information on the offer's publication status and dates.
        /// </summary>
        public Publication publication { get; set; }

        /// <summary>
        /// The definitions of the different after sales services assigned to the offer.
        /// </summary>
        public AfterSalesServices afterSalesServices { get; set; }

        /// <summary>
        /// The definition of the additional services assigned to the offer.
        /// </summary>
        public AdditionalServices additionalServices { get; set; }

        /// <summary>
        /// The information on the offer in an external system.
        /// </summary>
        public Base external { get; set; }

        /// <summary>
        /// Delivery information.
        /// </summary>
        public Delivery delivery { get; set; }

        /// <summary>
        /// Information about offer's business properties.
        /// </summary>
        public b2b b2b { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FundRaisingCampaign fundraisingCampaign { get; set; }

        //TODO additionalMarketplaces
    }
}
