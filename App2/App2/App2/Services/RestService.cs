using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace App2.Data
{
    public class RestService
    {
        HttpClient client;

        public RestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<JToken> foo(string url)
        {
            HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(continueOnCapturedContext: false);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();

                var token = JsonConvert.DeserializeObject<JToken>(data);
                return token;
            }
            return null;
        }
    }
}