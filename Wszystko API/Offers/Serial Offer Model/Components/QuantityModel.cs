using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wszystko_API.Global_Components;
using Wszystko_API.Offers.Common_Components;

namespace Wszystko_API.Offers.Serial_Offer_Model.Components
{
	public class QuantityModel
	{
		public int Value { get; set; }
		public SimpleChangeType Change { get; set; }
	}
}
