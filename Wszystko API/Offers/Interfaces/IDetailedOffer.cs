using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wszystko_API.Offers.Common_Components;

namespace Wszystko_API.Offers.Interfaces
{
    public interface IDetailedOffer: IBasicOffer
    {
        public int Id { get; set; }
        public string SellerId { get; set; }
        public string ShopUrlPart { get; set; }
        //[ReadOnly]
        public DateTime CreationDate { get; set; }
        //[ReadOnly]
        public double LowestShippingCost { get; set; }
        //[ReadOnly]
        public string SnapshotId { get; set; }
        public string OfferLink { get; set; }
        public int Purchased { get; set; }
        public int Visits { get; set; }
        public int Sold { get; set; }
        public bool ShowUnitPrice { get; set; }
        public bool AdultOnly { get; set; }
        public string GuaranteeSnapshotId { get; set; }
        public string ComplaintPolicySnapshotId { get; set; }
        public string ReturnPolicySnapshotId { get; set; }
        public string ShippingTariffSnapshotId { get; set; }
        public UnitPricesType UnitPriceType { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
