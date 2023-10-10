using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wszystko_API.Product
{
	public enum VatRateType
	{
		[Display(Name = "zw.")]
		zw,
		[Display(Name = "0%")]
		zero,
		[Display(Name = "5%")]
		five,
		[Display(Name = "8%")]
		eight,
		[Display(Name = "23%")]
		twenty_three
	}
}
