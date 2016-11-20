using FanaticFirefly.Data;
using FanaticFirefly.Helpers;
using FanaticFirefly.Services;
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
    public class UserViewModel : BaseModel
    {
        private string userName = string.Empty;
        public string UserName
        {
            get { return userName; }
            set { SetProperty(ref userName, value); }
        }

        private string email = string.Empty;
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        private string country = string.Empty;
        public string Country
        {
            get { return country; }
            set { SetProperty(ref country, value); }
        }

        private string phone = string.Empty;
        public string Phone
        {
            get { return phone; }
            set { SetProperty(ref phone, value); }
        }

        private string joinedOn = string.Empty;
        public string JoinedOn
        {
            get { return joinedOn; }
            set { SetProperty(ref joinedOn, value); }
        }

        private string defaultProfileName = string.Empty;
        public string DefaultProfileName
        {
            get { return defaultProfileName; }
            set { SetProperty(ref defaultProfileName, value); }
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

        private User user;

        public UserViewModel()
        {
            user = (User)ApplicationSettings.GetItem("SelectedUser");

            if(user != null)
            {
                UserName = user.userName;
                FirstName = user.firstName;
                LastName = user.lastName;
                JoinedOn = user.joinedOn;
                Country = user.country;
                Email = user.email;
                Phone = user.phone;
            }else
            {
                ErrorService.ShowError(Enumerations.ViewType.UserView);
            }
            

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
