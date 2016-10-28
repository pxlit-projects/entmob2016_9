using EuphoricElephant.Data;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;

namespace EuphoricElephant.Services
{
    public class JsonParseService<T>
    {

        private static String url = "http://localhost/";


        public static async Task<T> DeserializeDataFromJson(String type, int id)
        {
            try
            {
                JsonArray res = null;

                switch (type)
                {
                    case "user":
                        url += "user/" + id;
                        res = await Deserialize(url);
                        User user = new User();

                        for (uint i = 0; i < res.Count; i++)
                        {
                            user.UserId = (int)res.GetObjectAt(i).GetNamedNumber("userId");
                            user.FirstName = res.GetObjectAt(i).GetNamedString("firstName");
                            user.LastName = res.GetObjectAt(i).GetNamedString("lastName");
                            user.Password = res.GetObjectAt(i).GetNamedString("password");
                        }

                        return (T)Convert.ChangeType(user, typeof(T));

                    case "profile":
                        url += "profile/" + id;
                        res = await Deserialize(url);
                        Profile profile = new Profile();

                        for (uint i = 0; i < res.Count; i++)
                        {
                            profile.ProfileId = (int)res.GetObjectAt(i).GetNamedNumber("profileId");
                            profile.UserId = (int)res.GetObjectAt(i).GetNamedNumber("userId");
                            profile.ActId = (int)res.GetObjectAt(i).GetNamedNumber("actionId");
                            profile.CommandId = (int)res.GetObjectAt(i).GetNamedNumber("commandId");
                            profile.ProfileName = res.GetObjectAt(i).GetNamedString("profileName");
                        }

                        return (T)Convert.ChangeType(profile, typeof(T));

                    case "commands":
                        url += "command/" + id;
                        res = await Deserialize(url);
                        Command command = new Command();

                        command.CommandId = (int)res.GetObjectAt(0).GetNamedNumber("commandId");

                        for (uint i = 1; i < res.Count; i++)
                        {
                            command.CommandList[(int)i] = (int)res.GetObjectAt(i).GetNamedNumber("command" + i);
                        }

                        return (T)Convert.ChangeType(command, typeof(T));

                    case "actions":
                        url += "action/" + id;
                        res = await Deserialize(url);
                        Data.Action action = new Data.Action();

                        action.ActId = (int)res.GetObjectAt(0).GetNamedNumber("actionId");

                        for (uint i = 0; i < res.Count; i++)
                        {
                            action.ActionList[(int)i] = (int)res.GetObjectAt(i).GetNamedNumber("action" + i);
                        }

                        return (T)Convert.ChangeType(action, typeof(T));

                    case "users":
                        url += "users";
                        res = await Deserialize(url);
                        JsonArray users = res.GetObjectAt(0).GetNamedArray("users");

                        List<User> userList = new List<User>();

                        User tempUser = new User();

                        for (uint i = 0; i < res.Count; i++)
                        {
                            tempUser.UserId = (int)users.GetObjectAt(i).GetNamedNumber("userId");
                            tempUser.FirstName = users.GetObjectAt(i).GetNamedString("firstName");
                            tempUser.LastName = users.GetObjectAt(i).GetNamedString("lastName");
                            tempUser.Password = users.GetObjectAt(i).GetNamedString("password");
                            userList[(int)i] = tempUser;
                        }

                        return (T)Convert.ChangeType(userList, typeof(T));

                    case "profiles":
                        url += "profiles";
                        res = await Deserialize(url);
                        JsonArray profiles = res.GetObjectAt(0).GetNamedArray("profiles");

                        List<Profile> profileList = new List<Profile>();

                        Profile tempProfile = new Profile();

                        for (uint i = 0; i < res.Count; i++)
                        {
                            tempProfile.ProfileId = (int)res.GetObjectAt(i).GetNamedNumber("profileId");
                            tempProfile.UserId = (int)res.GetObjectAt(i).GetNamedNumber("userId");
                            tempProfile.ActId = (int)res.GetObjectAt(i).GetNamedNumber("actionId");
                            tempProfile.CommandId = (int)res.GetObjectAt(i).GetNamedNumber("commandId");
                            tempProfile.ProfileName = res.GetObjectAt(i).GetNamedString("profileName");
                        }

                        return (T)Convert.ChangeType(profileList, typeof(T));

                    default:
                        return (T)Convert.ChangeType(res, typeof(T));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static async Task<JsonArray> Deserialize(String url)
        {
            JsonArray res;
            var client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync(url);
            var data = await response.Content.ReadAsStringAsync();
            res = JsonValue.Parse(data).GetArray();

            return res;
        }

        public static async void SerializeDataToJson(String type, Object data)
        {
            try
            {
                switch (type)
                {
                    case "profile":
                        url += "profile";
                        await Serialize(url, data);
                        break;

                    case "user":
                        url += "user";
                        await Serialize(url, data);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static async Task<JsonObject> Serialize(String url, Object data)
        {
            var client = new HttpClient();
            var serealizedfile = JsonConvert.SerializeObject(data);
            var content = new StringContent(serealizedfile.ToString(), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);

            response.EnsureSuccessStatusCode();

            string reply = await response.Content.ReadAsStringAsync();
            return await Task.Run(() => JsonObject.Parse(reply));
        }
    }
}
