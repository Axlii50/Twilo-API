using Allegro_Api.Models.Offer.Interfaces;
using Allegro_Api.Models.Offer.offerComponents;
using Allegro_Api.Models.Offer.offerComponents.delivery;
using Allegro_Api.Models.Offer.offerComponents.publications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api.Models.Offer
{
    /// <summary>
    /// <list type="bullet">
    /// <item>id								Implemented</item>
    /// <item>name							    Implemented</item>
    /// <item>category						    Implemented</item>
    /// <item>primaryImage					    Implemented</item>
    /// <item>sellingMode					    Implemented</item>
    /// <item>SaleInfo</item>
    /// <item>stock							    Implemented</item>
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
    //TODO chyba trzeba bedzie ogarnac troche tą klase i te dictionary po zamieniac na klasy kltóre są juz stworzone
    public struct SimpleOfferModel : IBasicOffer
    {
        public string id { get ; set ; }
        public string name { get ; set ; }
        public Dictionary<string, string> category { get ; set ; }
        public Dictionary<string, string> primaryImage { get ; set ; }
        public SellingMode sellingMode { get ; set ; }
        public SaleInfo saleInfo { get ; set ; }
        public Stats stats { get ; set ; }
        public Publication publication { get ; set ; }
        public AfterSalesServices afterSalesServices { get ; set ; }
        public AdditionalServices additionalServices { get ; set ; }
        public DeliveryOfferModel delivery { get ; set ; }
        public b2b b2b { get ; set ; }
        public FundRaisingCampaign fundraisingCampaign { get ; set ; }
        public Base external { get; set; }
        public Stock stock { get; set; }
    }
}
