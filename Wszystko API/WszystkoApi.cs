﻿using Allegro_Api.Models;
using System.Net.Http.Headers;
using System.Text;

namespace Wszystko_API
{
    public class WszystkoApi
    {

        //acces token jest ważny przez 12 h
        private string AccessToken = string.Empty;
        private int TokenExpiresIn = -1;
        //RefreshToken jest ważny natomiast przez 3miesiace
        public string RefreshToken = string.Empty;


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

                Console.WriteLine((TokenExpiresIn / 2) * 1000);

                this.timer.Interval = (TokenExpiresIn / 2) * 1000;
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
            if (!odp.IsSuccessStatusCode) return;

            //if user authorized access then remove device code and set other variables for later
            AccessToken = model.access_token;
            RefreshToken = model.refresh_token;

            RefreshTokenEvent?.Invoke();
            try
            {
                this.timer.Interval = (TokenExpiresIn / 2) * 1000;
            }
            catch (ArgumentException)
            {
                this.timer.Interval = 21599000;
            }

            this.timer.Start();
        }
        #endregion

    }
}