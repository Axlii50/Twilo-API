using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Wszystko_API.Global_Components
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

	public static class VatRateTypeExtension
	{
		public static string VatRateToString(this VatRateType vatRateType)
		{
			switch(vatRateType)
			{
				case VatRateType.zw:
					return "zw.";
				case VatRateType.zero:
					return "0%";
				case VatRateType.five:
					return "5%";
				case VatRateType.eight:
					return "8%";
				case VatRateType.twenty_three:
					return "23%";
				default:
					return string.Empty;
			}
		}
	}
}
