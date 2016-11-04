﻿using System;
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

        public const string BASE_URL = "http://localhost:8000/DynasticDinosaur/";
    }
}
