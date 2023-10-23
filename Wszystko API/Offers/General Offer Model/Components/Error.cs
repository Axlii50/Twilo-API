using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wszystko_API.Offers.Common_Components;

namespace Wszystko_API.Offers.General_Offer_Model.Components
{
	public class Error
	{
		public ControlNameType ControlName { get; set; }
		public string Message { get; set; }
	}
}
