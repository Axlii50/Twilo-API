using Allegro_Api.Models;
using System;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;

using Newtonsoft;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using Allegro_Api.Models.Offer;
using System.Net.Http.Json;
using Allegro_Api.Models.Product;
using Allegro_Api.Models.category.CategorySuggestions;

namespace Allegro_Api
{
    public class AllegroApi
    {
        //client informations
        private string ClientID = string.Empty;
        private string ClientSecret = string.Empty;

        //Allegro URLs
        private string AllegroAuthURL = "https://allegro.pl/auth/oauth/device";
        private string AllegoTokenURL = "https://allegro.pl/auth/oauth/token";
        private string AllegroBaseURL = "https://api.allegro.pl";
        private string environment = "allegro.pl";

        private string DeviceCode = string.Empty;

        //dodać automatyczne przedłuzanie 
        //acces token jest ważny przez 12 h
        private string AccessToken = string.Empty;
        private int TokenExpiresIn = -1;
        //RefreshToken jest ważny natomiast przez 3miesiace
        private string RefreshToken = string.Empty;

        //przedłużanie accesstokena https://developer.allegro.pl/tutorials/uwierzytelnianie-i-autoryzacja-zlq9e75GdIR#przedluzenie-waznosci-tokena

        public AllegroApi(string ClientID, string ClientSecret)
        {
            this.ClientID = ClientID;
            this.ClientSecret = ClientSecret;
        }

        //TODO Create offer based on product
        //TODO EDIT AN OFFER
        //TODO BATCH OFFER PUBLISH/UNPUBLISH

        //Get verification link for user based on their client id and client secret
        #region AUTH
        /// <summary>
        /// Class automaticly saves Device code for later use
        /// </summary>
        /// <returns></returns>
        public async Task<VerificationULRModel> Authenticate()
        {
            HttpClient client = new HttpClient();
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

            HttpResponseMessage odp = await client.PostAsync(AllegroAuthURL, content);

            VerificationULRModel model = JsonConvert.DeserializeObject<VerificationULRModel>(odp.Content.ReadAsStringAsync().Result);

            DeviceCode = model.device_code;
            return model;
        }

        /// <summary>
        /// check if user granted access 
        /// </summary>
        /// <param name="permissions"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> CheckForAccessToken(AllegroPermissionState permissions)
        {
            if (DeviceCode == string.Empty) throw new Exception("Device code is empty please use  Authenticate() before this");

            //all permission as strings 
            string[] AllegroStringPermissions = permissions.ConvertToString().ToArray();

            HttpClient client = new HttpClient();
            //create normal string for client id and client secret 
            string formatedstring = $"{ClientID}:{ClientSecret}";

            byte[] bytes = Encoding.UTF8.GetBytes(formatedstring);
            //create auth string containg normal in base 64
            string AuthString = "Basic " + Convert.ToBase64String(bytes);

            //add neccessary headers 
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", AuthString);

            //prepare all params for content
            var values = new Dictionary<string, string>
            {
                 { "grant_type", "urn:ietf:params:oauth:grant-type:device_code" },
                 { "device_code", DeviceCode }
            };
            var content = new FormUrlEncodedContent(values);

            //send post request to AllegroTokenURL
            HttpResponseMessage odp = await client.PostAsync(AllegoTokenURL, content);

            //deserialize content of response
            AccessTokenModel model = JsonConvert.DeserializeObject<AccessTokenModel>(odp.Content.ReadAsStringAsync().Result);

            //if user authorized access then remove device code and set other variables for later
            if (model.access_token != null)
            {
                DeviceCode = string.Empty;
                AccessToken = model.access_token;
                RefreshToken = model.refresh_token;
                TokenExpiresIn = model.expires_in;
                return true;
            }

            return false;
        }
        #endregion

        #region offers
        /// <summary>
        /// to juz jest zapytanie strikte do api allegro typu REST api
        /// </summary>
        /// <returns></returns>
        public async Task<OffersModel> GetAllOffers()
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("pl-PL"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json"));

            HttpResponseMessage odp = await client.GetAsync(AllegroBaseURL + "/sale/offers");

            OffersModel model = JsonConvert.DeserializeObject<OffersModel>(odp.Content.ReadAsStringAsync().Result);

            //System.Diagnostics.Debug.WriteLine("tet:    "+odp.Content.ReadAsStringAsync().Result);

            return model;
        }

        /// <summary>
        /// funkcja do zmiany ceny konkretnej ofert (offerid)
        /// </summary>
        /// <param name="offerid"></param>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> ChangePriceOfOffer(string offerid, string amount, string currency = "PLN")
        {
            HttpClient client = new HttpClient();
            string commandID = Guid.NewGuid().ToString();

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("pl-PL"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json"));


            var jsonobject = new
            {
                id = commandID,
                input = new
                {
                    buyNowPrice = new Dictionary<string, string> {
                            { "amount", amount },
                            { "currency", currency }
                    }
                }
            };

            var jsonstring = JsonConvert.SerializeObject(jsonobject);
            //System.Diagnostics.Debug.WriteLine(jsonstring);
            //var buffer = System.Text.Encoding.UTF8.GetBytes(jsonstring);
            //var byteContent = new ByteArrayContent(buffer);
            //byteContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json");

            var content = new StringContent(jsonstring, Encoding.UTF8, "application/vnd.allegro.public.v1+json");

            string URL = $"https://api.{environment}/offers/{offerid}/change-price-commands/{commandID}";

            var response = await client.PutAsync(URL, content);

            //do przetestowania ze wzgledu ze na zwykłym koncie wywala forbidden ze brak dostepu do api dla kont nie firmowych
            //
            return response;
        }
        #endregion

        #region niedokonczone
        /// <summary>
        /// zwraca wszystkie dane na temat oferty not implemneted
        /// </summary>
        /// <param name="offerId"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetDetailedOffer(string offerId)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("pl-PL"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json"));

            HttpResponseMessage odp = await client.GetAsync(AllegroBaseURL + $"/sale/product-offers/{offerId}");


            //throw new NotImplementedException();

            return odp;
        }

        /// <summary>
        /// zwraca wszystkie dane na temat oferty not implemneted
        /// </summary>
        /// <param name="offerId"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> ProposeProduct(ProductModel product)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("pl-PL"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json"));

            string json = JsonConvert.SerializeObject(product);

            var content = new StringContent(json, Encoding.UTF8, "application/vnd.allegro.public.v1+json");

            HttpResponseMessage odp = await client.PostAsync(AllegroBaseURL + $"/sale/product-proposals", content);

            return odp;
        } 
        #endregion


        ///// <summary>
        ///// only for developmnet purpose 
        ///// </summary>
        ///// <returns></returns>
        //public async Task<HttpResponseMessage> GetAllCategories()
        //{
        //    HttpClient client = new HttpClient();

        //    client.DefaultRequestHeaders.Clear();
        //    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
        //    client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("pl-PL"));
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json"));

        //    HttpResponseMessage odp = await client.GetAsync(AllegroBaseURL + "/sale/categories");

        //    return odp;
        //}

        /// <summary>
        /// get suggestion based on item name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<CategorySuggestion> GetSuggestionOfCategory(string name)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("pl-PL"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json"));

            HttpResponseMessage odp = await client.GetAsync(AllegroBaseURL + $"/sale/matching-categories?name={name}");

            var suggestion = JsonConvert.DeserializeObject<CategorySuggestion>(odp.Content.ReadAsStringAsync().Result);

            return suggestion;
        }
    }
}