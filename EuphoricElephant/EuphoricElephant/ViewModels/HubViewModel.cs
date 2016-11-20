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
        #region Properties
        private ObservableCollection<String> hubPoints;

        public ObservableCollection<String> HubPoints
        {
            get { return hubPoints; }
            set { SetProperty(ref hubPoints, value); }
        }
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
        #endregion

        #region Commands
        public ICommand NavigateCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        #endregion

        #region Constructor
        public HubViewModel()
        {
            Init();
        }
        #endregion

        #region Init
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
        #endregion

        #region Private Actions
        private void NavigateAction(object param)
        {
            Frame frame = (Frame)Window.Current.Content;
            NavigationService.Navigate(frame, (string)param);
        }

        private async void LoginAction(object param)
        {
            if (!IsLoggedIn)
            {
                if (!UserName.Equals(""))
                {
                    if (myUser == null)
                    {
                        IsNotBusy = false;
                        await Login();
                    }
                }
            }
            else
            {
                Logout();
            }
        }

        private void RegisterAction(object param)
        {
            RegisterService.CreateRegisterDialog();
        }
        #endregion

        #region Private Methods
        private void Logout()
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

        private async Task<bool> Login()
        {
            bool succes = false;

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
                succes = true;
            }
            else if (b.Equals("2"))
            {
                await ErrorService.showError("Username and password did not match.");
            }
            else
            {
                await ErrorService.showError(b);
            }

            IsNotBusy = true;

            return succes;
        }
        #endregion
    }
}
