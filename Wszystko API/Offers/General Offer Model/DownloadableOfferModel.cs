using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wszystko_API.Offers.Interfaces;
using Wszystko_API.Product;

namespace Wszystko_API.Offers.General_Offer_Model
{
    public class DownloadableOfferModel : IDetailedOffer
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public double Price { get; set; }
		public int CategoryId { get; set; }
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
		public OfferStatusType OfferStatus { get; set; }
		public int UserQuantityLimit { get; set; }
		public bool IsDraft { get; set; }
		public int StockQuantity { get; set; }
		public string SellerId { get; set; }
		public string ShopUrlPart { get; set; }
		public DateTime CreationDate { get; set; }
		public double LowestShippingCost { get; set; }
		public string SnapshotId { get; set; }
		public string OfferLink { get; set; }
		public int Purchased { get; set; }
		public int Visits { get; set; }
		public int Sold { get; set; }
		public bool ShowUnitPrice { get; set; }
		public string GuaranteeSnapshotId { get; set; }
		public string ComplaintPolicySnapshotId { get; set; }
		public string ReturnPolicySnapshotId { get; set; }
		public string ShippingTariffSnapshotId { get; set; }
		public UnitPricesType UnitPriceType { get; set; }
		public DateTime ModificationDate { get; set; }
		public List<Components.Error> Errors { get; set; }
		public string BlockReason { get; set; }
	}
}
