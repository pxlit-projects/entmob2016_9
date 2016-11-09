using EuphoricElephant.Data;
using EuphoricElephant.Helpers;
using EuphoricElephant.Messaging;
using EuphoricElephant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuphoricElephant.ViewModels
{
    public class UserViewModel : BaseModel
    {
        private User currentUser = null;

        private string userName = string.Empty;

        public string UserName
        {
            get { return userName; }
            set { SetProperty(ref userName, value); }
        }

        private string selectedProfileName = "No Profile selected";

        public string SelectedProfileName
        {
            get { return selectedProfileName; }
            set { SetProperty(ref selectedProfileName, value); }
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

        private bool isEditing = false;

        public bool IsEditing
        {
            get { return isEditing; }
            set { SetProperty(ref isEditing, value); }
        }

        public UserViewModel()
        {
            RefisterMessages();
        }

        private void RefisterMessages()
        {
            Messenger.Default.Register<NavigationMessage>(this, OnNavigationMessageRecieved);
        }

        private void OnNavigationMessageRecieved(NavigationMessage m)
        {
            if(m.Type == Enumerations.ViewType.UserViewType)
            {
                Init();
            }
        }

        private void Init()
        {
            if (ApplicationSettings.Contains("CurrentUser")){
                currentUser = (User)ApplicationSettings.GetItem("CurrentUser");
            }

            if(currentUser != null)
            {
                UserName = currentUser.userName;
                FirstName = currentUser.firstName;
                LastName = currentUser.lastName;
            }
        }
    }
}
