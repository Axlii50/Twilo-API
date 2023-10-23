using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wszystko_API.Categories.Components;

namespace Wszystko_API.Categories
{
	public class CategoryDetails
	{
		public bool Required { get; set; }
		public double Id { get; set; }
		public string Label { get; set; }
		public bool Main { get; set; }
		public bool Multivariant { get; set; }
		public int FilterOrder { get; set; }
		public VarType Type { get; set; }
		public double? MinLength { get; set; }
		public double? MaxLength { get; set; }
		public string? Pattern { get; set; }
		public string? Unit { get; set; }
		public double? Min { get; set; }
		public double? Max { get; set; }
		public bool? IsRange { get; set; }
		public double? Precision { get; set; }
		public bool? IsMultiChoice { get; set; }
		public ValuesData[]? Values { get; set; }
		public int? SelectionLimit { get; set; }
	}
}
