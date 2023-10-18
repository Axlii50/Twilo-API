using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wszystko_API.Shipping.Components
{
    public class ShippingMethodModel
    {
        public string ShippingMethodId { get; set; }
        public ShippingMethodOptionAvailability shippingMethodOption { get; set; }
    }
}
