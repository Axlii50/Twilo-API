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
		Dzień
	}
}
