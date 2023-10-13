using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using Wszystko_API.Auth;
using Wszystko_API.Offers;
using Wszystko_API.Offers.General_Offer_Model;
using Wszystko_API.Offers.Serial_Offer_Model;
using Wszystko_API.Offers.Serial_Offer_Model.Components;
using Wszystko_API.Orders;
using Wszystko_API.Product;

namespace Wszystko_API
{
    public class WszystkoApi
    {
        //client informations
        private string ClientID = string.Empty;
        private string ClientSecret = string.Empty;

        //Wszystko URLs
        private string WszystkoBaseURL = $"https://wszystko.pl/api";

        private string DeviceCode = string.Empty;

        //acces token jest ważny przez 12 h
        private string AccessToken = string.Empty;
        private int TokenExpiresIn = -1;
        //RefreshToken jest ważny natomiast przez 3miesiace
        public string RefreshToken = string.Empty;

        private System.Timers.Timer timer = new System.Timers.Timer();

        public delegate void RefreshTokenDelgate();
        /// <summary>
        /// event occures when token is refreshed
        /// </summary>
        public event RefreshTokenDelgate RefreshTokenEvent;


        public WszystkoApi(string ClientID, string ClientSecret, RefreshTokenDelgate refreshtokenevent)
        {


        }

        #region AUTH
        /// <summary>
        /// Class automaticly saves Device code for later use
        /// </summary>
        /// <returns></returns>
        public async Task<BaseAuthModel> GenerateDeviceCode()
        {
            using HttpClient client = new HttpClient();
            //create normal string for client id and client secret
            string formatedstring = $"{ClientID}:{ClientSecret}";

            byte[] bytes = Encoding.UTF8.GetBytes(formatedstring);
            //create auth string containg normal in base 64
            string AuthString = "Basic " + Convert.ToBase64String(bytes);

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", AuthString);
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("pl-PL"));

            //tutaj zwieramy params oraz header do typu aplikacji ponieważ content-type header jest typem contentu nie requesta
            var content = new StringContent($"client_id={ClientID}", Encoding.UTF8, "application/x-www-form-urlencoded");

            HttpResponseMessage odp = await client.GetAsync(WszystkoBaseURL + "/integration/register");

            Console.WriteLine(odp.Content.ReadAsStringAsync().Result);

            BaseAuthModel model = JsonConvert.DeserializeObject<BaseAuthModel>(odp.Content.ReadAsStringAsync().Result);

            DeviceCode = model.deviceCode;
            return model;
        }

        /// <summary>
        /// check if user granted access
        /// </summary>
        /// <param name="permissions"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> CheckForAccessToken()
        {
            if (DeviceCode == string.Empty) throw new Exception("Device code is empty please use  Authenticate() before this");

            using HttpClient client = new HttpClient();
            //create normal string for client id and client secret

            //send post request to AllegroTokenURL
            HttpResponseMessage odp = await client.GetAsync(WszystkoBaseURL + "/integration/token?deviceCode=" + this.DeviceCode);

            if (odp.StatusCode == System.Net.HttpStatusCode.BadRequest)
                return false;

            //deserialize content of response
            AccessTokenModel model = JsonConvert.DeserializeObject<AccessTokenModel>(odp.Content.ReadAsStringAsync().Result);

            //if user authorized access then remove device code and set other variables for later
            if (model.AccessToken != null)
            {
                DeviceCode = string.Empty;
                AccessToken = model.AccessToken;
                RefreshToken = model.refreshToken;
                TokenExpiresIn = model.expiresIn;

                Console.WriteLine("Dlugosc tokenu: " + (TokenExpiresIn / 2) * 1000);

                this.timer.Interval = (TokenExpiresIn / 2) * 1000;
                this.timer.Start();

                return true;
            }

            return false;
        }

        //public async Task RefreshAccesToken()
        //{
        //    using HttpClient client = new HttpClient();
        //    string formatedstring = $"{ClientID}:{ClientSecret}";

        //    byte[] bytes = Encoding.UTF8.GetBytes(formatedstring);
        //    //create auth string containg normal in base 64
        //    string AuthString = "Basic " + Convert.ToBase64String(bytes);

        //    //add neccessary headers
        //    client.DefaultRequestHeaders.Clear();
        //    client.DefaultRequestHeaders.Add("Authorization", AuthString);

        //    client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("pl-PL"));
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json"));

