using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wszystko_API.Orders.Components
{
	public class BuyerModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string CompanyName { get; set; }
		public string Nip { get; set; }
		public BuyerAddress Address { get; set; }
		// CONSIDER ANOTHER CLASS
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public bool IsOver18 {  get; set; }
	}
}
