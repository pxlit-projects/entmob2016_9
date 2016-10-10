using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuphoricElephant.Services
{
    public class SensorTagDataCheck
    {
        public void MovementCheck(byte[] data)
        {
            SensorTagDataStabilizer stabilizer = new SensorTagDataStabilizer();
            if (stabilizer.AccellerometerStabilizer(data).YAcc > 4)
            {
                Debug.WriteLine("Volume up!");
            }
            else if (stabilizer.AccellerometerStabilizer(data).YAcc < 4 && stabilizer.AccellerometerStabilizer(data).YAcc > -4)
            {
                Debug.WriteLine("No movement!");
            }
            else if (stabilizer.AccellerometerStabilizer(data).YAcc < -4)
            {
                Debug.WriteLine("Volume down!");
            }
            if (stabilizer.AccellerometerStabilizer(data).XAcc > 4)
            {
                Debug.WriteLine("Previous song!");
            }
            else if (stabilizer.AccellerometerStabilizer(data).XAcc < 4 && stabilizer.AccellerometerStabilizer(data).XAcc > -4)
            {
                Debug.WriteLine("No movement!");
            }
            else if (stabilizer.AccellerometerStabilizer(data).XAcc < -4)
            {
                Debug.WriteLine("Next Song!");
            }

        }

    }
}
