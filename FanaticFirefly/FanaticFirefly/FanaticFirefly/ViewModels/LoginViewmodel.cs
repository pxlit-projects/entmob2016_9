using FanaticFirefly.Helpers;
using FanaticFirefly.Services;
using FanaticFirefly.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FanaticFirefly.ViewModels
{
    public class LoginViewmodel : BaseModel
    {
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

        private void LoginAction(object obj)
        {
            var main = (NavigationPage)Application.Current.MainPage;
            ApplicationSettings.AddItem("ErrorMessage", "Error");
            NavigationService.Navigate(main, new ErrorPage());

            //ErrorService.ShowError();
        }
    }
}
