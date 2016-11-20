using FanaticFirefly.Data;
using FanaticFirefly.Enumerations;
using FanaticFirefly.Helpers;
using FanaticFirefly.Services;
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
        private ObservableCollection<User> InitUsers;


        private ObservableCollection<User> users;
        public ObservableCollection<User> Users
        {
            get { return users; }
            set { SetProperty(ref users, value); }
        }

        private string searchtText = string.Empty;
        public string SearchText
        {
            get { return searchtText; }
            set { SetProperty(ref searchtText, value);
                SearchAction(null);
            }
        }

        private User selectedUser;
        public User SelectedUser
        {
            get { return selectedUser; }
            set {
                SetProperty(ref selectedUser, value);
                if (SelectedUser != null)
                {
                    NavigationService.OpenUser(value, (NavigationPage)App.Current.MainPage);
                }
            }
        }

        public UsersViewModel()
        {
            Init();
            LoadCommands();
        }

        private async void Init()
        {
            InitUsers = new ObservableCollection<User>(await Services.JsonParseService <List<User>>.DeserializeDataFromJson(Constants.USER_ALL_URL, null));
            if(InitUsers != null)
            {
                Users = new ObservableCollection<User>(InitUsers);
            }
            else
            {
                ErrorService.ShowError(ViewType.UsersView);
            }
        }

        private void LoadCommands()
        {
            SortByUserNameCommand = new CustomCommand(SortByUserNameAction);
            SortByJoinedDateCommand = new CustomCommand(SortByJoinedDateAction);
            SearchCommand = new CustomCommand(SearchAction);
        }

        private void SearchAction(object obj)
        {
            if(InitUsers != null && Users != null)
            {
                if (SearchText.Equals(string.Empty))
                {
                    Users = new ObservableCollection<User>(InitUsers);
                }
                else
                {
                    Users = new ObservableCollection<User>(InitUsers.Where(x => x.userName.Contains(SearchText)));
                }
            }
        }

        private void SortByJoinedDateAction(object obj)
        {
            Users = new ObservableCollection<User>(Users.OrderBy(x => x.joinedOn).ToList());
        }

        private void SortByUserNameAction(object obj)
        {
            Users = new ObservableCollection<User>(Users.OrderBy(x => x.userName).ToList());
        }

        public ICommand EditCommand {get;set;}
        public ICommand SortByUserNameCommand { get; set; }
        public ICommand SortByJoinedDateCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        
    }
}
