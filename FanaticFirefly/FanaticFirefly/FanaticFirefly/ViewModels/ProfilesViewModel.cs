using FanaticFirefly.Data;
using FanaticFirefly.Helpers;
using FanaticFirefly.Services;
using FanaticFirefly.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FanaticFirefly.ViewModels
{
    public class ProfilesViewModel : BaseModel
    {
        private ObservableCollection<Profile> profiles;
        public ObservableCollection<Profile> Profiles
        {
            get { return profiles; }
            set { SetProperty(ref profiles, value); }
        }

        private Profile selectedProfile;
        public Profile SelectedProfile
        {
            get { return selectedProfile; }
            set { SetProperty(ref selectedProfile, value); OpenProfile(); }
        }

        public ProfilesViewModel()
        {
            Init();
        }

        private async void Init()
        {
            User currUser = null;
            if (ApplicationSettings.Contains("CurrentUser")) currUser = (User)ApplicationSettings.GetItem("CurrentUser");
            if(currUser != null)
            {
                Profiles = new ObservableCollection<Profile>(await JsonParseService<List<Profile>>.DeserializeDataFromJson(Constants.PROFILE_BY_USERID_URL, Convert.ToString(currUser.userId)));
            }
        }

        private async void OpenProfile()
        {
            if (ApplicationSettings.Contains("SelectedProfile"))
            {
                ApplicationSettings.Edit("SelectedProfile", SelectedProfile);
            }
            else
            {
                ApplicationSettings.AddItem("SelectedProfile", SelectedProfile);
            }
            if (SelectedProfile != null)
            {
                var v = (NavigationPage)App.Current.MainPage;
                await v.PushAsync(new ProfilePage());
            }
        }
    }
}
