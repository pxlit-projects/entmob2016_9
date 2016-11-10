﻿using EuphoricElephant.Custom;
using EuphoricElephant.Data;
using EuphoricElephant.Helpers;
using EuphoricElephant.Messaging;
using EuphoricElephant.Models;
using EuphoricElephant.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

        private Profile selectedProfile;
        public Profile SelectedProfile
        {
            get { return selectedProfile; }
            set { SetProperty(ref selectedProfile, value); }
        }

        private ObservableCollection<Profile> profiles;
        public ObservableCollection<Profile> Profiles
        {
            get { return profiles; }
            set { SetProperty(ref profiles, value); }
        } 

        private string defaultProfileName = "No Profile selected";

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

        private bool isNotEditing = true;

        public bool IsNotEditing
        {
            get { return isNotEditing; }
            set { SetProperty(ref isNotEditing, value); }
        }

        private string editButtonText = "Edit";
        public string EditButtonText
        {
            get { return editButtonText; }
            set { SetProperty(ref editButtonText, value); }
        }

        public ICommand EditCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        public UserViewModel()
        {
            RefisterMessages();
            LoadCommands();
        }

        private void LoadCommands()
        {
            EditCommand = new CustomCommand(EditAction);
            SaveCommand = new CustomCommand(SaveAction);
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

        private void EditAction(object param)
        {
            IsNotEditing = !IsNotEditing;

            if (!isNotEditing)
            {
                EditButtonText = "Cancel";
            }else
            {
                EditButtonText = "Edit";
            }
        }

        private void SaveAction(object param)
        {

        }

        private async void Init()
        {
            if (ApplicationSettings.Contains("CurrentUser")){
                currentUser = (User)ApplicationSettings.GetItem("CurrentUser");
            }

            if(currentUser != null)
            {
                UserName = currentUser.userName;
                FirstName = currentUser.firstName;
                LastName = currentUser.lastName;

                Profiles = new ObservableCollection<Profile>(await JsonParseService<List<Profile>>.DeserializeDataFromJson("profile/user", currentUser.userId));

                DefaultProfileName = Profiles.Where(x => x.profileId == currentUser.defaultProfileId).SingleOrDefault().profileName;
            }
        }
    }
}
