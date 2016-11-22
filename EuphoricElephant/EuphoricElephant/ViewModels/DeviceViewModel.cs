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

        #region Properties
        private ObservableCollection<DeviceInformation> sensors;
        private ObservableCollection<DeviceInformation> drones;

        private DeviceInformation selectedTag;
        private DeviceInformation selectedDrone;

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
                if (value != null)
                {
                    SetProperty(ref selectedTag, value);
                    activeSensor = new SensorTag
                    {
                        Info = value
                    };

                    if (!ApplicationSettings.Contains("ActiveSensor"))
                    {
                        ApplicationSettings.AddItem("ActiveSensor", activeSensor);
                        showError("SensorTag succesfully paired.");
                    }
                    else
                    {
                        showMessage("SensorTag was already paired, unpair to select another");
                    }

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
        #endregion

        #region Private Variables
        private string selectedIndex = null;
        private Frame frame;
        private SensorTag activeSensor;
        #endregion 

        #region Actions
        private void UnpairAction(object obj)
        {
            SelectedTag = null;
            activeSensor = null;
        }
        #endregion

        #region Constructor
        public DeviceViewModel()
        {
            LoadCommands();
            RegisterMessages();
        }
        #endregion

        #region Init
        private bool Init()
        {
            bool succes = false;

            
            if (LoadData())
            {
                succes = true;
            }

            return succes;
        }

        private void LoadCommands()
        {
            UnpairCommand = new CustomCommand(UnpairAction);
        }
        #endregion

        #region Messaging
        private void RegisterMessages()
        {
            Messenger.Default.Register<NavigationMessage>(this, OnNavigationMessageRecieved);
        }

        private void OnNavigationMessageRecieved(NavigationMessage m)
        {
            if (m.Type == Enumerations.ViewType.DeviceViewType)
            {
                frame = m.Frame;
                if (!Init())
                {
                    showError("no devices found");
                }
            }
        }
        #endregion

        #region Error
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

        private void showMessage(string message)
        {
            Task.Run(() => ErrorService.showError(message));
        }
        #endregion

        #region Private Methods
        private bool LoadData()
        {
            bool succes = false;

            Sensors = new ObservableCollection<DeviceInformation>();
            Drones = new ObservableCollection<DeviceInformation>();

            foreach (DeviceInformation d in (Task.Run(()=> SensorTagService.FindAllTagNames())).Result)
            {
                if (d.Name.ToLower().Contains("sensortag"))
                {
                    Sensors.Add(d);
                }
                else if (d.Name.ToLower().Contains("drone"))
                {
                    Drones.Add(d);
                }
            }

            if (!((Sensors.Count() == 0 && Drones.Count() == 0)))
            {
                succes = true;
            }

            return succes;
        }
        #endregion
    }
}
