using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wszystko_API.Offers.Serial_Offer_Model.Components
{
	public interface IDetailedError
	{
		//[Required]
		//[ReadOnly]
		public DetailedCodeType Code { get; set; }
		//[Required]
		//[ReadOnly]
		//[AllowNull]
		public ResourceType? Resource { get; set; }
	}
}
