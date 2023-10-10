using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wszystko_API.Product
{
	public class ParameterKit
	{
		//[Required]
		public int Id { get; set; }
		//[Required]
		// typ value do poprawy
		public string value { get; set; }
	}
}
