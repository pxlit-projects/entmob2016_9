using EuphoricElephant.Custom;
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
using System.Windows.Input;
using Windows.Devices.Enumeration;

namespace EuphoricElephant.ViewModels
{
    public class DeviceViewModel : BaseModel
    {

        #region Commands
        public ICommand UnpairCommand { get; set; }
        #endregion

        private ObservableCollection<DeviceInformation> devices;

        private DeviceInformation selectedTag;
        private DeviceInformation selectedDrone;

        private string selectedIndex = null;

        private void LoadCommands()
        {
            UnpairCommand = new CustomCommand(UnpairAction);
        }

        private void UnpairAction(object obj)
        {
            SelectedTag = null;
            activeSensor = null;
        }

        public string SelectedIndex
        {
            get { return selectedIndex; }
            set { SetProperty(ref selectedIndex, value); }
        }

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
                if( value != null)
                {
                    SetProperty(ref selectedTag, value);
                    activeSensor = new SensorTag
                    {
                        Info = value
                    };
                    ApplicationSettings.AddItem("ActiveSensor", activeSensor);
                }
                else
                {
                    SetProperty(ref selectedTag, value);
                    ApplicationSettings.Remove("ActiveSensor");
                }
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
            LoadCommands();
            Devices = new ObservableCollection<DeviceInformation>(await SensorTagService.FindAllTagNames());
        }
    }
}
