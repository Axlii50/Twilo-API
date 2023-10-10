using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wszystko_API.Offers
{
	[Description("Used for downloading offers from https://wszystko.pl/")]
	public class SimpleOfferModel
	{
		//[ReadOnly(true)]
		public int Id { get; set; }
		public string Title { get; set; }
		public string MainPhotoUrl { get; set; }
		public StatusType status { get; set; }
		public double Price { get; set; }
		public int Quantity { get; set; }
		//[Required]
		public int SoldInLastMonth { get; set; }
		//[Required]
		public int VisitsInLastMonth { get; set; }
		public string SnapshotId { get; set; }
		//[ReadOnly(true)]
		public string BlockReason { get; set; }
		public UnitPriceType UnitPrice { get; set; }
		public int userQuantityLimit { get; set; }
	}
}
