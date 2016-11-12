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

        public static ObservableCollection<string> HUB_POINTS = new ObservableCollection<string> { DEVICES_TEXT, MEDIA_TEXT, DRONE_TEXT, USER_TEXT };

        public const string BASE_URL = "http://localhost:8080/";

        public const string USER_BY_ID_URL = BASE_URL + "user/id/";
        public const string USER_UPDATE_URL = BASE_URL + "user/update";
        public const string USER_ADD_URL = BASE_URL + "user/add";
        public const string USER_DELETE_URL = BASE_URL + "user/delete/";
        public const string USER_ALL_URL = BASE_URL + "user/all";
        public const string USER_BY_USERNAME_URL = BASE_URL + "user/name/";

        public const string PROFILE_BY_USERID_URL = BASE_URL + "user/userid/";
    }
}
