using FanaticFirefly.Helpers;
using FanaticFirefly.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FanaticFirefly.Views
{
    public partial class ErrorPage : ContentPage
    {
        public ErrorPage()
        {
            InitializeComponent();

            BindingContext = new ErrorViewModel();
        }
    }
}
