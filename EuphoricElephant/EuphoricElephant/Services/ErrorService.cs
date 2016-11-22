using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace EuphoricElephant.Services
{
    public static class ErrorService
    {
        public static void showError(string message)
        {
            var m = message;

            if(m == null || m == string.Empty)
            {
                m = "Oops, something went wrong.";
            }

            var dialog = new Windows.UI.Popups.MessageDialog(m);

            dialog.Commands.Add(new Windows.UI.Popups.UICommand("OK") { Id = 0 });

           Task.Run(()=>dialog.ShowAsync());
        }

        public static void showError()
        {
            showError("Oops, something went wrong.");
        }
    }
}
