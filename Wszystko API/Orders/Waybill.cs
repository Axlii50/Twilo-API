using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wszystko_API.Orders
{
	public class Waybill
	{
		public string TrackingNumber { get; set; }
		public string ShippingMethodId { get; set; }
		public Uri LinkToTrackPackage { get; set; }
	}
}
