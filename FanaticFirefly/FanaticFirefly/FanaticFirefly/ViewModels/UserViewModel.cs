using FanaticFirefly.Data;
using FanaticFirefly.Helpers;
using FanaticFirefly.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FanaticFirefly.ViewModels
{
    class UserViewModel : BaseModel
    {
        private string userName = string.Empty;
        public string UserName
        {
            get { return userName; }
            set { SetProperty(ref userName, value); }
        }

        private string firstName = string.Empty;
        public string FirstName
        {
            get { return firstName; }
            set { SetProperty(ref firstName, value); }
        }

        private string lastName = string.Empty;
        public string LastName
        {
            get { return lastName; }
            set { SetProperty(ref lastName, value); }
        }

        public UserViewModel()
        {
            var user = (User)ApplicationSettings.GetItem("CurrentUser");
            UserName = user.userName;
            FirstName = user.firstName;
            LastName = user.lastName;

            LoadCommands();
        }

        private void LoadCommands()
        {            
            EditCommand = new CustomCommand(EditAction);
        }

        private async void EditAction(object o)
        {
            var v = (NavigationPage)App.Current.MainPage;
            await v.PushAsync(new UserPage());
        }

        public ICommand EditCommand
        {
            get;
            set;
        }
    }
}
