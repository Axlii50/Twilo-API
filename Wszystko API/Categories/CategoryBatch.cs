using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wszystko_API.Categories
{
	[Description("A fragment of category tree, just one batch, due to restriction of up to 100 elements in one batch")]
	public class CategoryBatch
	{
		public Category[] Categories {  get; set; }
		public int NumberOfPages { get; set; }
	}
}
