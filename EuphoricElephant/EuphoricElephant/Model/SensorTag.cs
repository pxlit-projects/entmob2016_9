using EuphoricElephant.Custom;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.UI.Core;
using X2CodingLab.SensorTag;
using X2CodingLab.SensorTag.Sensors;

namespace EuphoricElephant.Model
{
    public class SensorTag
    {
        public CustomAccelerometer Accelerometer { get; set; }
        public CustomSimpleKey SimpleKey { get; set; }

        public DeviceInformation Info { get; set; }

        public SensorTag()
        {
            
        }

        
    }

    
}
