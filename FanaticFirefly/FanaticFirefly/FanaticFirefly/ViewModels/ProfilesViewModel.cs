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
            set { SetProperty(ref selectedProfile, value); NavigationService.OpenProfile(value, (NavigationPage)App.Current.MainPage); }
        }

        public ProfilesViewModel()
        {
            Init();
        }

        private async void Init()
        {
            User currUser = null;
            if (ApplicationSettings.Contains("SelectedUser")) currUser = (User)ApplicationSettings.GetItem("SelectedUser");
            if(currUser != null)
            {
                var profiles = await DataAccessService.GetProfiles(currUser);

                if(profiles == null)
                {
                    ErrorService.ShowError(Enumerations.ViewType.ProfilesView);
                }
                else
                {
                    Profiles = profiles;
                }
            }else
            {
                ErrorService.ShowError(Enumerations.ViewType.ProfilesView);
            }
        }

        
    }
}