        //    var values = new Dictionary<string, string>
        //    {
        //         { "grant_type", "refresh_token" },
        //         { "refresh_token", RefreshToken }
        //    };
        //    var content = new FormUrlEncodedContent(values);

        //    HttpResponseMessage odp = await client.GetAsync(WszystkoBaseURL);

        //    AccessTokenModel model = JsonConvert.DeserializeObject<AccessTokenModel>(odp.Content.ReadAsStringAsync().Result);
        //    if (!odp.IsSuccessStatusCode) return;

        //    //if user authorized access then remove device code and set other variables for later
        //    AccessToken = model.access_token;
        //    RefreshToken = model.refresh_token;

        //    RefreshTokenEvent?.Invoke();
        //    try
        //    {
        //        this.timer.Interval = (TokenExpiresIn / 2) * 1000;
        //    }
        //    catch (ArgumentException)
        //    {
        //        this.timer.Interval = 21599000;
        //    }

        //    this.timer.Start();
        //}

        public async void GetSessions()
        {
            using HttpClient client = new HttpClient();

            HttpResponseMessage odp = await client.GetAsync(WszystkoBaseURL + "/me/integrations");

            System.Diagnostics.Debug.WriteLine(odp.StatusCode);
            System.Diagnostics.Debug.WriteLine(odp.Content.ReadAsStringAsync().Result);

        }
		#endregion

		#region Offers

		public async Task<SimpleOfferList> GetAllOffers()
		{
			using HttpClient client = new HttpClient();

			client.DefaultRequestHeaders.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			HttpResponseMessage odp = await client.GetAsync(WszystkoBaseURL + $"/me/offers?userId={AccessToken}");

			string odpcontent = odp.Content.ReadAsStringAsync().Result;
			System.Console.WriteLine(odpcontent);

            SimpleOfferList simpleOfferList = JsonConvert.DeserializeObject<SimpleOfferList>(odpcontent);

            return simpleOfferList;
		}

        public async Task<RequestAddProductOffer> CreateOffer()
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            RequestAddProductOffer addProductOffer = new RequestAddProductOffer()
            {
                Title = "aaaaaaa",
                //Price = 100,
                //CategoryId = 1,
                //Gallery = new string[] { "http://example.com" },
                //VatRate = VatRateType.zw,
                //Parameters = new List<ParameterKit>(),
                //Descriptions = new List<Description>(),
                //guaranteeId = "",
                //complaintPolicyId = "",
                //returnPolicyId = "",
                //shippingTariffId = "",
                //LeadTime = LeadTimeType.Natychmiast,
                //StockQuantityUnit = StockQuantityUnitType.sztuk,
                OfferStatus = OfferStatusType.blocked,
                //UserQuantityLimit = 100,
                IsDraft = true,
                //StockQuantity = 5,
                //ShowUnitPrice = false
            };

            string json = JsonConvert.SerializeObject(addProductOffer);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

			HttpResponseMessage odp = null;
			//try
			//{
				odp = await client.PostAsync(WszystkoBaseURL + $"/me/offers", content);
                System.Diagnostics.Debug.WriteLine(odp.Content.ReadAsStringAsync().Result);
            //}
            //catch (HttpRequestException)
            //{
            //  System.Diagnostics.Debug.WriteLine("HttpRequestException");
            //}
            //catch (TaskCanceledException)
            //{
            //	System.Diagnostics.Debug.WriteLine("TaskCanceledException");
            //}


            string responseBody = odp.Content.ReadAsStringAsync().Result;
            RequestAddProductOffer requestAddProductOffer = JsonConvert.DeserializeObject<RequestAddProductOffer>(responseBody);

