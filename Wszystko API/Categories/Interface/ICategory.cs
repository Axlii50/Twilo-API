using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wszystko_API.Categories.Interface
{
	public interface ICategory
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int ParentId { get; set; }
		public bool HasSubcategories { get; set; }
	}
}
