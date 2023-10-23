using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wszystko_API.Shipping.Components;

namespace Wszystko_API.Shipping
{
    public class ShippingModel
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public ShippingType Shipping {  get; set; }
		public Uri logoUri { get; set; }
		public ShippingMethodOptionAvailability AvailableShippingMethodOptions { get; set; }
		public int MinShippingDays { get; set; }
		public int MaxShippingDays { get; set; }
		public DateOnly EarliestEstimatedShippingDate { get; set; }
		public DateOnly LatestEstimatedShippingDate { get; set; }
	}
}
