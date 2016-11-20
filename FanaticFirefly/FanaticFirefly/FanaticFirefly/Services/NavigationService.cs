using FanaticFirefly.Data;
using FanaticFirefly.Helpers;
using FanaticFirefly.ViewModels;
using FanaticFirefly.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FanaticFirefly.Services
{
    public static class NavigationService
    {
        public static async void OpenProfile(Profile SelectedProfile, NavigationPage nav)
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
                await nav.PushAsync(new ProfilePage());
            }
        }

        public static async void OpenUser(User SelectedUser, NavigationPage nav)
        {
            if (ApplicationSettings.Contains("SelectedUser"))
            {
                ApplicationSettings.Edit("SelectedUser", SelectedUser);
            }
            else
            {
                ApplicationSettings.AddItem("SelectedUser", SelectedUser);
            }
            await nav.PushAsync(new TabbedUserPage());
        }
    }
}
