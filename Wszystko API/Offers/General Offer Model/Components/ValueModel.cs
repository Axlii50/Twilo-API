using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wszystko_API.Offers.General_Offer_Model.Components
{
	public class ValueModel
	{
		public string? StringValue { get; set; }
		public double? NumberValue { get; set; }
		public int? IntegerValue { get; set; }
		public int[]? IntegerArray { get; set; }
		public ValueRange? RangeObject { get; set; }
	}
}
