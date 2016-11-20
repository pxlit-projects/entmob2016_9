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

            if (ApplicationSettings.Contains("ActiveSensor"))
            {

            }
        }

        private void LoadCommands()
        {
            NavigateCommand = new CustomCommand(NavigateAction);
            LoginCommand = new CustomCommand(LoginAction);
            RegisterCommand = new CustomCommand(RegisterAction);
        }

        public async void NavigateAction(object param)
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
                    await ErrorService.showError();
                    break;
            }
        }

        public async void LoginAction(object param)
        {
            if (!IsLoggedIn)
            {
                if (!UserName.Equals(""))
                {
                    if (myUser == null)
                    {
                        IsNotBusy = false;

                        User u = new User
                        {
                            userName = UserName,
                            password = CustomPasswordIncriptor.sha256_hash(PassWord, UserName)
                        };

                        string b = await Services.JSonParseService2<string>.SerializeDataToJson(Constants.CHECK_PASSWORD, u, SerializeType.Post);

                        if (b.Equals("1"))
                        {
                            myUser = await Services.JSonParseService2<User>.DeserializeDataFromJson(Constants.USER_BY_USERNAME_URL, UserName);
                            IsLoggedIn = true;

                            UserName = string.Empty;
                            PassWord = string.Empty;

                            LogButtonText = "Log out";

                            ApplicationSettings.AddItem("CurrentUser", myUser);
                        }
                        else if(b.Equals("2"))
                        {
                            await ErrorService.showError("Username and password did not match.");
                        }
                        else
                        {
                            await ErrorService.showError(b);
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

                if (ApplicationSettings.Contains("MediaPlayer"))
                {
                    MusicPlayer player = (MusicPlayer)ApplicationSettings.GetItem("MediaPlayer");
                    player.Stop();
                    ApplicationSettings.Remove("MediaPlayer");
                }

                ApplicationSettings.Remove("ActiveSensor");
            }
        }

        public void RegisterAction(object param)
        {
            RegisterService.CreateRegisterDialog();
        }

        
    }
}
