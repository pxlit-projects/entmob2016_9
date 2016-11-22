using EuphoricElephant.Helpers;
using EuphoricElephant.Messaging;
using EuphoricElephant.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace EuphoricElephant.Services
{
    public static class NavigationService
    {
        public static async void Navigate(Frame frame, string param)
        {
            switch (param)
            {
                case Constants.DEVICES_TEXT:
                    frame.Navigate(typeof(DeviceView));
                    Messenger.Default.Send<NavigationMessage>(new NavigationMessage(Enumerations.ViewType.DeviceViewType, frame));
                    break;
                case Constants.MEDIA_TEXT:
                    frame.Navigate(typeof(MediaView));
                    Messenger.Default.Send<NavigationMessage>(new NavigationMessage(Enumerations.ViewType.MediaPlayerViewType, frame));
                    break;
                case Constants.USER_TEXT:
                    frame.Navigate(typeof(UserView));
                    Messenger.Default.Send<NavigationMessage>(new NavigationMessage(Enumerations.ViewType.UserViewType, frame));
                    break;
                default:
                    ErrorService.showError();
                    break;
            }
        }
    }
}
