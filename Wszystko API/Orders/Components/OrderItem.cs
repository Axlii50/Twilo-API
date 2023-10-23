using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wszystko_API.Global_Components;
using Wszystko_API.Product;

namespace Wszystko_API.Orders.Components
{
	public class OrderItem
	{
		public int OfferId { get; set; }
		public string SnapshotId { get; set; }
		public string Title { get; set; }
		public int Quantity { get; set; }
		public double Price { get; set; }
		public double Cost {  get; set; }
		public Uri Uri { get; set; }
		public VatRateType VatRate {  get; set; }
		public string Ean {  get; set; }
		public string ProducerCode {  get; set; }
	}
}
