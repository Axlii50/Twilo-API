using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wszystko_API.Shipping.Components;

namespace Wszystko_API.Shipping
{
	public class ShippingTariffModel
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string SnapshotId { get; set; }
		public ShippingMethodOptionParametersModel? shippingMethodOptionParametersModel { get; set; }
		public ShippingMethodOptionAgreementModel? shippingMethodOptionAgreementModel { get; set; }
	}
}
