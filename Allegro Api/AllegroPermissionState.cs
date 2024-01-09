using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api
{
	public enum AllegroPermissionState
	{
		//Offers
		allegro_api_sale_offers_read = 1,
		allegro_api_sale_offers_write = 2,
		allegro_api_sale_settings_read = 4,
		allegro_api_sale_settings_write = 8,
		allegro_api_ads = 16,
		allegro_api_campaigns = 32,

		//Order/Service
		allegro_api_orders_read = 64,
		allegro_api_orders_write = 128,
		allegro_api_ratings = 256,
		allegro_api_disputes = 512,
		allegro_api_billing_read = 1024,
		allegro_api_payments_read = 2048,
		allegro_api_payments_write = 4096,
		allegro_api_profile_read = 8192,
		allegro_api_profile_write = 16384,

		//Licitatons
		allegro_api_bids = 32768,
		allegro_api_messaging = 65536,
		allegro_api_fulfillment_read = 131072,
		allegro_api_fulfillment_write = 262144,

        //shipment 
        allegro_api_shipments_read = 524288,
        allegro_api_shipments_write = 1048576
    }

	public static class AllegroPermissionStateClass
	{
		public static List<string> ConvertToString(this AllegroPermissionState per)
		{
			List<string> result = new List<string>();

			for (int i = 1; i <= 262144; i*=2)
				if(per.HasFlag((AllegroPermissionState)i))
					result.Add(((AllegroPermissionState)i).ToString().Replace('_',':'));
			
			return result;
		}
	}
}
