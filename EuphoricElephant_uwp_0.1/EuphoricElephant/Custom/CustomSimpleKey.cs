using EuphoricElephant.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X2CodingLab.SensorTag;
using X2CodingLab.SensorTag.Sensors;

namespace EuphoricElephant.Custom
{
    public class CustomSimpleKey : SensorBase
    {
        public CustomSimpleKey()
            : base(SensorName.SimpleKeyService, String.Format(Constants.SERVICE_ID, "ffe0"), null, String.Format(Constants.SERVICE_ID, "ffe1"))
        {
        
        }
    }
}
