﻿using System;
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
		opakow
	}
}
