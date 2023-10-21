using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wszystko_API.Offers.General_Offer_Model.Components;

namespace Wszystko_API.Offers.Common_Components
{
	public class ParameterKit
	{
		//[Required]
		[JsonProperty("id")]
		public int Id { get; set; }
		//[Required]
		// typ value do poprawy
		[JsonProperty("value")]
		public int Value { get; set; }
	}
}
