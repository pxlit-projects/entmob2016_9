using EuphoricElephant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuphoricElephant
{
    public class ViewModelLocator
    {
        private static HubViewModel hubViewModel = new HubViewModel();
        private static DeviceViewModel deviceViewModel = new DeviceViewModel();
        private static DroneViewModel droneViewModel = new DroneViewModel();
        private static MediaViewModel mediaViewModel = new MediaViewModel();
        private static UserViewModel userViewModel = new UserViewModel();

        public static HubViewModel HubViewModel
        {
            get
            {
                return hubViewModel;
            }
        }

        public static DeviceViewModel DeviceViewModel
        {
            get
            {
                return deviceViewModel;
            }
        }

        public static DroneViewModel DroneViewModel
        {
            get
            {
                return droneViewModel;
            }
        }

        public static MediaViewModel MediaViewModel
        {
            get
            {
                return mediaViewModel;
            }
        }

        public static UserViewModel UserViewModel
        {
            get
            {
                return userViewModel;
            }
        }
    }
}
