using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wszystko_API.Shipping.Components
{
	public class ShippingMethodOptionsParameters
	{
		public AdvancePaymentParameters advancePaymentParameters { get; set; }
		public CashOnDeliveryParameters cashOnDeliveryParameters { get; set; }
	}
}
