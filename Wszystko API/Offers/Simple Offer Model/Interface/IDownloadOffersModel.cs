using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wszystko_API.Offers.Common_Components;

namespace Wszystko_API.Offers.Simple_Offer_Model.Interface
{
	public interface IDownloadOffersModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public OfferStatusType Status { get; set; }
		public double Price { get; set; }
		public int Quantity { get; set; }
		public string SnapshotId { get; set; }
		public UnitPricesType UnitPrice { get; set; }
		public int userQuantityLimit { get; set; }
	}
}
