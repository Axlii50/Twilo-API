using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wszystko_API.Global_Components;
using Wszystko_API.Offers.Common_Components;
using Wszystko_API.Offers.Serial_Offer_Model.Components;

namespace Wszystko_API.Offers.Serial_Offer_Model
{
	public class SerialOfferChangesSet
	{
		public int Ids { get; set; }
		public PriceModel Price { get; set; }
		public QuantityModel Quantity { get; set; }
		public bool ShowUnitPrice { get; set; }
		public string GuaranteeId { get; set; }
		public string ComplaintPolicyId { get; set; }
		public string ReturnPolicyId { get; set; }
		public string ShippingTariffId { get; set; }
		public string StockQuantityUnit { get; set; }
		public VatRateType VatRate { get; set; }
		public LeadTimeType LeadTime { get; set; }
		//[AllowNull]
		public int? UserQuantityLimit { get; set; }
	}
}
