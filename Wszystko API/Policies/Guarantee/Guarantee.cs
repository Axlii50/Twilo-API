using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wszystko_API.Policies.Guarantee.Components;

namespace Wszystko_API.Policies.Guarantee
{
	public class Guarantee
	{
		public string AdditionalInformation { get; set; }
		public string Id { get; set; }
		public string Name { get; set; }
		public GuaranteeData GuaranteeDataDetails { get; set; }
	}
}
