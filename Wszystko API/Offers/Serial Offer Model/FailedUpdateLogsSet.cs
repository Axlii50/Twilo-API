using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wszystko_API.Offers.Serial_Offer_Model.Components
{
	public class FailedUpdateLogsSet
	{
		//[ReadOnly]
		public int Id { get; set; }
		//[ReadOnly]
		public GeneralCodeType Code { get; set; }
		//[ReadOnly]
		public string Message { get; set; }
		//[AllowNull
		//public IDetailedError? DetailedError { get; set; }
	}
}
