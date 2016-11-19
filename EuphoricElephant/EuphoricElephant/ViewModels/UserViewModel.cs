using EuphoricElephant.Custom;
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
using Windows.UI.Xaml.Controls;

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
            set { SetProperty(ref selectedProfile, value);
                if (SelectedProfile != null)
                {
                    DefaultProfileName = SelectedProfile.profileName;
                    LoadProfileItems();
                }
            }
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

        private string email = string.Empty;
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        private string phone = string.Empty;
        public string Phone
        {
            get { return phone; }
            set { SetProperty(ref phone, value); }
        }

        private string country = string.Empty;

        public string Country
        {
            get { return country; }
            set { SetProperty(ref country, value); }
        }

        private string joinedOn = string.Empty;

        public string JoinedOn
        {
            get { return joinedOn; }
            set { SetProperty(ref joinedOn, value); }
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

        private ObservableCollection<ProfileItem> profileItems;
        public ObservableCollection<ProfileItem> ProfileItems
        {
            get { return profileItems; }
            set { SetProperty(ref profileItems, value); }
        }

        public ICommand EditCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand NewCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand DefaultCommand { get; set; }

        public UserViewModel()
        {
            RefisterMessages();
            LoadCommands();
        }

        private void LoadCommands()
        {
            EditCommand = new CustomCommand(EditAction);
            SaveCommand = new CustomCommand(SaveAction);
            NewCommand = new CustomCommand(NewAction);
            RefreshCommand = new CustomCommand(RefreshAction);
            DeleteCommand = new CustomCommand(DeleteAction);
            DefaultCommand = new CustomCommand(DefaultAction);
        }

        private async void DefaultAction(object obj)
        {
            var d = currentUser.defaultProfileId;

            currentUser.defaultProfileId = SelectedProfile.profileId;

            var v = await JSonParseService2<User>.SerializeDataToJson(Constants.USER_UPDATE_URL, currentUser, Enumerations.SerializeType.Put);

            if (!v.Equals("1"))
            {
                currentUser.defaultProfileId = d;
                await ErrorService.showError(v);
            }
        }

        private async void DeleteAction(object obj)
        {
            var v = await JSonParseService2<Profile>.SerializeDataToJson(Constants.PROFILE_DELETE_URL, SelectedProfile.profileId, Enumerations.SerializeType.Delete);

            if (v.Equals("1"))
            {
                Profiles.Remove(SelectedProfile);
                DefaultProfileName = Profiles.Where(x => x.profileId == currentUser.defaultProfileId).SingleOrDefault().profileName;
            }
            else
            {
                await ErrorService.showError(v);
            }
        }

        private async void RefreshAction(object obj)
        {
            Profiles = null;
            Profiles = new ObservableCollection<Profile>(await JSonParseService2<List<Profile>>.DeserializeDataFromJson(Constants.PROFILE_BY_USERID_URL, Convert.ToString(currentUser.userId)));

            DefaultProfileName = Profiles.Where(x => x.profileId == currentUser.defaultProfileId).SingleOrDefault().profileName;
            SelectedProfile = Profiles.Where(x => x.profileName == DefaultProfileName).SingleOrDefault();
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
                LoadUser();
                EditButtonText = "Edit";
            }
        }

        private async void SaveAction(object param)
        {
            Profile p = SelectedProfile;
            p.profileName = DefaultProfileName;

            var v = await JSonParseService2<Profile>.SerializeDataToJson(Constants.PROFILE_UPDATE_URL, p, Enumerations.SerializeType.Put);

            if(v.Equals("1"))
            {
                User u = currentUser;

                u.firstName = FirstName;
                u.lastName = LastName;
                u.userName = UserName;
                u.defaultProfileId = GetProfileId();
                u.country = Country;
                u.email = Email;
                u.phone = Phone;
                u.joinedOn = JoinedOn;

                ApplicationSettings.Edit("CurrentUser", currentUser);

                v = await JSonParseService2<User>.SerializeDataToJson(Constants.USER_UPDATE_URL, u, Enumerations.SerializeType.Put);

                if (!v.Equals("1"))
                {
                    await ErrorService.showError(v);
                }
                else
                {
                    List<ComboBox> comboxList = new List<ComboBox>();

                    foreach(var i in ProfileItems)
                    {
                        ComboBox box = new ComboBox();

                        foreach(var c in i.command)
                        {
                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = c.command;
                            box.Items.Add(item);
                        }
                        
                        box.SelectedIndex = i.commandIndex;
                        comboxList.Add(box);
                    };

                    bool b = ProfileService.Validated("default", comboxList);

                    if (b)
                    {
                        Profile profile = new Profile
                        {
                            profileName = SelectedProfile.profileName,
                            userId = SelectedProfile.userId,
                            pairings = await ProfileService.GetPairings(comboxList),
                            profileId = selectedProfile.profileId
                        };

                        v = await JSonParseService2<Profile>.SerializeDataToJson(Constants.PROFILE_UPDATE_URL, profile, Enumerations.SerializeType.Put);

                        if (v.Equals("1"))
                        {
                            EditAction(null);
                            LoadUser();
                        }
                        else
                        {
                            await ErrorService.showError(v);
                        }
                    }
                    
                }
            }
        }

        private int GetProfileId()
        {
            return Profiles.Where(x => x.profileName == DefaultProfileName).SingleOrDefault().profileId;
        }

        private async void NewAction(object param)
        {
            ContentDialog d = await ProfileService.CreateNewProfileDialog(currentUser);
            ContentDialogResult r = await d.ShowAsync();

            if(r == ContentDialogResult.Primary)
            {
                Profiles = new ObservableCollection<Profile>(await JSonParseService2<List<Profile>>.DeserializeDataFromJson(Constants.PROFILE_BY_USERID_URL, Convert.ToString(currentUser.userId)));
            }
        }
            

        private void Init()
        {
            if (ApplicationSettings.Contains("CurrentUser")){
                currentUser = (User)ApplicationSettings.GetItem("CurrentUser");
            }

            if(currentUser != null)
            {
                LoadUser();
            }
        }

        private async void LoadUser()
        {
            UserName = currentUser.userName;
            FirstName = currentUser.firstName;
            LastName = currentUser.lastName;
            JoinedOn = currentUser.joinedOn;
            Email = currentUser.email;
            Phone = currentUser.phone;
            Country = currentUser.country;

            Profiles = new ObservableCollection<Profile>(await JSonParseService2<List<Profile>>.DeserializeDataFromJson(Constants.PROFILE_BY_USERID_URL, Convert.ToString(currentUser.userId)));

            DefaultProfileName = Profiles.Where(x => x.profileId == currentUser.defaultProfileId).SingleOrDefault().profileName;
            SelectedProfile = Profiles.Where(x => x.profileName == DefaultProfileName).SingleOrDefault();
        }

        private async void LoadProfileItems()
        {
            ProfileItems = new ObservableCollection<ProfileItem>(await ProfileService.SetPairings(SelectedProfile));
        }
    }
}
