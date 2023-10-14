using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wszystko_API.Orders.Components
{
	public enum OrderStatusType
	{
		[Display(Name = "new")]
		new_,
		inProcess,
		shipped,
		completed,
		cancelled
	}
}
