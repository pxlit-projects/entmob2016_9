using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace EuphoricElephant.Services
{
    public static class RestService
    {
        public static async Task<JsonArray> Deserialize(String url)
        {
            JsonArray res;
            var client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(url);
            var data = await response.Content.ReadAsStringAsync();
            res = JsonValue.Parse(data).GetArray();

            return res;
        }

        public static async Task<JToken> foo(string url)
        {
            var client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(url);
            var data = await response.Content.ReadAsStringAsync();

            var token = JsonConvert.DeserializeObject<JToken>(data);

            return token;
        }

        public static async Task<JsonArray> DeserializeSingle(String url)
        {
            JsonArray res;
            var client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(url);
            var data = await response.Content.ReadAsStringAsync();

            if (data == string.Empty)
            {
                res = null;
            }
            else
            {
                res = new JsonArray() { JsonValue.Parse(data) };
            }

            return res;
        }

        public static async Task<JsonObject> Serialize(String url, Object data)
        {
            try
            {
                var client = new HttpClient();
                var serealizedfile = JsonConvert.SerializeObject(data);
                var content = new StringContent(serealizedfile.ToString(), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                Debug.WriteLine("iets dom "+ response.StatusCode);
                string reply = await response.Content.ReadAsStringAsync();
                return await Task.Run(()=> JsonObject.Parse(reply));
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
            
        }
    }
}
