using FanaticFirefly.Data;
using FanaticFirefly.Enumerations;
using FanaticFirefly.Helpers;
using FanaticFirefly.Services;
using FanaticFirefly.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace FanaticFirefly.ViewModels
{
    public class LoginViewmodel : BaseModel
    {
        private User myUser = null;

        private string loginText = "Login";
        public string LoginText
        {
            get { return loginText; }
            set { SetProperty(ref loginText, value); }
        }

        private string userName = string.Empty;
        public string UserName
        {
            get { return userName; }
            set { SetProperty(ref userName, value); }
        }

        private string passWord = string.Empty;
        public string PassWord
        {
            get { return passWord; }
            set { SetProperty(ref passWord, value); }
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

        public ICommand LoginCommand { get; set; }
        public ICommand ShowUsersCommand { get; set; }

        public LoginViewmodel()
        {
            LoadCommands();
        }

        private void LoadCommands()
        {
            LoginCommand = new Xamarin.Forms.Command(LoginAction);
            ShowUsersCommand = new Xamarin.Forms.Command(ShowUsersAction);
        }

        private async void ShowUsersAction(object obj)
        {
            var v = (NavigationPage)App.Current.MainPage;
            await v.PushAsync(new UsersPage());
        }

        private async void LoginAction(object obj)
        {
            try
            {
                var main = (NavigationPage)Application.Current.MainPage;

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
                                password = EasyEncryption.SHA.ComputeSHA256Hash(PassWord + UserName)
                            };

                            string b = await Services.JsonParseService<string>.SerializeDataToJson(Constants.CHECK_PASSWORD, u, SerializeType.Post);

                            if (b.Equals("1"))
                            {
                                myUser = await Services.JsonParseService<User>.DeserializeDataFromJson(Constants.USER_BY_USERNAME_URL, UserName);

                                if(myUser != null)
                                {
                                    IsLoggedIn = true;

                                    UserName = string.Empty;
                                    PassWord = string.Empty;
                                    ApplicationSettings.AddItem("CurrentUser", myUser);

                                    LoginText = "Logout";
                                }
                                else
                                {
                                    ErrorService.ShowError();
                                }
                            }
                            else if (b.Equals("2"))
                            {
                                ErrorService.ShowError("Username and password did not match.");
                            }
                            else
                            {
                                ErrorService.ShowError(b);
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
                }
            }
            catch (Exception e)
            {
                ErrorService.ShowError(e.Message);
            }
        }

    }
}
