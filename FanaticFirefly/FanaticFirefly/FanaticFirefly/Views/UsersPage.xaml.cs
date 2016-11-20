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
    public partial class UsersPage : ContentPage
    {
        public UsersPage()
        {
            BindingContext = new UsersViewModel();
            InitializeComponent();

            Messenger.Default.Register<ErrorServiceMessage>(this, OnErrorServiceMessageRecieved);
        }

        private void OnErrorServiceMessageRecieved(ErrorServiceMessage m)
        {
            if (m.Type == ViewType.UsersView)
            {
                DisplayAlert("Error", m.ErrorMessage, "OK");
            }
        }
    }
}
