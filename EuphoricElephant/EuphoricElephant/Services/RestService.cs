﻿using EuphoricElephant.Enumerations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace EuphoricElephant.Services
{
    public static class RestService
    {
        public static async Task<JToken> Deserialize(string url)
        {
            HttpResponseMessage response = null;
            JToken token = null;

            try
            {
                var client = new HttpClient();

                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "Your Oauth token");

                response = await client.GetAsync(url);
                var data = await response.Content.ReadAsStringAsync();

                token = JsonConvert.DeserializeObject<JToken>(data);

                return token;
            }
            catch (Exception e)
            {
                ErrorService.showError(e.Message);
                return token;
            }
            finally
            {
                response.Dispose();
            }
            
        }

        public static async Task<string> Serialize(String url, Object data, SerializeType serialize)
        {
            string reply = "";
            var response = new HttpResponseMessage();

            try
            {
                var client = new HttpClient();
                var serealizedfile = JsonConvert.SerializeObject(data);
                var content = new StringContent(serealizedfile.ToString(), Encoding.UTF8, "application/json");             

                switch (serialize)
                {
                    case SerializeType.Post:
                        response = await client.PostAsync(url, content);
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
                ErrorService.showError(e.Message);
            }
            finally
            {
                reply = await response.Content.ReadAsStringAsync();
                response.Dispose();
            }

            return reply;


        }
    }
}
