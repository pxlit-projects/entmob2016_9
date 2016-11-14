using FanaticFirefly.ViewModels;
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
        public static void Navigate(NavigationPage mainPage, Page page)
        {
            NavigationPage navPage = new NavigationPage(page);
            mainPage.PushAsync(navPage);
        }
    }
}
