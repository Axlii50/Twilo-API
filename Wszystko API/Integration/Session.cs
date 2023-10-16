using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wszystko_API.Integration
{
	public class Session
	{
		public string Id { get; set; }
		public string UserName { get; set; }
		public string IpAddress { get; set; }
		public DateTime Start { get; set; }
		public DateTime LastAccess { get; set; }
	}
}
