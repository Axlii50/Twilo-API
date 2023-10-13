using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wszystko_API.Product;

namespace Wszystko_API.Offers.Interfaces
{
    public interface IBasicOffer
    {
        //[Required]
        public string Title { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        //[Description("URL")]
        public string[] Gallery { get; set; }
        public string VatRate { get; set; }
        public List<ParameterKit> Parameters { get; set; }
        public List<Description> Descriptions { get; set; }
        public string GuaranteeId { get; set; }
        public string ComplaintPolicyId { get; set; }
        public string ReturnPolicyId { get; set; }
        public string ShippingTariffId { get; set; }
        public string LeadTime { get; set; }
        public string StockQuantityUnit { get; set; }
        //[Required]
        public OfferStatusType OfferStatus { get; set; }
        public int UserQuantityLimit { get; set; }
        //[Required]
        public bool IsDraft { get; set; }
        public int StockQuantity { get; set; }
    }
}
