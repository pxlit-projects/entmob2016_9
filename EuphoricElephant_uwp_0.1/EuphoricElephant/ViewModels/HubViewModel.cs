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
            if (!IsLoggedIn)
            {
                if (!UserName.Equals(""))
                {
                    if (myUser == null)
                    {
                        IsNotBusy = false;

                        User v = await Services.JsonParseService<User>.DeserializeDataFromJson("user", userName);

                        if (v.userName != null)
                        {
                            User u = await Services.JsonParseService<User>.DeserializeDataFromJson("user", v.userId);

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

        public async void RegisterAction(object param)
        {
            var dialog = new ContentDialog()
            {
                Title = "Create new account"
            };
            

            var grid = new Grid();

            var userNameTB = new TextBox();
            var passWordTB = new TextBox();
            var repeatpassWordTb = new TextBox();
            var fnTB = new TextBox();
            var lnTB = new TextBox();

            var uNT = new TextBlock { Text = "User Name" };
            var pwT = new TextBlock { Text = "Password" };
            var rpwT = new TextBlock { Text = "Repeat Password" };
            var fnT = new TextBlock { Text = "First Name" };
            var lnT = new TextBlock { Text = "Last Name" };
            
            ColumnDefinition col1 = new ColumnDefinition();
            ColumnDefinition col2 = new ColumnDefinition();
            col1.Width = new GridLength(200, GridUnitType.Pixel);
            col2.Width = new GridLength(200, GridUnitType.Pixel);
            grid.ColumnDefinitions.Add(col1);
            grid.ColumnDefinitions.Add(col2);
            RowDefinition row1 = new RowDefinition();
            RowDefinition row2 = new RowDefinition();
            RowDefinition row3 = new RowDefinition();
            RowDefinition row4 = new RowDefinition();
            RowDefinition row5 = new RowDefinition();
            row1.Height = new GridLength(0, GridUnitType.Auto);
            row2.Height = new GridLength(0, GridUnitType.Auto);
            row3.Height = new GridLength(0, GridUnitType.Auto);
            row4.Height = new GridLength(0, GridUnitType.Auto);
            row5.Height = new GridLength(0, GridUnitType.Auto);
            grid.RowDefinitions.Add(row1);
            grid.RowDefinitions.Add(row2);
            grid.RowDefinitions.Add(row3);
            grid.RowDefinitions.Add(row4);
            grid.RowDefinitions.Add(row5);

            grid.Children.Add(uNT);
            grid.Children.Add(pwT);
            grid.Children.Add(rpwT);
            grid.Children.Add(fnT);
            grid.Children.Add(lnT);

            grid.Children.Add(userNameTB);
            grid.Children.Add(passWordTB);
            grid.Children.Add(repeatpassWordTb);
            grid.Children.Add(fnTB);
            grid.Children.Add(lnTB);

            Grid.SetColumn(uNT, 0);
            Grid.SetColumn(userNameTB, 1);
            Grid.SetRow(uNT, 0);
            Grid.SetRow(userNameTB, 0);

            Grid.SetColumn(lnT, 0);
            Grid.SetColumn(lnTB, 1);
            Grid.SetRow(lnT, 1);
            Grid.SetRow(lnTB, 1);

            Grid.SetColumn(fnT, 0);
            Grid.SetColumn(fnTB, 1);
            Grid.SetRow(fnT, 2);
            Grid.SetRow(fnTB, 2);

            Grid.SetColumn(pwT, 0);
            Grid.SetColumn(passWordTB, 1);
            Grid.SetRow(pwT, 3);
            Grid.SetRow(passWordTB, 3);

            Grid.SetColumn(rpwT, 0);
            Grid.SetColumn(repeatpassWordTb, 1);
            Grid.SetRow(rpwT, 4);
            Grid.SetRow(repeatpassWordTb, 4);


            dialog.Content = grid;

            dialog.PrimaryButtonText = "OK";

            dialog.SecondaryButtonText = "Cancel";

            dialog.PrimaryButtonClick += async delegate
            {
                if (userNameTB.Text != "" && passWordTB.Text != "" && repeatpassWordTb.Text != "" && fnTB.Text != "" && lnTB.Text != "")
                {
                    if (passWordTB.Text.Equals(repeatpassWordTb.Text))
                    {
                        User tempUser = await Services.JsonParseService<User>.DeserializeDataFromJson("user", userNameTB.Text);

                        if (tempUser.userName == null || tempUser.userName == string.Empty)
                        {
                            try
                            {
                                User newUser = new User
                                {
                                    userName = userNameTB.Text,
                                    lastName = lnTB.Text,
                                    firstName = fnTB.Text,
                                    password = CustomPasswordIncriptor.sha256_hash(passWordTB.Text, userNameTB.Text),
                                    defaultProfileId = 0
                                };
                                bool succes =  await Services.JsonParseService<User>.SerializeDataToJson("user", newUser);

                                if (succes)
                                {
                                    var u = await Services.JsonParseService<User>.DeserializeDataFromJson("user", newUser.userName);

                                    Debug.WriteLine("id= " + u.userId);

                                    Profile newProfile = new Profile()
                                    {
                                        profileName = "Default Profile",
                                        userId = u.userId,
                                        pairings = "[(1,2,3,4,5),(1,2,3,4,5)]" //TODO dynamic
                                    };

                                    succes =  await Services.JsonParseService<Profile>.SerializeDataToJson("profile", newProfile);

                                    if (succes)
                                    {
                                        await Services.JsonParseService<User>.DeserializeDataFromJson("user/profile", newUser.userName);
                                    }
                                }

                                
                            }
                            catch (Exception e)
                            {
                                Debug.WriteLine(e.Message);
                            }
                        }
                        else
                        {
                            showError("User already exists.");
                            
                        }
                    }
                    else
                    {
                        showError("Passwords do not match.");
                    }
                }
                else
                {
                    showError("Please fill in all fields.");
                }
            };

            // Show Dialog
            var result = await dialog.ShowAsync();
        }

        private async void showError(string error)
        {
            var dialog = new Windows.UI.Popups.MessageDialog(error);

            dialog.Commands.Add(new Windows.UI.Popups.UICommand("OK") { Id = 0 });

            var result = await dialog.ShowAsync();

            if(Convert.ToUInt32(result.Id.ToString()) == 0)
            {
                RegisterAction(null);
            }
        }
    }
}
