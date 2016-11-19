using FanaticFirefly.Data;
using FanaticFirefly.Enumerations;
using FanaticFirefly.Helpers;
using FanaticFirefly.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FanaticFirefly.ViewModels
{
    class UsersViewModel : BaseModel
    {

        private ObservableCollection<User> users;
        public ObservableCollection<User> Users
        {
            get { return users; }
            set { SetProperty(ref users, value); }
        }

        private User selectedUser;
        public User SelectedUser
        {
            get { return selectedUser; }
            set {
                SetProperty(ref selectedUser, value);
                if (SelectedUser != null)
                {
                    OpenUser();
                }
            }
        }

        public UsersViewModel()
        {
            Init();
        }

        private async void Init()
        {
            Users = new ObservableCollection<User>(await Services.JsonParseService <List<User>>.DeserializeDataFromJson(Constants.USER_ALL_URL, null));
            Debug.WriteLine("funfunfun");
        }

        public ICommand EditCommand
        {
            get;
            set;
        }

        private async void OpenUser()
        {
            if (ApplicationSettings.Contains("SelectedUser"))
            {
                ApplicationSettings.Edit("SelectedUser", SelectedUser);
            }
            else
            {
                ApplicationSettings.AddItem("SelectedUser", SelectedUser);
            }
            var v = (NavigationPage)App.Current.MainPage;
            await v.PushAsync(new UserPage());
        }
    }
}
