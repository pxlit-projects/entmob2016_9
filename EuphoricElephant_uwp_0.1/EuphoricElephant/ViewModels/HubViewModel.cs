using EuphoricElephant.Custom;
using EuphoricElephant.Enumerations;
using EuphoricElephant.Helpers;
using EuphoricElephant.Messaging;
using EuphoricElephant.Models;
using EuphoricElephant.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace EuphoricElephant.ViewModels
{
    public class HubViewModel : BaseModel
    {
        private ObservableCollection<string> hubPoints;

        public ObservableCollection<string> HubPoints
        {
            get { return hubPoints; }
            set { SetProperty(ref hubPoints, value); }
        }

        public ICommand NavigateCommand { get; set; }

        public HubViewModel()
        {
            Init();
        }

        private void Init()
        {
            HubPoints = Constants.HUB_POINTS;
            LoadCommands();
        }

        private void LoadCommands()
        {
            NavigateCommand = new CustomCommand(NavigateAction);
        }

        public void NavigateAction(object param)
        {
            var frame = (Frame)Window.Current.Content;

            switch ((string)param)
            {
                case Constants.DEVICES_TEXT:
                    frame.Navigate(typeof(DeviceView));
                    break;
                case Constants.MEDIA_TEXT:
                    frame.Navigate(typeof(MediaView));
                    break;
                case Constants.DRONE_TEXT:
                    frame.Navigate(typeof(DroneView));
                    break;
                case Constants.USER_TEXT:
                    frame.Navigate(typeof(UserView));
                    break;
                default:
                    //throw error
                    break;
            }
        }
    }
}
