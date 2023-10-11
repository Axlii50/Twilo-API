using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Wszystko_API.Auth;
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

		public async Task GetAllOffers()
		{
			using HttpClient client = new HttpClient();

			client.DefaultRequestHeaders.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			HttpResponseMessage odp = await client.GetAsync(WszystkoBaseURL + $"/me/offers?userId={AccessToken}");

			string odpcontent = odp.Content.ReadAsStringAsync().Result;
			System.Console.WriteLine(odpcontent);

		}

        public async Task CreateOffer()
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Clear();

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


		}

        public async Task GetOfferData(int offerId)
        {
			using HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Clear();


            HttpResponseMessage odp = await client.GetAsync(WszystkoBaseURL + $"/me/offers/{offerId}");
			System.Diagnostics.Debug.WriteLine(odp.Content.ReadAsStringAsync().Result);
		}

		#endregion
	}
}