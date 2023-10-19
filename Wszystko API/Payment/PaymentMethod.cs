using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wszystko_API.Payment.Components;

namespace Wszystko_API.Payment
{
	public class PaymentMethod
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public Uri logoUrl { get; set; }
		public PaymentType Type { get; set; }
		public double MinimumTransactionAmount { get; set; }
		public double MaximumTransactionAmount { get; set; }
		public ShippingMethodType[] UnsupportedShippingMethodTypes { get; set; }
	}
}
