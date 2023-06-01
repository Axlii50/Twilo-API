using Allegro_Api.Models;
using System;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;
using System.Drawing;
using Newtonsoft;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using Allegro_Api.Models.Offer;
using System.Net.Http.Json;
using Allegro_Api.Models.Product;
using Allegro_Api.Models.category.CategorySuggestions;
using System.Xml.Linq;
using Allegro_Api.Models.category.Parameters;
using System.Drawing.Imaging;
using System.Net;
using Image = Allegro_Api.Models.Image;
using Allegro_Api.Models.Offer.offerComponents.publications;
using Allegro_Api.Models.Product.ProductComponents;
using Allegro_Api.Models.Delivery;
using Allegro_Api.Models.Offer.offerComponents;
using System.ComponentModel.Design;

namespace Allegro_Api
{

    //typy okładek oraz id
    //twarda 75_2
    //twarda z obwolutą 75_4
    //miekka 75_1
    //miekka ze skrzydełkami 75_306417
    //miekka z obwolutą 75_3
    //zintegrowana 75_5


    //TODO po doprowadzeniu do funkcjonalnosci kolejnosc rzeczy do zrobienia
    //1 restrukturyzacja klas 

    public class AllegroApi
    {
        //client informations
        private string ClientID = string.Empty;
        private string ClientSecret = string.Empty;

        //Allegro URLs


        private string AllegroAuthURL = "https://allegro.pl/auth/oauth/device";
        private string AllegoTokenURL = "https://allegro.pl/auth/oauth/token";
        private static string environment = "allegro.pl";

        //private string AllegroAuthURL = " https://allegro.pl.allegrosandbox.pl/auth/oauth/device";
        //private string AllegoTokenURL = " https://allegro.pl.allegrosandbox.pl/auth/oauth/token";
        //private static string environment = "allegrosandbox.pl";


        private string AllegroUploadURL = $"https://upload.{environment}/sale/images";
        private string AllegroBaseURL = $"https://api.{environment}";

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
            if (!Directory.Exists("Images"))
                Directory.CreateDirectory("Images");

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

