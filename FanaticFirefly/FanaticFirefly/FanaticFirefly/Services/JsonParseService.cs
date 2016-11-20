using FanaticFirefly.Data;
using FanaticFirefly.Enumerations;
using FanaticFirefly.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanaticFirefly.Services
{
    public static class JsonParseService<T>
    {
        public static async Task<string> SerializeDataToJson(String url, Object data, SerializeType type)
        {
            string reply = string.Empty;

            try
            {
                reply = await RestService.Serialize(url, data, type);
            }
            catch (Exception e)
            {
                ErrorService.ShowError(e.Message);
            }

            return reply;
        }

        public static async Task<T> DeserializeDataFromJson(string url, string param)
        {
            object obj = null;

            try
            {
                var formattedParam = param;

                if (param == null)
                {
                    formattedParam = string.Empty;
                }

                var formattedUrl = url + param;

                JToken res = res = await RestService.Deserialize(formattedUrl);

                switch (url)
                {
                    case Constants.USER_ALL_URL:
                        obj = res.ToObject<List<User>>();
                        break;
                    case Constants.USER_BY_ID_URL:
                        obj = res.ToObject<User>();
                        break;
                    case Constants.PROFILE_BY_USERID_URL:
                        obj = res.ToObject<List<Profile>>();
                        break;
                    case Constants.USER_BY_USERNAME_URL:
                        obj = res.ToObject<List<User>>()[0];
                        break;
                    case Constants.PROFILE_BY_ID_URL:
                        obj = res.ToObject<Profile>();
                        break;
                    case Constants.ACTION_BY_ID_URL:
                        obj = res.ToObject<Data.Action>();
                        break;
                    case Constants.ACTION_ALL_URL:
                        obj = res.ToObject<List<Data.Action>>();
                        break;
                    case Constants.COMMAND_BY_ID_URL:
                        obj = res.ToObject<Command>();
                        break;
                    case Constants.COMMAND_ALL_URL:
                        obj = res.ToObject<List<Command>>();
                        break;
                }
            }
            catch (Exception e)
            {
                //OK Just Return NULL
            }

            return (T)Convert.ChangeType(obj, typeof(T));

        }
    }
}
