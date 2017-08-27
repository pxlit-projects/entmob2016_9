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
            get { return logButtonText; }
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

		private bool isDummyMode = false;
		public bool IsDummyMode
		{
			get { return isDummyMode; }
			set { SetProperty(ref isDummyMode, value); }
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

        private void LoginAction(object param)
        {
            if (!IsLoggedIn)
            {
                if (!UserName.Equals(""))
                {
                    if (myUser == null)
                    {
                        IsNotBusy = false;
                        Login();
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
			ApplicationSettings.Remove("IsDummyMode");
		}

        private void Login()
        {
            User u = new User
            {
                userName = UserName,
                password = CustomPasswordIncriptor.sha256_hash(PassWord, UserName)
            };

            string b = Task.Run(() => Services.JSonParseService2<string>.SerializeDataToJson(Constants.CHECK_PASSWORD, u, SerializeType.Post)).Result;

            if (b.Equals("1"))
            {
                myUser = Task.Run(() => Services.JSonParseService2<User>.DeserializeDataFromJson(Constants.USER_BY_USERNAME_URL, UserName)).Result;
                IsLoggedIn = true;

                UserName = string.Empty;
                PassWord = string.Empty;

                LogButtonText = "Log out";

                ApplicationSettings.AddItem("CurrentUser", myUser);
				ApplicationSettings.AddItem("IsDummyMode", isDummyMode);

			}
            else if (b.Equals("2"))
            {
                Task.Run(() => ErrorService.showError("Username and password did not match."));
            }
            else
            {
                Task.Run(() => ErrorService.showError());
            }

            IsNotBusy = true; ;
        }
        #endregion
    }
}
