using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wszystko_API.Orders
{
	public class OrdersListModel
	{
		public SimpleOrderModel[] simpleOrderModels { get; set; }
		public int TotalElements { get; set; }
	}
}
