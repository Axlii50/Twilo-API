using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wszystko_API.Orders.Components;

namespace Wszystko_API.Orders
{
	public class OrderStatus
	{
		public string Id {  get; set; }
		public OrderStatusType Status { get; set; }
		public PaymentStatusType PaymentStatus { get; set; }
	}
}