            using HttpClient client = new HttpClient();
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
        const int offerslimit = 1000;
        public async Task<OffersModel> GetAllOffers(bool getAll = false)
        {
            using HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("pl-PL"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json"));

            OffersModel retrvied = new OffersModel()
            {
                offers = new List<SimpleOfferModel>(),
                totalCount = 0,
                count = 0
            };

            bool keepgoin = true;
            int offset = 0;
            do
            {
                keepgoin = false;
                HttpResponseMessage odp = await client.GetAsync(AllegroBaseURL + $"/sale/offers?limit={offerslimit}&offset={offset}");

                OffersModel model = JsonConvert.DeserializeObject<OffersModel>(odp.Content.ReadAsStringAsync().Result);
                if (model.offers == null)
                    break;
                retrvied.offers.AddRange(model.offers);
                retrvied.count += model.count;
                retrvied.totalCount += model.totalCount;

                if(retrvied.count >= offerslimit)
                {
                    System.Diagnostics.Debug.WriteLine(model.count);
                    offset += offerslimit;
                    if (getAll)
                        keepgoin = true;
                }

                System.Diagnostics.Debug.WriteLine("tet:    " + model.count);
            }
            while (keepgoin);

            return retrvied;
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
            using HttpClient client = new HttpClient();
            string commandID = Guid.NewGuid().ToString();

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("pl-PL"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json"));

            if (amount.Contains(',')) amount = amount.Replace(',', '.');

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

            return response;
        }

        public async Task<HttpResponseMessage> ChangeStock(string offerId, Stock _stock)
        {
            using HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("pl-PL"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json"));

            var jsonobject = new
            {
                stock = _stock
            };

            var jsonstring = JsonConvert.SerializeObject(jsonobject);
            var content = new StringContent(jsonstring, Encoding.UTF8, "application/vnd.allegro.public.v1+json");

            var response = await client.PatchAsync(AllegroBaseURL + $"/sale/product-offers/{offerId}", content);

            //System.Diagnostics.Debug.WriteLine(response.Content.ReadAsStringAsync().Result);
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offerids"></param>
        /// <param name="activate">true => publish false => end</param>
        /// <returns></returns>
        public async Task<(HttpResponseMessage, string)> BatchChangePublication(Base[] offerids, bool activate)
        {
            using HttpClient client = new HttpClient();
            string commandID = Guid.NewGuid().ToString();

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("pl-PL"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json"));

            var publication = new PublicationModel()
            {
                publication = new ModifiPublication()
                {
                    action = activate ? "ACTIVATE" : "END"
                    //scheduleFor = DateTime.Now.AddMinutes(10).ToString("yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'")
                },

                offerCriteria = new Criteria[]
                {
                    new Criteria()
                    {
                        offers = offerids,
                        type = "CONTAINS_OFFERS"
                    }
                }
            };

            var jsonstring = JsonConvert.SerializeObject(publication);
            var content = new StringContent(jsonstring, Encoding.UTF8, "application/vnd.allegro.public.v1+json");
            System.Diagnostics.Debug.WriteLine(jsonstring);

            var response = await client.PutAsync(AllegroBaseURL + $"/sale/offer-publication-commands/{commandID}", content);

            //System.Diagnostics.Debug.WriteLine(response.Content.ReadAsStringAsync().Result);
            return (response, commandID);
        }

        public async Task<HttpResponseMessage> GetPublicationResult(Base[] offerids, string commandID)
        {
            using HttpClient client = new HttpClient();
            //string commandID = Guid.NewGuid().ToString();

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("pl-PL"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json"));

            var response = await client.GetAsync(AllegroBaseURL + $"/sale/offer-publication-commands/{commandID}/tasks");
            //var response = await client.GetAsync(AllegroBaseURL + $"/sale/offer-publication-commands/{commandID}");

            //System.Diagnostics.Debug.WriteLine(response.Content.ReadAsStringAsync().Result);
            return response;
        }

        /// <summary>
        /// function for creating offer based on existing product from allegro
        /// </summary>
        /// <param name="_product"></param>
        /// <param name="baseValue"></param>
        /// <param name="bookid"></param>
        /// <param name="deliveryid"></param>
        /// <param name="offerName"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public async Task<(HttpContent, HttpStatusCode)> CreateOfferBasedOnExistingProduct(ProductModel _product, BaseValue baseValue, string bookid, string deliveryid, string offerName, string price)
        {
            using HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("pl-PL"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json"));

            OfferModel allegrooffer = new OfferModel();

            AllegroOfferProduct prod = new AllegroOfferProduct()
            {
                id = _product.id,
                name = _product.name,
                category = _product.category,
                parameters = _product.parameters,
                images = new string[_product.images.Length]
            };

            for (int i = 0; i < _product.images.Length; prod.images[i] = _product.images[i].url, i++)

            allegrooffer.productSet = new Models.Offer.offerComponents.ProductItem[]
                {
                new Models.Offer.offerComponents.ProductItem()
                {
                    product = prod,
                    quantity = new BaseValue()
                    {
                        value= 1
                    }
                }
            };

            allegrooffer.external = new Base()
            {
                id = bookid
            };

            allegrooffer.stock = new Models.Offer.offerComponents.Stock();
            allegrooffer.stock.unit = "UNIT";
            allegrooffer.stock.available = baseValue.value;

            allegrooffer.payments = new Models.Offer.offerComponents.Payments();
            allegrooffer.payments.invoice = "VAT";

            allegrooffer.name = offerName;

            if (price.Contains(',')) price = price.Replace(',', '.');
            allegrooffer.sellingMode = new Models.Offer.offerComponents.SellingMode()
            {
                format = "BUY_NOW",
                price = new Models.Offer.offerComponents.PriceModel()
                {
                    amount = price,
                    currency = "PLN"
                }
            };

            allegrooffer.delivery = new Models.Offer.offerComponents.delivery.Delivery()
            {
                shippingRates = new Base()
                {
                    id = deliveryid
                }
            };

            allegrooffer.category = new Base()
            {
                //id = "66791"
                id = _product.category.id
                //id = categoryid
            };

            allegrooffer.publication = new Publication()
            {
                status = "INACTIVE",
                //duration = "P7D",
                //republish = true,
                endedBy = "EMPTY_STOCK"
            };

            string json = JsonConvert.SerializeObject(allegrooffer);
            var content = new StringContent(json, Encoding.UTF8, "application/vnd.allegro.public.v1+json");

            //https://api.{environment}/sale/product-offers
            HttpResponseMessage odp = await client.PostAsync(AllegroBaseURL + $"/sale/product-offers", content);

            return (odp.Content,odp.StatusCode);
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

            return odp;
        }
        #endregion

        #region Product

        /// <summary>
        /// zwraca wszystkie dane na temat oferty not implemneted
        /// </summary>
        /// <param name="offerId"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> ProposeProduct(ProductModel product)
        {
            string productid = string.Empty;

            using HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("pl-PL"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json"));

            string json = JsonConvert.SerializeObject(product);

            var content = new StringContent(json, Encoding.UTF8, "application/vnd.allegro.public.v1+json");
            //System.Diagnostics.Debug.WriteLine("Test :" +json);
            HttpResponseMessage odp = await client.PostAsync(AllegroBaseURL + $"/sale/product-proposals", content);


            if (odp.Headers.Contains("location"))
                productid = odp.Headers.Location.AbsoluteUri.Split("/")[5];

            return odp;
        }

        public async Task<ProductModel> CheckForProduct(string productEan)
        {
            using HttpClient client = new HttpClient();

            System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("pl-PL"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json"));

            var productmodel = new CheckForProductModel()
            {
                ean = productEan
            };

            string json = JsonConvert.SerializeObject(productmodel);

            var content = new StringContent(json, Encoding.UTF8, "application/vnd.allegro.public.v1+json");
            HttpResponseMessage odp = await client.GetAsync(AllegroBaseURL + $"/sale/products?ean={productEan}");

            //System.Diagnostics.Debug.WriteLine(odp.Content.ReadAsStringAsync().Result);

            //troche do przerobienia w celu unikniecnia wartosci null
            ProductModel product = null;
            try
            {
                 product = JsonConvert.DeserializeObject<AllegroProductResponse>(odp.Content.ReadAsStringAsync().Result).products?[0];

            }
            catch (IndexOutOfRangeException e) { System.Diagnostics.Debug.Write("     no product "); }
            return product;
        }
        #endregion


        #region Delivery

        public async Task<ShippingRate[]> GetShippingRates()
        {
            using HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("pl-PL"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json"));

            HttpResponseMessage odp = await client.GetAsync(AllegroBaseURL + "/sale/shipping-rates");

            ShippingRates model = JsonConvert.DeserializeObject<ShippingRates>(odp.Content.ReadAsStringAsync().Result);

            //System.Diagnostics.Debug.WriteLine(odp.StatusCode.ToString());
            //System.Diagnostics.Debug.WriteLine(odp.Content.ReadAsStringAsync().Result);

            return model.shippingRates;
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
        /// return url to uploaded image
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public async Task<string> UploadImage(Stream image)
        {
            //save and convert image to jpeg from jpg
            Bitmap mapa = new Bitmap(image);
            string photoguid = Guid.NewGuid().ToString().Substring(0, 7);
            mapa.Save($"Images/{photoguid}.jpeg", ImageFormat.Jpeg);

            //upload image
            WebRequest request = WebRequest.Create(AllegroUploadURL);
            request.Method = "POST";
            byte[] byteArray = File.ReadAllBytes($"Images/{photoguid}.jpeg");
            request.ContentType = "image/jpeg";
            request.ContentLength = byteArray.Length;
            request.Headers.Add("Authorization", "Bearer " + AccessToken);
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();

            //retreview response string
            var responsecontent = new StreamReader(response.GetResponseStream()).ReadToEnd();
            //System.Diagnostics.Debug.WriteLine(responsecontent);

            //Deserialize url to object
            ImageUpload imageurl = JsonConvert.DeserializeObject<ImageUpload>(responsecontent);

            //delete saved image
            File.Delete($"Images/{photoguid}.jpeg");

            return imageurl.location;
        }

        #region category
        /// <summary>
        /// get suggestion based on item name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<CategorySuggestionModel> GetSuggestionOfCategory(string name)
        {
            using HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("pl-PL"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json"));

            HttpResponseMessage odp = await client.GetAsync(AllegroBaseURL + $"/sale/matching-categories?name={name}");

            var suggestion = JsonConvert.DeserializeObject<CategorySuggestionModel>(odp.Content.ReadAsStringAsync().Result);

            return suggestion;
        }

        public async Task<CategoryParametersModel> GetCategoryParameters(string categoryID)
        {
            using HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("pl-PL"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json"));

            HttpResponseMessage odp = await client.GetAsync(AllegroBaseURL + $"/sale/categories/{categoryID}/parameters");

            //System.Diagnostics.Debug.WriteLine(odp.Content.ReadAsStringAsync().Result);

            var parameters = JsonConvert.DeserializeObject<CategoryParametersModel>(odp.Content.ReadAsStringAsync().Result);

            return parameters;
        }
        #endregion
    }
}