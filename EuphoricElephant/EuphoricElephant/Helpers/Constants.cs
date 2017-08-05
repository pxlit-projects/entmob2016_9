using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuphoricElephant.Helpers
{
    public static class Constants
    {
        public static string BASE_ID = "f000{0}-0451-4000-b000-000000000000";
        public static string SERVICE_ID = "0000{0}-0000-1000-8000-00805f9b34fb";

        public const string DEVICES_TEXT = "/Assets/connect.png";
        public const string MEDIA_TEXT = "/Assets/music.png";
        public const string DRONE_TEXT = "/Assets/drone.png";
        public const string USER_TEXT = "/Assets/user.png";

        //public static ObservableCollection<string> HUB_POINTS = new ObservableCollection<string> { DEVICES_TEXT, MEDIA_TEXT, DRONE_TEXT, USER_TEXT };
        public static ObservableCollection<string> HUB_POINTS = new ObservableCollection<string> { DEVICES_TEXT, MEDIA_TEXT, USER_TEXT };


        public const string BASE_URL = "http://localhost:8080/zanyzebra/";

        public const string USER_BY_ID_URL = BASE_URL + "user/id/";
        public const string USER_UPDATE_URL = BASE_URL + "user/update";
        public const string USER_ADD_URL = BASE_URL + "user/add";
        public const string USER_DELETE_URL = BASE_URL + "user/delete/";
        public const string USER_ALL_URL = BASE_URL + "user/all";
        public const string USER_BY_USERNAME_URL = BASE_URL + "user/name/";
        public const string PROFILE_BY_ID_URL = BASE_URL + "profile/id/";
        public const string PROFILE_UPDATE_URL = BASE_URL + "profile/update";
        public const string PROFILE_ADD_URL = BASE_URL + "profile/add";
        public const string PROFILE_DELETE_URL = BASE_URL + "profile/delete/";
        public const string PROFILE_BY_USERID_URL = BASE_URL + "profile/userid/";
        public const string ACTION_BY_ID_URL = BASE_URL + "action/id/";
        public const string ACTION_UPDATE_URL = BASE_URL + "action/update";
        public const string ACTION_ADD_URL = BASE_URL + "action/add";
        public const string ACTION_DELETE_URL = BASE_URL + "action/delete/";
        public const string ACTION_ALL_URL = BASE_URL + "action/all";
        public const string COMMAND_BY_ID_URL = BASE_URL + "command/id/";
        public const string COMMAND_UPDATE_URL = BASE_URL + "command/update";
        public const string COMMAND_ADD_URL = BASE_URL + "command/add";
        public const string COMMAND_DELETE_URL = BASE_URL + "command/delete/";
        public const string COMMAND_ALL_URL = BASE_URL + "command/all";
        public const string CHECK_PASSWORD = BASE_URL + "user/pass/";

        public const string DATE_FORMAT = "dd-MM-yyyy";
    }
}
