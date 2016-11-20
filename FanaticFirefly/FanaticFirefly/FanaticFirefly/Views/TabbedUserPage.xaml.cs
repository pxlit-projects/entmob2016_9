using FanaticFirefly.Enumerations;
using FanaticFirefly.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FanaticFirefly.Views
{
    public partial class TabbedUserPage : TabbedPage
    {
        public TabbedUserPage()
        {
            InitializeComponent();

            Messenger.Default.Register<ErrorServiceMessage>(this, OnErrorServiceMessageRecieved);
        }

        private void OnErrorServiceMessageRecieved(ErrorServiceMessage m)
        {
            if (m.Type == ViewType.TabbedUserView)
            {
                DisplayAlert("Error", m.ErrorMessage, "OK");
            }
        }
    }
}
