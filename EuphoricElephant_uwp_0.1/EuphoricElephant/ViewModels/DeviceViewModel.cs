using EuphoricElephant.Helpers;
using EuphoricElephant.Model;
using EuphoricElephant.Models;
using EuphoricElephant.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;

namespace EuphoricElephant.ViewModels
{
    public class DeviceViewModel : BaseModel
    {
        private ObservableCollection<DeviceInformation> devices;

        private DeviceInformation selectedTag;
        private DeviceInformation selectedDrone;

        public ObservableCollection<DeviceInformation> Devices
        {
            get { return devices; }
            set { SetProperty(ref devices, value); }
        }

        public DeviceInformation SelectedTag
        {
            get { return selectedTag; }
            set
            {
                SetProperty(ref selectedTag, value);
                activeSensor = new SensorTag
                {
                    Info = value
                };
                ApplicationSettings.AddItem("ActiveSensor", activeSensor);
            }
        }

        public DeviceInformation SelectedDrone
        {
            get { return selectedDrone; }
            set
            {
                SetProperty(ref selectedDrone, value);               
            }
        }

        private SensorTag activeSensor;

        public DeviceViewModel()
        {
            Init();
        }

        private async void Init()
        {
            Devices = new ObservableCollection<DeviceInformation>(await SensorTagService.FindAllTagNames());
        }
    }
}
