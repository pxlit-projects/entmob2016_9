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

        public const string DEVICES_TEXT = "Devices";
        public const string MEDIA_TEXT = "Media Player";
        public const string DRONE_TEXT = "Drones";
        public const string USER_TEXT = "My eMotion";

        public static ObservableCollection<string> HUB_POINTS = new ObservableCollection<string> { DEVICES_TEXT, MEDIA_TEXT, DRONE_TEXT, USER_TEXT };
    }
}
