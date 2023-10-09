using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wszystko_API.Offers
{
	internal class SimpleOfferModel
	{
		// readonly
		public int Id { get; set; }
		public string Title { get; set; }
		public string MainPhotoUrl { get; set; }
		public StatusType status { get; set; }
		public double Price { get; set; }
		public int Quantity { get; set; }
		public int SoldInLastMonth { get; set; }
		public int VisitsInLastMonth { get; set; }
		public string SnapshotId { get; set; }
		// readonly
		public string BlockReason { get; set; }
		public UnitPrice UnitPrice { get; set; }
		public int userQuantityLimit { get; set; }

	}
}
