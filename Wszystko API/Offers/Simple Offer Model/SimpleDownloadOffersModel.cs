using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wszystko_API.Offers.Common_Components;
using Wszystko_API.Offers.Simple_Offer_Model.Interface;

namespace Wszystko_API.Offers
{
	[Description("Used for downloading offers from https://wszystko.pl/")]
	public class SimpleDownloadOffersModel: IDownloadOffersModel
	{
		//[ReadOnly(true)]
		public int Id { get; set; }
		public string Title { get; set; }
		public Uri MainPhotoUrl { get; set; }
		public OfferStatusType Status { get; set; }
		public double Price { get; set; }
		public int Quantity { get; set; }
		//[Required]
		public int SoldInLastMonth { get; set; }
		//[Required]
		public int VisitsInLastMonth { get; set; }
		public string SnapshotId { get; set; }
		//[ReadOnly(true)]
		public string BlockReason { get; set; }
		public UnitPricesType UnitPrice { get; set; }
		public int userQuantityLimit { get; set; }
	}
}
