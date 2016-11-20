using FanaticFirefly.Helpers;
using FanaticFirefly.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FanaticFirefly.ViewModels
{
    public class ErrorViewModel : BaseModel
    {
        private string message = string.Empty;

        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }

        private string errorMesssage = string.Empty;

        public string ErrorMessage
        {
            get { return errorMesssage; }
            set { SetProperty(ref errorMesssage, value); }
        }

        private bool isHidden = true;
        public bool IsHidden
        {
            get { return isHidden; }
            set { SetProperty(ref isHidden, value); }
        }

        private string buttonText = "Show Error";
        public string ButtonText
        {
            get { return buttonText; }
            set { SetProperty(ref buttonText, value); }
        }
        
        public Command ShowErrorCommand { get; set; }

        public ErrorViewModel()
        {
            Message = Constants.ERROR_MESSAGE;
            ErrorMessage = ErrorService.GetErrorMessage();
            LoadCommands();
        }

        private void LoadCommands()
        {
            ShowErrorCommand = new Command(ShowErrorAction);
        }

        private void ShowErrorAction(object obj)
        {
            IsHidden = !IsHidden;
            if (IsHidden)
            {
                ButtonText = "Show Error";
            }
            else
            {
                ButtonText = "Hide Error";
            }
        }
    }
}
