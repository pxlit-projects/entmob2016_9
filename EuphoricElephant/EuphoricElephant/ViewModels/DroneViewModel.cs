using EuphoricElephant.Messaging;
using EuphoricElephant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuphoricElephant.ViewModels
{
    public class DroneViewModel : BaseModel
    {

        private void RegisterMessages()
        {
            Messenger.Default.Register<NavigationMessage>(this, OnNavigationMessageRecieved);
        }

        private void OnNavigationMessageRecieved(NavigationMessage m)
        {
            if (m.Type == Enumerations.ViewType.DeviceViewType)
            {

            }
        }
    }
}
