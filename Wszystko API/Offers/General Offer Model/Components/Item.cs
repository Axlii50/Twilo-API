using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wszystko_API.Product
{
	public class Item
	{
		[JsonProperty("type")]
		public string ContentType { get; set; }
		[JsonProperty("value")]
		public string Value { get; set; }
	}
}
