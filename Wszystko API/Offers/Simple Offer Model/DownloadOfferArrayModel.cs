using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wszystko_API.Offers.Simple_Offer_Model.Interface;

namespace Wszystko_API.Offers
{
	public class DownloadOfferArrayModel
	{
		public IDownloadOffersModel[] Offers { get; set; }
		public int NumberOfOffers { get; set; }
	}
}
