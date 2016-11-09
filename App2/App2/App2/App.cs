using App2.Pages;
using App2.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace App2
{
    public class App : Xamarin.Forms.Application
    {
        public App()
        {
            MainPage = new LoginPage();
        }
    }
}
