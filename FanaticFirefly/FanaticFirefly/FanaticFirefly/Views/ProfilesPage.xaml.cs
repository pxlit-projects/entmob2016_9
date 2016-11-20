using FanaticFirefly.Enumerations;
using FanaticFirefly.Messaging;
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

            Messenger.Default.Register<ErrorServiceMessage>(this, OnErrorServiceMessageRecieved);

        }

        private void OnErrorServiceMessageRecieved(ErrorServiceMessage m)
        {
            if (m.Type == ViewType.ProfilesView)
            {
                DisplayAlert("Error", m.ErrorMessage, "OK");
            }
        }
    }
}
