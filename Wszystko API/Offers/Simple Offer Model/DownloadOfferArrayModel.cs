using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wszystko_API.Offers.Simple_Offer_Model.Interface;
using Wszystko_API.Offers.Simple_Offer_Model.JsonConverter;

namespace Wszystko_API.Offers
{
	public class DownloadOfferArrayModel
	{
		[JsonProperty("records")]
		public IDownloadOffersModel[] Offers { get; set; }
		[JsonProperty("totalElements")]
		public int NumberOfOffers { get; set; }
	}
}
