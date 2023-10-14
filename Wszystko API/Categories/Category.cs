using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wszystko_API.Categories.Interface;

namespace Wszystko_API.Categories
{
	public class Category : ICategory
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int ParentId { get; set; }
		public bool HasSubcategories { get; set; }
		public string UrlPart { get; set; }
	}
}
