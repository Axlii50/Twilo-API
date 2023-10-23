using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wszystko_API.Shipping.Components
{
	public class ShippingMethodOptionAgreementModel
	{
		public string ShippingMethodId { get; set; }
		public bool AgreementForElectronicDeliveryRequired { get; set; }
	}
}
