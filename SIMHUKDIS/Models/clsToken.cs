using Newtonsoft.Json;
using SIMHUKDIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace SIMHUKDIS.Models
{    
    public class clsToken
    {
        string baseAddress = "https://api.kemenag.go.id/v1/";

        public string GetAccessToken(string username, string password)
        {
            Token token = new Token();
            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            var RequestBody = new Dictionary<string, string>
                {
                {"email", username},
                {"password", password},
                };
            var tokenResponse = client.PostAsync(baseAddress + "auth/login", new FormUrlEncodedContent(RequestBody)).Result;

            if (tokenResponse.IsSuccessStatusCode)
            {
                var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;
                token = JsonConvert.DeserializeObject<Token>(JsonContent);
            }
            else
            {
                token.token = "";
            }
            return token.token;
        }
    }
}