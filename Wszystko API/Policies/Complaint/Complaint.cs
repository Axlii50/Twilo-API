using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wszystko_API.Policies.Common_Components;

namespace Wszystko_API.Policies.Complaint
{
	public class Complaint
	{
		public string Id { get; set; }
		public int ComplaintYears { get; set; }
		public bool IsForBusiness { get; set; }
		public PolicyAddress Address { get; set; }
	}
}
