using FanaticFirefly.Enumerations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace FanaticFirefly.Data
{
    public static class RestService
    {
        public static async Task<JToken> Deserialize(string url)
        {
            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;

            HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(continueOnCapturedContext: false);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();

                var token = JsonConvert.DeserializeObject<JToken>(data);
                return token;
            }
            return null;
        }

        public static async Task<string> Serialize(String url, Object data, SerializeType serialize)
        {
            string reply = "";
            var response = new HttpResponseMessage();

            try
            {
                HttpClient client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;

                var serealizedfile = JsonConvert.SerializeObject(data);
                var content = new StringContent(serealizedfile.ToString(), Encoding.UTF8, "application/json");

                switch (serialize)
                {
                    case SerializeType.Post:
                        response = await client.PostAsync(url, content).ConfigureAwait(continueOnCapturedContext: false); 
                        break;
                    case SerializeType.Put:
                        response = await client.PutAsync(url, content);
                        break;
                    case SerializeType.Delete:
                        response = await client.DeleteAsync(url + Convert.ToString(data));
                        break;
                }

                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            finally
            {
                reply = await response.Content.ReadAsStringAsync();
            }

            return reply;
        }
    }
}