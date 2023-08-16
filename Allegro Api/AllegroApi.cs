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
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Timers;
using Allegro_Api.Models.Offer.offerComponents.delivery;
using Allegro_Api.Models.Order;
using Allegro_Api.Models.Order.checkoutform.components;
using Allegro_Api.Models.Order.checkoutform;
using System.Runtime.Intrinsics.Arm;

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
        public string RefreshToken = string.Empty;

        //przedłużanie accesstokena https://developer.allegro.pl/tutorials/uwierzytelnianie-i-autoryzacja-zlq9e75GdIR#przedluzenie-waznosci-tokena

        private System.Timers.Timer timer = new System.Timers.Timer();

        public delegate void RefreshTokenDelgate();
        /// <summary>
        /// event occures when token is refreshed
        /// </summary>
        public event RefreshTokenDelgate RefreshTokenEvent;

        public AllegroApi(string ClientID, string ClientSecret, RefreshTokenDelgate refreshtokenevent)
        {
            if (!Directory.Exists("Images"))
                Directory.CreateDirectory("Images");

            this.ClientID = ClientID;
            this.ClientSecret = ClientSecret;


            this.RefreshTokenEvent += refreshtokenevent;

            System.Diagnostics.Debug.WriteLine(RefreshToken);
            this.RefreshAccesToken();


            this.timer.Elapsed += Timer_Elapsed;
        }

        public AllegroApi(string ClientID, string ClientSecret, string RefreshToken, RefreshTokenDelgate refreshtokenevent)
        {
            if (!Directory.Exists("Images"))
                Directory.CreateDirectory("Images");

            this.ClientID = ClientID;
            this.ClientSecret = ClientSecret;
            this.RefreshToken = RefreshToken;

            this.RefreshTokenEvent += refreshtokenevent;

            System.Diagnostics.Debug.WriteLine(RefreshToken);
            this.RefreshAccesToken();

            this.timer.Elapsed += Timer_Elapsed;
        }

        private async void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            await RefreshAccesToken();
        }

        //TODO EDIT AN OFFER
        //TODO BATCH OFFER PUBLISH/

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

                this.timer.Interval = TokenExpiresIn * 1000;
                this.timer.Start();

                return true;
            }

            return false;
        }

        public async Task RefreshAccesToken()
        {
            using HttpClient client = new HttpClient();
            string formatedstring = $"{ClientID}:{ClientSecret}";

            byte[] bytes = Encoding.UTF8.GetBytes(formatedstring);
            //create auth string containg normal in base 64
            string AuthString = "Basic " + Convert.ToBase64String(bytes);

            //add neccessary headers
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", AuthString);

            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("pl-PL"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json"));

            var values = new Dictionary<string, string>
            {
                 { "grant_type", "refresh_token" },
                 { "refresh_token", RefreshToken }
            };
            var content = new FormUrlEncodedContent(values);

            HttpResponseMessage odp = await client.PostAsync(AllegoTokenURL, content);

            AccessTokenModel model = JsonConvert.DeserializeObject<AccessTokenModel>(odp.Content.ReadAsStringAsync().Result);
            System.Diagnostics.Debug.WriteLine(odp.Content.ReadAsStringAsync().Result);
            if (!odp.IsSuccessStatusCode) return;

            //if user authorized access then remove device code and set other variables for later
            AccessToken = model.access_token;
            RefreshToken = model.refresh_token;
            System.Diagnostics.Debug.WriteLine(RefreshToken);
            RefreshTokenEvent?.Invoke();
            this.timer.Interval = TokenExpiresIn * 1000;
            this.timer.Start();
        }
        #endregion

        #region offers
        /// <summary>
        /// to juz jest zapytanie strikte do api allegro typu REST api
        /// </summary>
        /// <returns></returns>
        const int offerslimit = 1000;
        public async Task<OffersModel> GetAllOffers(OfferState StateFilter = 0)
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

            string StateFilterstring = string.Empty;
            if (StateFilter.HasFlag(OfferState.INACTIVE)) StateFilterstring += "&publication.status=INACTIVE";
            if (StateFilter.HasFlag(OfferState.ACTIVE)) StateFilterstring += "&publication.status=ACTIVE";
            if (StateFilter.HasFlag(OfferState.ENDED)) StateFilterstring += "&publication.status=ENDED";
            if (StateFilter.HasFlag(OfferState.ACTIVATING)) StateFilterstring += "&publication.status=ACTIVATING";

            int offset = 0;
            do
            {
                HttpResponseMessage odp = await client.GetAsync(AllegroBaseURL + $"/sale/offers?limit={offerslimit}&offset={offset}{StateFilterstring}");

                if (odp == null) return null;

                OffersModel model = JsonConvert.DeserializeObject<OffersModel>(odp.Content.ReadAsStringAsync().Result);

                if (model == null || model.offers == null)
                    break;

                retrvied.offers.AddRange(model.offers);
                retrvied.count += model.count;
                retrvied.totalCount = model.totalCount;
                //System.Diagnostics.Debug.WriteLine(model.count);

                if (model.count >= offerslimit)
                {
                    offset += offerslimit;
                }

                odp.Dispose();
                //System.Diagnostics.Debug.WriteLine("tet:    " + retrvied.count);
            }
            while (retrvied.count != retrvied.totalCount);

            client.Dispose();

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

            client.Dispose();
            //System.Diagnostics.Debug.WriteLine(response.Content.ReadAsStringAsync().Result);
            return response;
        }

        public async Task<HttpResponseMessage> ChangeDeliveryTime(string offerId, DeliveryOfferModel deliveryOffer, string handlingtime)
        {
            using HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("pl-PL"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json"));

            deliveryOffer.handlingTime = handlingtime;

            var jsonobject = new
            {
                delivery = deliveryOffer
            };

            var jsonstring = JsonConvert.SerializeObject(jsonobject);
            var content = new StringContent(jsonstring, Encoding.UTF8, "application/vnd.allegro.public.v1+json");

            var response = await client.PatchAsync(AllegroBaseURL + $"/sale/product-offers/{offerId}", content);

            client.Dispose();
            //System.Diagnostics.Debug.WriteLine(response.Content.ReadAsStringAsync().Result);
            return response;
        }


        public async Task<HttpResponseMessage> ChangeExternal(string offerId, string externalID)
        {
            using HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("pl-PL"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json"));

            var jsonobject = new
            {
                external = new { id = externalID }
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
        public async Task<(HttpResponseMessage, string, PublicationModel)> BatchChangePublication(Base[] offerids, bool activate)
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

            System.Diagnostics.Debug.WriteLine(publication.offerCriteria[0].offers.Length);

            var jsonstring = JsonConvert.SerializeObject(publication);
            var content = new StringContent(jsonstring, Encoding.UTF8, "application/vnd.allegro.public.v1+json");
            System.Diagnostics.Debug.WriteLine(jsonstring);

            var response = await client.PutAsync(AllegroBaseURL + $"/sale/offer-publication-commands/{commandID}", content);

            //System.Diagnostics.Debug.WriteLine(response.Content.ReadAsStringAsync().Result);
            return (response, commandID, publication);
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
        public async Task<(HttpContent, HttpStatusCode, OfferModel)> CreateOfferBasedOnExistingProduct(
            ProductModel _product,string EAN, BaseValue stock, string bookid, string deliveryid, string handlingTime, string offerName, string price)
        {
            using HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("pl-PL"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json"));

            OfferModel allegrooffer = new OfferModel();

            //if (float.Parse(price) < 4) price = "4";

            //allegrooffer.customParameters = new CustomParameter[] {
            //    new CustomParameter()
            //    {
            //        name = "EAN",
            //        values = new string[] { EAN }
            //    }
            //};

            //_product.parameters.Append(new ProductParameter()
            //{

            //})

            AllegroOfferProduct prod = new AllegroOfferProduct()
            {
                id = _product.id,
                idType = "GTIN",
                name = _product.name,
                category = _product.category,
                parameters = _product.parameters,
                images = new string[_product.images.Length >= 16 ? 16 : _product.images.Length]
            };

            offerName = offerName.Replace("•", "").Replace("—", "").Replace("®", "");

            for (int i = 0; i < _product.images.Length && i < 16; prod.images[i] = _product.images[i].url, i++) ;

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

            allegrooffer.description = _product.description;

            allegrooffer.images = prod.images;

            allegrooffer.external = new Base()
            {
                id = bookid
            };

            allegrooffer.stock = new Models.Offer.offerComponents.Stock();
            allegrooffer.stock.unit = "UNIT";
            allegrooffer.stock.available = stock.value;

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
                },
                handlingTime = handlingTime
            };

            allegrooffer.category = new Base()
            {
                id = _product.category.id
            };

            allegrooffer.publication = new Publication()
            {
                status = "INACTIVE",
                endedBy = "EMPTY_STOCK"
            };

            string json = JsonConvert.SerializeObject(allegrooffer);
            System.Diagnostics.Debug.WriteLine(json);
            var content = new StringContent(json, Encoding.UTF8, "application/vnd.allegro.public.v1+json");

            //https://api.{environment}/sale/product-offers
            HttpResponseMessage odp = null;
            try
            {
                 odp = await client.PostAsync(AllegroBaseURL + $"/sale/product-offers", content);
            }catch(HttpRequestException e)
            {
                return (null,HttpStatusCode.BadRequest,null);
            }

            return (odp.Content, odp.StatusCode, allegrooffer);
        }

        public async Task<(HttpContent, HttpStatusCode, OfferModel)> CreateOfferSetBasedOnExistingProducts(
            ProductModel[] _product, BaseValue[] stock, string[] bookid, StandardizedDescription SetDescription, string deliveryid, string handlingTime, string offerName, string price)
        {
            using HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("pl-PL"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json"));

            OfferModel allegrooffer = new OfferModel();

            AllegroOfferProduct[] products = new AllegroOfferProduct[_product.Length];
            string[] imagesList = new string[products.Length];

            for (int i = 0; i < _product.Length; i++)
            {
                products[i] = new AllegroOfferProduct()
                {
                    id = _product[i].id,
                    name = _product[i].name,
                    category = _product[i].category,
                    parameters = _product[i].parameters,
                    images = new string[] { _product[i].images[0].url }
                };
                imagesList[i] = _product[i].images[0].url;
            }

            offerName = offerName.Replace("•", "").Replace("—", "").Replace("®", "");

            int smallestMagazinCount = stock[0].value;
            for (int i = 1; i < stock.Length; i++)
                if (smallestMagazinCount > stock[i].value)
                    smallestMagazinCount = stock[i].value;

            allegrooffer.productSet = new Models.Offer.offerComponents.ProductItem[_product.Length];

            for (int i = 0; i < allegrooffer.productSet.Length; i++)
            {
                allegrooffer.productSet[i] = new Models.Offer.offerComponents.ProductItem()
                {
                    product = products[i],
                    quantity = new BaseValue()
                    {
                        value = 1
                    }
                };
            }

            allegrooffer.description = SetDescription;


            allegrooffer.images = imagesList;

            StringBuilder externalid = new StringBuilder();
            foreach(var item in bookid)
                externalid.Append( item + "/");
            externalid.Remove(externalid.Length - 1, 1);

            allegrooffer.external = new Base()
            {
                id = externalid.ToString()
            };

            allegrooffer.stock = new Models.Offer.offerComponents.Stock();
            allegrooffer.stock.unit = "SET";
            allegrooffer.stock.available = smallestMagazinCount;

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
                },
                handlingTime = handlingTime
            };

            allegrooffer.category = new Base()
            {
                id = _product[0].category.id
            };

            allegrooffer.publication = new Publication()
            {
                status = "INACTIVE",
                endedBy = "EMPTY_STOCK"
            };

            string json = JsonConvert.SerializeObject(allegrooffer);
            System.Diagnostics.Debug.WriteLine(json);
            var content = new StringContent(json, Encoding.UTF8, "application/vnd.allegro.public.v1+json");

            //https://api.{environment}/sale/product-offers
            HttpResponseMessage odp = await client.PostAsync(AllegroBaseURL + $"/sale/product-offers", content);

            return (odp.Content, odp.StatusCode, allegrooffer);
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
        //public async Task<(HttpContent, HttpStatusCode, OfferModel)> CreateOfferWithSetOfProducts(
        //    ProductModel[] _products, BaseValue baseValue, string bookid, string deliveryid, string offerName, string price)
        //{
        //    using HttpClient client = new HttpClient();

        //    client.DefaultRequestHeaders.Clear();
        //    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
        //    client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("pl-PL"));
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json"));

        //    OfferModel allegrooffer = new OfferModel();

        //    AllegroOfferProduct prod = new AllegroOfferProduct()
        //    {
        //        id = _product.id,
        //        name = _product.name,
        //        category = _product.category,
        //        parameters = _product.parameters,
        //        images = new string[1]
        //    };

        //    offerName = offerName.Replace("•", "").Replace("—", "").Replace("®", "");

        //    List<string> Images = new List<string>();
        //    foreach (ProductModel x in _products)
        //    {
        //        if (x.images.Length > 0)
        //            Images.Add(x.images[0].url);
        //    }

        //    allegrooffer.productSet = new Models.Offer.offerComponents.ProductItem[]
        //    {
        //        new Models.Offer.offerComponents.ProductItem()
        //        {
        //            product = prod,
        //            quantity = new BaseValue()
        //            {
        //                value= 1
        //            }
        //        }
        //    };

        //    allegrooffer.external = new Base()
        //    {
        //        id = bookid
        //    };

        //    allegrooffer.stock = new Models.Offer.offerComponents.Stock();
        //    allegrooffer.stock.unit = "SET";
        //    allegrooffer.stock.available = baseValue.value;

        //    allegrooffer.payments = new Models.Offer.offerComponents.Payments();
        //    allegrooffer.payments.invoice = "VAT";

        //    allegrooffer.name = offerName;

        //    if (price.Contains(',')) price = price.Replace(',', '.');
        //    allegrooffer.sellingMode = new Models.Offer.offerComponents.SellingMode()
        //    {
        //        format = "BUY_NOW",
        //        price = new Models.Offer.offerComponents.PriceModel()
        //        {
        //            amount = price,
        //            currency = "PLN"
        //        }
        //    };

        //    allegrooffer.delivery = new Models.Offer.offerComponents.delivery.Delivery()
        //    {
        //        shippingRates = new Base()
        //        {
        //            id = deliveryid
        //        }
        //    };

        //    allegrooffer.category = new Base()
        //    {
        //        id = _product.category.id
        //    };

        //    allegrooffer.publication = new Publication()
        //    {
        //        status = "INACTIVE",
        //        endedBy = "EMPTY_STOCK"
        //    };

        //    string json = JsonConvert.SerializeObject(allegrooffer);
        //    var content = new StringContent(json, Encoding.UTF8, "application/vnd.allegro.public.v1+json");

        //    //https://api.{environment}/sale/product-offers
        //    HttpResponseMessage odp = await client.PostAsync(AllegroBaseURL + $"/sale/product-offers", content);

        //    return (odp.Content, odp.StatusCode, allegrooffer);
        //}

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

        public async Task<ProductModel> CheckForProduct(string productISBN)
        {
            using HttpClient client = new HttpClient();

            System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("pl-PL"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json"));
            HttpResponseMessage odp = null;
            try
            {
                 odp = await client.GetAsync(AllegroBaseURL + $"/sale/products?phrase={productISBN}&mode=GTIN");
            }
            catch(HttpRequestException) { return null; }

            client.Dispose();

            ProductModel product = null;

            var result = JsonConvert.DeserializeObject<AllegroProductResponse>(odp.Content.ReadAsStringAsync().Result);

            odp.Dispose();

            if (result.products != null && result.products.Length > 0)
                product = result.products[0];

            return product;
        }

        public async Task<bool> ValidateProduct(string productdesciption, string ISBN)
        {
            //search for url in description
            Regex rx = new Regex(@"[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b(?:[-a-zA-Z0-9()@:%_\+.~#?&//=]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            if (rx.IsMatch(productdesciption))
            {
                return false;
            }
            rx = null;
            return true;
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

        /// <summary>
        /// return url to uploaded image
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public async Task<string> UploadImage(Stream image)
        {
            //save and convert image to jpeg from jpg
            try
            {
                Bitmap mapa = null;
                try
                {
                    mapa = new Bitmap(image);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine(image.Length.ToString());
                    Console.WriteLine();
                    return null;
                }
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
            catch (WebException)
            {
                return null;
            }

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

        #region Drafts

        public async Task<HttpResponseMessage> DeleteDraft(string offerId)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("pl-PL"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json"));

            HttpResponseMessage odp = await client.DeleteAsync(AllegroBaseURL + $"/sale/offers/{offerId}");

            return odp;
        }

		#endregion

		#region Orders
		public async Task<List<CheckOutForm>> GetOrders(DateTime LatestDownloadDate, OrderStatusType type)
		{
			using HttpClient client = new HttpClient();

			client.DefaultRequestHeaders.Clear();
			client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
			client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("pl-PL"));
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json"));

            OrdersModel retrevied = new OrdersModel()
            {
                totalCount = 0,
                count = 0,
                checkoutForms = new List<CheckOutForm>() 
            };

            string stringtype = type.ToString();
            int offset = 0;
            int limit = 100;
            do
            {
                HttpResponseMessage odp = await client.GetAsync(AllegroBaseURL + $"/order/checkout-forms?limit={limit}&offset={offset}&lineItems.boughtAt.gte={LatestDownloadDate.ToString("O")}&fulfillment.status={stringtype}");

                if (odp == null) return null;

                OrdersModel model = JsonConvert.DeserializeObject<OrdersModel>(odp.Content.ReadAsStringAsync().Result);

                if (model == null || model.checkoutForms == null)
                    break;

                retrevied.checkoutForms.AddRange(model.checkoutForms);
                retrevied.count += model.count;
                retrevied.totalCount = model.totalCount;

                if (retrevied.totalCount - retrevied.count < limit) limit = retrevied.totalCount - retrevied.count;

            } while (retrevied.count < retrevied.totalCount);

            return retrevied.checkoutForms;
		}
        
        public async Task<List<CheckOutForm>> GetOrders(OrderStatusType type)
		{
			using HttpClient client = new HttpClient();

			client.DefaultRequestHeaders.Clear();
			client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
			client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("pl-PL"));
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json"));

            OrdersModel retrevied = new OrdersModel()
            {
                totalCount = 0,
                count = 0,
                checkoutForms = new List<CheckOutForm>() 
            };

            string stringtype = type.ToString();
            int offset = 0;
            int limit = 100;
            do
            {
                HttpResponseMessage odp = await client.GetAsync(AllegroBaseURL + $"/order/checkout-forms?limit={limit}&offset={offset}&fulfillment.status={stringtype}");

                if (odp == null) return null;

                OrdersModel model = JsonConvert.DeserializeObject<OrdersModel>(odp.Content.ReadAsStringAsync().Result);

                if (model == null || model.checkoutForms == null)
                    break;

                retrevied.checkoutForms.AddRange(model.checkoutForms);
                retrevied.count += model.count;
                retrevied.totalCount = model.totalCount;

                if (retrevied.totalCount - retrevied.count < limit) limit = retrevied.totalCount - retrevied.count;

            } while (retrevied.count < retrevied.totalCount);

            return retrevied.checkoutForms;
		}
        
        public async Task<DetailedCheckOutForm> GetOrderDetails(string OrderID)
		{
			using HttpClient client = new HttpClient();

			client.DefaultRequestHeaders.Clear();
			client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AccessToken);
			client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("pl-PL"));
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json"));

            HttpContent odp =  (await client.GetAsync(AllegroBaseURL + $"/order/checkout-forms/{OrderID}")).Content;

            DetailedCheckOutForm model = JsonConvert.DeserializeObject<DetailedCheckOutForm>(odp.ReadAsStringAsync().Result);

            return model;
		}
		#endregion
	}
}