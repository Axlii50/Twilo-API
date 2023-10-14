using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wszystko_API.Product
{
	public enum LeadTimeType
	{
		Natychmiast,
		[Display(Name = "12 godzin")]
		Godziny,
		[Display(Name = "1 dzień")]
		Dzień,
		_2_dni,
		_3_dni,
		_4_dni,
		_5_dni,
		_6_dni,
		_7_dni,
		_8_15_dni,
		_16_21_dni,
		Powyżej_21_dni
	}

	public static class LeadTimeTypeExtension
	{
		public static string LeadTimeToString(this LeadTimeType leadTimeType)
		{
			switch (leadTimeType)
			{
				case LeadTimeType.Natychmiast:
					return "Natychmiast";
				case LeadTimeType.Godziny:
					return "Godziny";
				case LeadTimeType.Dzień:
					return "Dzień";
				case LeadTimeType._2_dni:
					return "2 dni";
				case LeadTimeType._3_dni:
					return "3 dni";
				case LeadTimeType._4_dni:
					return "4 dni";
				case LeadTimeType._5_dni:
					return "5 dni";
				case LeadTimeType._6_dni:
					return "6 dni";
				case LeadTimeType._7_dni:
					return "7 dni";
				case LeadTimeType._8_15_dni:
					return "8 - 15 dni";
				case LeadTimeType._16_21_dni:
					return "16 - 21 dni";
				case LeadTimeType.Powyżej_21_dni:
					return "Powyżej 21 dni";
				default:
					return string.Empty;
			}
		}
	}
}
