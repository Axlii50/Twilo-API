using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wszystko_API.Orders.Components
{
	public class BuyerAddress
	{
		public string Street { get; set; }
		public string BuildingNumber { get; set; }
		public string FlatNumber { get; set; }
		public string PostCode { get; set; }
		public string City {  get; set; }
	}
}
