using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wszystko_API.Policies.Guarantee.Components
{
	public class GuaranteeData
	{
		public string Id { get; set; }
		public GuaranteeProviderType ProviderType { get; set; }
		public bool IsLifetime { get; set; }
		public int? ValidityMonths { get; set; }
	}
}
