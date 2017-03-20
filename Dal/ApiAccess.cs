using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using Thinktecture.IdentityModel.Client;

namespace BIMToolkitAPIClient.Dal
{

    /// <summary>
    /// Demonstration of how to call BIM Toolkit API server side
    /// </summary>
    public static class ApiAccess
    {
        //enter these values in web.config
        //NOTE :- To obtain this information, please email your request seth.okai@thenbs.com
        private static string _clientId = ConfigurationManager.AppSettings["ClientId"];
        private static string _clientSecret = ConfigurationManager.AppSettings["ClientSecret"];

        /// <summary>
        /// Get a token using Thinktecture OAuth2Client
        /// </summary>
        /// <returns></returns>
        public static string GetToken()
        {
            var client = new OAuth2Client(
                new Uri(ConfigurationManager.AppSettings["Authority"] + "/connect/token"),
                _clientId,
                _clientSecret);

            var response = client.RequestClientCredentialsAsync("bimtoolkitapi").Result;

            return response.AccessToken;
        }

        /// <summary>
        /// Illustrates how to get token without OAuth2Client helper
        /// </summary>
        /// <returns></returns>
        public static string GetTokenExplained()
        {
            //data to post to the token endpoint
            var fields = new Dictionary<string, string>
            {
                { "scope", "bimtoolkitapi" },
                { "grant_type", "client_credentials" }
            };

            //setup basic authentication
            string creds = String.Format("{0}:{1}", _clientId, _clientSecret);
            byte[] bytes = Encoding.ASCII.GetBytes(creds);
            var header = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));

            //configure client
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = header;
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Authority"] + "/connect/token");

            //get response and parse as json
            var response = client.PostAsync(string.Empty, new FormUrlEncodedContent(fields)).Result;
            string raw = response.Content.ReadAsStringAsync().Result;
            var json = JObject.Parse(raw);

            //try to get the access token from the json response
            JToken token;
            if (json.TryGetValue("access_token", out token))
            {
                return token.ToString();
            }

            return "";
        }

        /// <summary>
        /// Get a token before making a GET request to the specified api path
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        public static string CallApi(string path)
        {
            string token = GetToken();
            //get token without helper
            //string token = GetTokenExplained();
            var client = new HttpClient();
            client.SetBearerToken(token);
            //set header without helper
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return client.GetStringAsync(ConfigurationManager.AppSettings["Api"] + "/" + path).Result;
        }
    }
}