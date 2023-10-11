using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wszystko_API.Offers.Interfaces;

namespace Wszystko_API.Product
{
    public class RequestAddProductOffer : IBasicOffer
	{
		public string Title { get; set; }
		public double Price { get; set; }
		public int CategoryId { get; set; }
		public string[] Gallery { get; set; }
		public VatRateType VatRate { get; set; }
		public List<ParameterKit> Parameters { get; set; }
		public List<Description> Descriptions { get; set; }
		public string GuaranteeId { get; set; }
		public string ComplaintPolicyId { get; set; }
		public string ReturnPolicyId { get; set; }
		public string ShippingTariffId { get; set; }
		public LeadTimeType LeadTime { get; set; }
		public StockQuantityUnitType StockQuantityUnit { get; set; }
		public int UserQuantityLimit { get; set; }
		//[Required]
		public bool IsDraft { get; set; }
		public int StockQuantity { get; set; }
		public OfferStatusType OfferStatus { get; set; }
		public bool ShowUnitPrice { get; set; }
	}
}
