using FanaticFirefly.Data;
using FanaticFirefly.Enumerations;
using FanaticFirefly.Helpers;
using FanaticFirefly.Services;
using FanaticFirefly.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FanaticFirefly.ViewModels
{
    public class LoginViewmodel : BaseModel
    {
        private User myUser = null;

        private INavigation Navigation;

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

        public Xamarin.Forms.Command LoginCommand { get; set; }

        public LoginViewmodel()
        {
            LoadCommands();
        }

        private void LoadCommands()
        {
            //LoginCommand = new Xamarin.Forms.Command(async () => await LoginAction());
            LoginCommand = new Xamarin.Forms.Command(LoginAction);
        }

        private async void LoginAction(object obj)
        {
            var main = (NavigationPage)Application.Current.MainPage;
          /*  ApplicationSettings.AddItem("ErrorMessage", "Error");
            NavigationService.Navigate(main, new ErrorPage());*/

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
                            IsLoggedIn = true;

                            UserName = string.Empty;
                            PassWord = string.Empty;

                            Debug.WriteLine("Success");
                            ApplicationSettings.AddItem("CurrentUser", myUser);;

                            await Navigation.PushAsync(new UserPage());
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

            //ErrorService.ShowError();
        }
    }
}
