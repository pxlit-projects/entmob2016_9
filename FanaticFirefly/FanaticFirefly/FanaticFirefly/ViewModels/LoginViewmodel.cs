using FanaticFirefly.Data;
using FanaticFirefly.Enumerations;
using FanaticFirefly.Helpers;
using FanaticFirefly.Services;
using FanaticFirefly.Views;
using System;
using System.Threading.Tasks;
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

        private void LoginAction(object obj)
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
                            Login();
                        }
                    }
                }
                else
                {
                    Logout();
                }
            }
            catch (Exception e)
            {
                ErrorService.ShowError(ViewType.LoginView, e.Message);
            }
            finally
            {
                IsNotBusy = true;
            }
        }

        private void Logout()
        {
            myUser = null;
            IsLoggedIn = false;

            ApplicationSettings.Remove("CurrentUser");

            UserName = string.Empty;
            PassWord = string.Empty;

            LoginText = "Login";
        }

        private async void Login()
        {
            string b = await DataAccessService.GetLoginStatus(UserName, PassWord);

            if (b.Equals("1"))
            {

                myUser = await DataAccessService.GetLoggedInUser(UserName);

                if (myUser != null)
                {
                    IsLoggedIn = true;

                    UserName = string.Empty;
                    PassWord = string.Empty;
                    ApplicationSettings.AddItem("CurrentUser", myUser);

                    LoginText = "Logout";
                }
                else
                {
                    ErrorService.ShowError(ViewType.LoginView);
                }
            }
            else if (b.Equals("2"))
            {
                ErrorService.ShowError(ViewType.LoginView, "Username and password did not match.");
            }
            else
            {
                ErrorService.ShowError(ViewType.LoginView, b);
            }
        }
    }
}
