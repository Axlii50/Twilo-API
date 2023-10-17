using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wszystko_API.Orders.Components
{
	public class ShippingMethod
	{
		public string ShippingMethodId { get; set; }
		public double Number {  get; set; }
		public int NumberOfPackages { get; set; }
	}
}
