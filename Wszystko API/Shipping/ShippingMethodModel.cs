using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wszystko_API.Shipping
{
	public class ShippingMethodModel
	{
		public string ShippingMethodId { get; set; }
		public ShippingMethodOption shippingMethodOption { get; set; }
	}
}
