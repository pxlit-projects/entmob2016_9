using FanaticFirefly.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FanaticFirefly.Views
{
    public partial class ProfilesPage : ContentPage
    {
        public ProfilesPage()
        {
            BindingContext = new ProfilesViewModel();
            InitializeComponent();
        }
    }
}
