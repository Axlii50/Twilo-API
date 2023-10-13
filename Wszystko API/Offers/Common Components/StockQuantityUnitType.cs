using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wszystko_API.Product
{
	public enum StockQuantityUnitType
	{
		sztuk,
		[Display(Name = "kompletów")]
		kompletow,
		par,
		opakowan,
		paczek
	}

	public static class StockQuantityUnitTypeExtension
	{
		public static string StockQuantityUnitTypeToString(this StockQuantityUnitType stockQuantityUnitType)
		{
			switch(stockQuantityUnitType)
			{
				case StockQuantityUnitType.sztuk:
					return "sztuk";
				case StockQuantityUnitType.kompletow:
					return "kompletow";
				case StockQuantityUnitType.par:
					return "par";
				case StockQuantityUnitType.opakowan:
					return "opakowan";
				case StockQuantityUnitType.paczek:
					return "paczek";
				default:
					return string.Empty;
			}
		}
	}
}
