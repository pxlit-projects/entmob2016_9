using EuphoricElephant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;

namespace EuphoricElephant.Data
{
    public class SensorTag : BaseModel
    {

        private AccelReading accel;
    
        public AccelReading Accel
        {
            get { return accel; }
            set { SetProperty(ref accel, value); }
        }
        
    }
}
