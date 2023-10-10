using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wszystko_API.Product
{
	public class RequestAddProductOffer
	{
		//[Required]
		public string Title { get; set; }
		public double Price { get; set; }
		public int CategoryId { get; set; }
		[Description("URL")]
		public string Gallery { get; set; }
		public VatRateType VatRate { get; set; }
		public List<ParameterKit> Parameters { get; set; }
		public List<Description> Descriptions { get; set; }
		public string guaranteeId { get; set; }
		public string complaintPolicyId { get; set; }
		public string returnPolicyId { get; set; }
		public string shippingTariffId { get; set; }
		public LeadTimeType LeadTime { get; set; }
		public StockQuantityUnitType StockQuantityUnit { get; set; }
		//[Required]
		public OfferStatusType OfferStatus { get; set; }
		public int UserQuantityLimit { get; set; }
		//[Required]
		public bool IsDraft { get; set; }
		public int StockQuantity { get; set; }
		public bool ShowUnitPrice { get; set; }
	}
}
