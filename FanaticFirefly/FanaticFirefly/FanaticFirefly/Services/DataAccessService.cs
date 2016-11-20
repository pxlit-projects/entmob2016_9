using FanaticFirefly.Data;
using FanaticFirefly.Enumerations;
using FanaticFirefly.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanaticFirefly.Services
{
    public static class DataAccessService
    {
        public static async Task<string> GetLoginStatus(string UserName, string PassWord)
        {
            User u = new User
            {
                userName = UserName,
                password = GetEncriptedPassword(UserName, PassWord)
            };

            string b = await Services.JsonParseService<string>.SerializeDataToJson(Constants.CHECK_PASSWORD, u, SerializeType.Post);

            return b;
        }

        public static async Task<User> GetLogedInUser(string UserName)
        {
            return await Services.JsonParseService<User>.DeserializeDataFromJson(Constants.USER_BY_USERNAME_URL, UserName);
        }

        public static string GetEncriptedPassword(string UserName, string PassWord)
        {
            return EasyEncryption.SHA.ComputeSHA256Hash(PassWord + UserName);
        }

        public static async Task<ObservableCollection<Profile>> GetProfiles(User currUser)
        {
            return new ObservableCollection<Profile>(await JsonParseService<List<Profile>>.DeserializeDataFromJson(Constants.PROFILE_BY_USERID_URL, Convert.ToString(currUser.userId)));
        }
    }
}
