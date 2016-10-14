using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuphoricElephant.Helpers
{
    public class MovementData
    {

        public MovementData(byte[] data)
        {            
        }
        
        public double XAcc { get; set; }
        public double YAcc { get; set; }
        public double ZAcc { get; set; }

        public double XGyr { get; set; }
        public double YGyr { get; set; }
        public double ZGyr { get; set; }

        public double XMag { get; set; }
        public double YMag { get; set; }
        public double ZMag { get; set; }


    }
}
