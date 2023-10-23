using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wszystko_API.Policies.Return.Components;
using Wszystko_API.Policies.Common_Components;

namespace Wszystko_API.Policies.Return
{
	public class Return
	{
		public string TruncatedAdditionalInformation { get; set; }
		public string Name { get; set; }
		public string Id { get; set; }
		public int RevocationDays { get; set; }
		public ShippingCostPayer shippingCostPayer { get; set; }
		public PolicyAddress Address { get; set; }
		public string CompanyName { get; set; }
		public Uri fileUri { get; set; }
		public string? ReturnRestriction { get; set; }
	}
}
