using EuphoricElephant.Data;
using EuphoricElephant.Helpers;
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
        private static String URL = Constants.BASE_URL;
        
        public static async Task<T> DeserializeDataFromJson(String type, int id)
        {
            try
            {
                JsonArray res = null;
                var url = string.Empty;

                switch (type)
                {
                    case "user":
                        url  = URL + "user/id/" + id;
                        res =  await RestService.DeserializeSingle(url) ;
                        User user = new User();

                        for (uint i = 0; i < res.Count; i++)
                        {
                            user.userId = (int)res.GetObjectAt(i).GetNamedNumber("userId");
                            user.firstName = res.GetObjectAt(i).GetNamedString("firstName");
                            user.lastName = res.GetObjectAt(i).GetNamedString("lastName");
                            user.password = res.GetObjectAt(i).GetNamedString("password");
                            user.userName = res.GetObjectAt(i).GetNamedString("userName");
                            user.defaultProfileId = (int)res.GetObjectAt(i).GetNamedNumber("defaultProfileId");
                        }

                        return (T)Convert.ChangeType(user, typeof(T));

                    case "profile/user":
                        url  = URL + "profile/user/" + id;
                        res = await RestService.Deserialize(url);
                        List<Profile> profiles = new List<Profile>();

                        for (uint i = 0; i < res.Count; i++)
                        {
                            Profile profile = new Profile();
                            profile.profileId = (int)res.GetObjectAt(i).GetNamedNumber("profileId");
                            profile.userId = (int)res.GetObjectAt(i).GetNamedNumber("userId");
                            profile.profileName = res.GetObjectAt(i).GetNamedString("profileName");
                            profile.pairings = res.GetObjectAt(i).GetNamedString("pairings");

                            profiles.Add(profile);
                        }

                        return (T)Convert.ChangeType(profiles, typeof(T));

                    case "commands":
                        url  = URL + "command/" + id;
                        res = await RestService.Deserialize(url);
                        Command command = new Command();

                        command.CommandId = (int)res.GetObjectAt(0).GetNamedNumber("commandId");

                        for (uint i = 1; i < res.Count; i++)
                        {
                            //command.CommandList[(int)i] = (int)res.GetObjectAt(i).GetNamedNumber("command" + i);
                        }

                        return (T)Convert.ChangeType(command, typeof(T));

                    case "actions":
                        url  = URL + "action/" + id;
                        res = await RestService.Deserialize(url);
                        Data.Action action = new Data.Action();

                        action.ActId = (int)res.GetObjectAt(0).GetNamedNumber("actionId");

                        for (uint i = 0; i < res.Count; i++)
                        {
                            //action.ActionList[(int)i] = (int)res.GetObjectAt(i).GetNamedNumber("action" + i);
                        }

                        return (T)Convert.ChangeType(action, typeof(T));

                    case "users":
                        url  = URL + "users";
                        res = await RestService.Deserialize(url);

                        List<User> userList = new List<User>();
                        for (uint i = 0; i < res.Count; i++)
                        {
                            User tempUser = new User();

                            tempUser.userId = (int)res.GetObjectAt(i).GetNamedNumber("userId");
                            tempUser.firstName = res.GetObjectAt(i).GetNamedString("firstName");
                            tempUser.lastName = res.GetObjectAt(i).GetNamedString("lastName");
                            tempUser.password = res.GetObjectAt(i).GetNamedString("password");
                            tempUser.defaultProfileId = (int)res.GetObjectAt(i).GetNamedNumber("defaultProfile");

                            userList.Add(tempUser);
                        }

                        return (T)Convert.ChangeType(userList, typeof(T));

                    case "profiles":
                        url  = URL + "profiles";
                        res = await RestService.Deserialize(url);

                        List<Profile> profileList = new List<Profile>();

                        Profile tempProfile = new Profile();

                        for (uint i = 0; i < res.Count; i++)
                        {
                            tempProfile.profileId = (int)res.GetObjectAt(i).GetNamedNumber("profileId");
                            tempProfile.userId = (int)res.GetObjectAt(i).GetNamedNumber("userId");
                            tempProfile.profileName = res.GetObjectAt(i).GetNamedString("profileName");
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

        public static async Task<T> DeserializeDataFromJson(String type, string name)
        {
            try
            {
                JsonArray res = null;

                string url;

                switch (type)
                {
                    case "user":
                        url = URL + "user/name/" + name;
                        res = await RestService.DeserializeSingle(url);
                        User user = new User();

                        if(res != null)
                        {
                            user.userId = Convert.ToInt32(res.GetObjectAt(0).GetNamedNumber("userId"));
                            user.firstName = res.GetObjectAt(0).GetNamedString("firstName");
                            user.lastName = res.GetObjectAt(0).GetNamedString("lastName");
                            user.password = res.GetObjectAt(0).GetNamedString("password");
                            user.userName = res.GetObjectAt(0).GetNamedString("userName");
                        }

                        return (T)Convert.ChangeType(user, typeof(T));
                    case "user/profile":
                        url = URL + "user/profile/" + name;
                        res = await RestService.DeserializeSingle(url);

                        int id = 0;

                        if(res != null)
                        {
                            id = (int)res.GetObjectAt(0).GetNamedNumber("userId");
                        }

                        return (T)Convert.ChangeType(id, typeof(T));
                    default:
                        return (T)Convert.ChangeType(res, typeof(T));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        

        public static async Task<bool> SerializeDataToJson(String type, Object data)
        {
            try
            {
                var url = string.Empty;

                switch (type)
                {
                    case "profile":
                        url  = URL + "profile";
                        await RestService.Serialize(url, data);
                        break;

                    case "user":
                        url  = URL + "users";
                        await RestService.Serialize(url, data);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        
    }
}
