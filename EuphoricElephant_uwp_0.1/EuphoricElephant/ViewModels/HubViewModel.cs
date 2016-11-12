using EuphoricElephant.Custom;
using EuphoricElephant.Data;
using EuphoricElephant.Enumerations;
using EuphoricElephant.Helpers;
using EuphoricElephant.Messaging;
using EuphoricElephant.Models;
using EuphoricElephant.Services;
using EuphoricElephant.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace EuphoricElephant.ViewModels
{
    public class HubViewModel : BaseModel
    {
        private string logButtonText = "Log in";

        public string LogButtonText
        {
            get { return logButtonText;}
            set { SetProperty(ref logButtonText, value); }
        }

        private User myUser = null;

        private string userName = string.Empty;

        public string UserName
        {
            get { return userName; }
            set { SetProperty(ref userName, value); }
        }

        private string password = string.Empty;

        public string PassWord
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        private bool isLoggedIn = false;
        public bool IsLoggedIn
        {
            get { return isLoggedIn; }
            set { SetProperty(ref isLoggedIn, value); }
        }

        private bool isNotBusy = true;
        public bool IsNotBusy
        {
            get { return isNotBusy; }
            set { SetProperty(ref isNotBusy, value); }
        }

        private ObservableCollection<String> hubPoints;

        public ObservableCollection<String> HubPoints
        {
            get { return hubPoints; }
            set { SetProperty(ref hubPoints, value); }
        }

        public ICommand NavigateCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }

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
            LoginCommand = new CustomCommand(LoginAction);
            RegisterCommand = new CustomCommand(RegisterAction);
        }

        public void NavigateAction(object param)
        {
            var frame = (Frame)Window.Current.Content;

            switch ((string)param)
            {
                case Constants.DEVICES_TEXT:
                    frame.Navigate(typeof(DeviceView));
                    Messenger.Default.Send<NavigationMessage>(new NavigationMessage(Enumerations.ViewType.DeviceViewType, frame));
                    break;
                case Constants.MEDIA_TEXT:
                    frame.Navigate(typeof(MediaView));
                    Messenger.Default.Send<NavigationMessage>(new NavigationMessage(Enumerations.ViewType.MediaPlayerViewType, frame));
                    break;
                case Constants.DRONE_TEXT:
                    frame.Navigate(typeof(DroneView));
                    Messenger.Default.Send<NavigationMessage>(new NavigationMessage(Enumerations.ViewType.DroneViewType, frame));
                    break;
                case Constants.USER_TEXT:
                    frame.Navigate(typeof(UserView));
                    Messenger.Default.Send<NavigationMessage>(new NavigationMessage(Enumerations.ViewType.UserViewType, frame));
                    break;
                default:
                    //throw error
                    break;
            }
        }

        public async void LoginAction(object param)
        {
            User u = new User
            {
                userId = 1,
                userName = "bleh",
                defaultProfileId = 0,
                firstName = "m",
                lastName = "dd",
                password = "sdlvk"
            };

            var v = await JSonParseService2<bool>.SerializeDataToJson(Constants.USER_UPDATE_URL, u, SerializeType.Put);
            Debug.WriteLine(v.ToString());

            if (!IsLoggedIn)
            {
                if (!UserName.Equals(""))
                {
                    if (myUser == null)
                    {
                        IsNotBusy = false;

                        User user = await Services.JSonParseService2<User>.DeserializeDataFromJson(Constants.USER_BY_USERNAME_URL, userName);

                        if (user.userName != null)
                        {
                            User u = await Services.JsonParseService<User>.DeserializeDataFromJson("user", v.id);

                            if (u.password.Equals(CustomPasswordIncriptor.sha256_hash(PassWord, UserName)))
                            {
                                myUser = u;
                                IsLoggedIn = true;

                                UserName = string.Empty;
                                PassWord = string.Empty;

                                LogButtonText = "Log out";

                                ApplicationSettings.AddItem("CurrentUser", myUser);
                            }
                            else
                            {
                                showError("Username and password did not match.");
                            }
                        }

                        IsNotBusy = true;
                    }
                }
            }
            else
            {
                myUser = null;
                IsLoggedIn = false;

                ApplicationSettings.Remove("CurrentUser");

                UserName = string.Empty;
                PassWord = string.Empty;

                LogButtonText = "Log in";
            }
        }

        public void RegisterAction(object param)
        {
            RegisterService.CreateRegisterDialog();
        }

        
    }
}
