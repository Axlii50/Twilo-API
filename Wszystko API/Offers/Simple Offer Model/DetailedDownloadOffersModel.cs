using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wszystko_API.Global_Components;
using Wszystko_API.Offers.Common_Components;
using Wszystko_API.Offers.General_Offer_Model.Components;
using Wszystko_API.Offers.Simple_Offer_Model.Interface;

namespace Wszystko_API.Offers.Simple_Offer_Model
{
	public class DetailedDownloadOffersModel: IDownloadOffersModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public double Price { get; set; }
		public int CategoryId { get; set; }
		public Uri[] Gallery { get; set; }
		public string VatRate { get; set; }
		public ParameterKit[] Parameters { get; set; }
		public Description[] Descriptions { get; set; }
		public string GuaranteeId { get; set; }
		public string ComplaintPolicyId { get; set; }
		public string ReturnPolicyId { get; set; }
		public string ShippingTariffId { get; set; }
		public string LeadTime { get; set; }
		public StockQuantityUnitType StockQuantityUnit { get; set; }
		public string SellerId { get; set; }
		public Uri ShopUrlPart { get; set; }
		public OfferStatusType Status { get; set; }
		public DateTime CreationDate { get; set; }
		public bool AdultOnly { get; set; }
		public double LowestShippingCost { get; set; }
		public string SnapshotId { get; set; }
		public int userQuantityLimit { get; set; }
		public Uri OfferLink { get; set; }
		public int Purchased { get; set; }
		public int Visits { get; set; }
		public int Sold { get; set; }
		public int Quantity { get; set; }
		public UnitPricesType UnitPrice { get; set; }
		public DateTime ModificationDate { get; set; }
		public bool IsDraft { get; set; }
		public bool ShowUnitPrice { get; set; }
	}
}
