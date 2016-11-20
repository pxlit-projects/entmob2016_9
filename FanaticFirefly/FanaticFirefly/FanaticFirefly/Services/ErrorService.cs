using FanaticFirefly.Enumerations;
using FanaticFirefly.Helpers;
using FanaticFirefly.Messaging;
using FanaticFirefly.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FanaticFirefly.Services
{
    public static class ErrorService
    {
        public static void ShowError(ViewType type, string message = "Oops something went wrong.")
        {
            Messenger.Default.Send<ErrorServiceMessage>(new ErrorServiceMessage(message, type));
        }
    }
}
