using FanaticFirefly.Helpers;
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
        public static string GetErrorMessage()
        {
            object error = null;

            if (ApplicationSettings.Contains("ErrorMessage"))
            {
                error = ApplicationSettings.GetItem("ErrorMessage");
            }

            return (string)error;
        }

        public static void ShowError(string message = "Oops something went wrong.")
        {
            
        }
    }
}
