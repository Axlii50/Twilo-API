using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wszystko_API.Orders.Components;
using Wszystko_API.Product;

namespace Wszystko_API.Orders
{
	public class SimpleOrderModel
	{
		public int Id { get; set; }
		public DateTime CreationDate { get; set; }
		public double TotalCost { get; set; }
		public OrderStatusType StatusType { get; set; }
		public string Message { get; set; }
		public BuyerModel Buyer { get; set; }
		public bool VatInvoice { get; set; }
		public string[] TrackingNumbers {  get; set; }
		public PaymentStatusType PaymentStatus { get; set; }
		public ShippingModel Shipping { get; set; }
		public OrderItem[] Items { get; set; }
		public int TotalElements {  get; set; }
	}
}
