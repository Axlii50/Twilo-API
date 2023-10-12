using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wszystko_API.Product;

namespace Wszystko_API.Offers.Serial_Offer_Model.Components
{
	public class QuantityModel
	{
		public int Value { get; set; }
		public SimpleChangeType Change { get; set; }
		public bool ShowUnitPrice { get; set; }
		public string GuaranteeId { get; set; }
		public string ComplaintPolicyId { get; set; }
		public string ReturnPolicyId { get; set; }
		public string ShippingTariffId { get; set; }
		public StockQuantityUnitType StockQuantityUnit { get; set; }
		public VatRateType VatRate { get; set; }
		public LeadTimeType LeadTime { get; set; }
		//[AllowNull]
		public int? UserQuantityLimit { get; set; }
	}
}