            return requestAddProductOffer;
		}

        public async Task<DownloadableOfferModel> GetOfferData(string userId)
        {
			using HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			HttpResponseMessage odp = await client.GetAsync(WszystkoBaseURL + $"/me/offers/{userId}");
			System.Diagnostics.Debug.WriteLine(odp.Content.ReadAsStringAsync().Result);

            string responseBody = odp.Content.ReadAsStringAsync().Result;
			DownloadableOfferModel offerData = JsonConvert.DeserializeObject<DownloadableOfferModel>(responseBody);

            return offerData;
		}

        public async Task<HttpContent> UpdateOfferData(int offerId, UpdateOfferModel offerUpdateContent)
		{
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Clear();

			var json = JsonConvert.SerializeObject(offerUpdateContent);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			HttpResponseMessage odp = await client.PutAsync(WszystkoBaseURL + $"/me/offers/{offerId}", content);

            return odp.Content;
        }

        public async Task<HttpContent> DeleteOffer(int offerId)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			HttpResponseMessage odp = await client.DeleteAsync(WszystkoBaseURL + $"/me/offers/{offerId}");
			System.Diagnostics.Debug.WriteLine(odp.Content.ReadAsStringAsync().Result);

			return odp.Content;
        }

        // case-study by GPT:
        // W przypadku braku dostarczenia ResourceIntegerId w żądaniu, operacja może być stosowana do wszystkich dostępnych ofert, które nie są zablokowane, zamiast do jednego określonego zasobu. Proszę dokładnie przemyśleć, czy i jakie ResourceIntegerId chcesz dostarczyć w zapytaniu, zgodnie z wymaganiami Twojej aplikacji i zrozumieniem, co dokładnie chcesz osiągnąć za pomocą tej operacji
        public async Task<FailedUpdateLogsSet[]> MassUpdateOffers(int[]? relevantOfferIds, SerialOfferChangesSet serialOfferModel)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Clear();

            var json = JsonConvert.SerializeObject(serialOfferModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage odp = null;
			switch (relevantOfferIds)
            {
                case not null:
					// Create a query string by joining the values
					string queryString = string.Join("&", relevantOfferIds.Select(id => $"resourceIntegerId={id}"));

                    odp = await client.PostAsync(WszystkoBaseURL + $"/me/update-offers?{queryString}", content);
                    break;
                case null:
					odp = await client.PostAsync(WszystkoBaseURL + $"/me/update-offers", content);
					break;
			}

            string responseBody = odp.Content.ReadAsStringAsync().Result;
            FailedUpdateLogsSet[] errors = JsonConvert.DeserializeObject<FailedUpdateLogsSet[]>(responseBody);

            return errors;
		}
		#endregion

		#region Orders

        public async Task<SimpleOrderModel[]> GetAllOrders()
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			HttpResponseMessage odp = await client.GetAsync(WszystkoBaseURL + $"/me/sales");
            string responseBody = odp.Content.ReadAsStringAsync().Result;

            SimpleOrderModel[] orders = JsonConvert.DeserializeObject<SimpleOrderModel[]>(responseBody);

            return orders;
		}

        public async Task<SimpleOrderModel> GetOrderWithId(string orderId)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage odp = await client.GetAsync(WszystkoBaseURL + $" /me/sales/{orderId}");

            string responseBody = odp.Content.ReadAsStringAsync().Result;
            SimpleOrderModel order = JsonConvert.DeserializeObject<SimpleOrderModel>(responseBody);

            return order;
        }

        public async Task<HttpContent> UpdateOrderStatus(UpdateOrderStatusModel updateOrderStatusModel)
        {
			using HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Clear();

            string json = JsonConvert.SerializeObject(updateOrderStatusModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage odp = await client.PutAsync(WszystkoBaseURL + $"/me/sales/updateStatus", content);

            return odp.Content;
		}

        public async Task<Waybill[]> GetWaybillsAddedToOrder(string orderId)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage odp = await client.GetAsync(WszystkoBaseURL + $"/me/sales/{orderId}/trackingNumbers");
			string responseBody = odp.Content.ReadAsStringAsync().Result;

			Waybill[] waybills = JsonConvert.DeserializeObject<Waybill[]>(responseBody);

            return waybills;
        }

        public async Task<HttpContent> UpdateOrderWithWaybills(string orderId, Waybill[] waybills)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Clear();

            string json = JsonConvert.SerializeObject(waybills);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage odp = await client.PutAsync(WszystkoBaseURL + $"/me/sales/{orderId}/trackingNumbers", content);

            return odp.Content;
        }

        public async Task<OrderStatus[]> GetOrdersStatus(string[] orderId)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string json = JsonConvert.SerializeObject(orderId);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage odp = await client.PostAsync(WszystkoBaseURL + $"/me/sales/retrieve-statuses", content);

            string responseBody = odp.Content.ReadAsStringAsync().Result;
            OrderStatus[] ordersStatus = JsonConvert.DeserializeObject<OrderStatus[]>(responseBody);

            return ordersStatus;
        }

		#endregion
	}
}