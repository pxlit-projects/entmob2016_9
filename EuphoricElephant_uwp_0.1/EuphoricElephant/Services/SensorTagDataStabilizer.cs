using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EuphoricElephant.Helpers;

namespace EuphoricElephant.Services
{
    public class SensorTagDataStabilizer
    {
        public MovementData AccellerometerStabilizer(byte[] data)                                   //returns a value between ~-8 and ~8 (need to use Y for up/down, X for left/right)
        {
            MovementData result = new MovementData(data);

            result.XAcc = (BitConverter.ToInt16(data, 6) * 1.0) / (32768 / 64);
            result.YAcc = (BitConverter.ToInt16(data, 8) * 1.0) / (32768 / 64);
            result.ZAcc = (BitConverter.ToInt16(data, 10) * 1.0) / (32768 / 64);

            return result;
        }

        public MovementData GyroscopeStabilizer(byte[] data)
        {
            MovementData result = new MovementData(data);

            result.XGyr = (BitConverter.ToInt16(data, 0) * 1.0) / (65536 / 500);
            result.YGyr = (BitConverter.ToInt16(data, 2) * 1.0) / (65536 / 500);
            result.ZGyr = (BitConverter.ToInt16(data, 4) * 1.0) / (65536 / 500);

            return result;
        }

        public MovementData MagnetometerStabilizer(byte[] data)
        {
            MovementData result = new MovementData(data);

            result.XMag = data[7];
            result.YMag = data[9];
            result.ZMag = data[11];

            return result;
        }
    }
}
