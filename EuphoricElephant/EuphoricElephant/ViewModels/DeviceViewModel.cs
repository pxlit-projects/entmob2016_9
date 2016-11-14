using EuphoricElephant.Custom;
using EuphoricElephant.Helpers;
using EuphoricElephant.Messaging;
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
using Windows.UI.Xaml.Controls;

namespace EuphoricElephant.ViewModels
{
    public class DeviceViewModel : BaseModel
    {

        #region Commands
        public ICommand UnpairCommand { get; set; }
        #endregion

        private ObservableCollection<DeviceInformation> sensors;
        private ObservableCollection<DeviceInformation> drones;

        private DeviceInformation selectedTag;
        private DeviceInformation selectedDrone;

        private string selectedIndex = null;

        private Frame frame;

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

        public ObservableCollection<DeviceInformation> Sensors
        {
            get { return sensors; }
            set { SetProperty(ref sensors, value); }
        }

        public ObservableCollection<DeviceInformation> Drones
        {
            get { return drones; }
            set { SetProperty(ref drones, value); }
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
            RegisterMessages();
        }

        private void RegisterMessages()
        {
            Messenger.Default.Register<NavigationMessage>(this, OnNavigationMessageRecieved);
        }

        private void OnNavigationMessageRecieved(NavigationMessage m)
        {
            if (m.Type == Enumerations.ViewType.DeviceViewType)
            {
                frame = m.Frame;
                Init();
            }
        }

        private async void Init()
        {
            LoadCommands();


            Sensors = new ObservableCollection<DeviceInformation>();
            Drones = new ObservableCollection<DeviceInformation>();


            foreach (DeviceInformation d in (await SensorTagService.FindAllTagNames()))
            {
                switch (d.Name)
                {
                    case "CC2650 SensorTag":
                        Sensors.Add(d);
                        break;
                    case "MX Master":
                        Drones.Add(d);
                        break;
                }
            }

            if (Sensors.Count() == 0 && Drones.Count() == 0)
            {
                showError("No devices found.");
            }
        }

        private async void showError(string error)
        {
            var dialog = new Windows.UI.Popups.MessageDialog(error);

            dialog.Commands.Add(new Windows.UI.Popups.UICommand("OK") { Id = 0 });

            var result = await dialog.ShowAsync();

            if (Convert.ToUInt32(result.Id.ToString()) == 0)
            {
                frame.GoBack();
            }
        }
    }
}
