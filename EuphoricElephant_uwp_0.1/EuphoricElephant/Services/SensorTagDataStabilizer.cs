﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EuphoricElephant.Helpers;

namespace EuphoricElephant.Services
{
    public class SensorTagDataStabilizer
    {
        public MovementData AccellerometerStabilizer(byte[] data)                                   //returns a value between ~-10 and ~10 (need to use Y for up/down, X for left/right)
        {
            MovementData result = new MovementData(data);

            result.XAcc = (BitConverter.ToInt16(data, 6) * 10.0f) / (32768 / 8);
            result.YAcc = (BitConverter.ToInt16(data, 8) * 10.0f) / (32768 / 8);
            result.ZAcc = (BitConverter.ToInt16(data, 10) * 10.0f) / (32768 / 8);

            return result;
        }

        public MovementData GyroscopeStabilizer(byte[] data)                                        //returns a value between ~-250 and ~250
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
