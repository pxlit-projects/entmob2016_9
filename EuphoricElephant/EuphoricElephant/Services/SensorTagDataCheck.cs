using EuphoricElephant.Enumerations;
using EuphoricElephant.Helpers;
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

        Boolean b = false;

        public ActionType MovementCheck(byte[] data, MusicPlayer player)
        {
            SensorTagDataStabilizer stabilizer = new SensorTagDataStabilizer();

            if (!b)
            {
                if ((stabilizer.AccellerometerStabilizer(data).YAcc < 5 && stabilizer.AccellerometerStabilizer(data).YAcc > -5) && (stabilizer.AccellerometerStabilizer(data).XAcc < 5 && stabilizer.AccellerometerStabilizer(data).XAcc > -5))
                {
                    b = true;
                    return ActionType.NoAction;
                    //   Debug.WriteLine("No movement!");
                }
                else 
                {
                    return ActionType.NoAction;
                }
            }
            else
            {
                Debug.WriteLine(stabilizer.GyroscopeStabilizer(data).XGyr);
                if (Math.Abs(stabilizer.GyroscopeStabilizer(data).XGyr) >= 100 && Math.Abs(stabilizer.GyroscopeStabilizer(data).ZGyr) >= 100 && Math.Abs(stabilizer.GyroscopeStabilizer(data).YGyr) >= 100)
                {
                    Debug.WriteLine("Shake");
                    b = false;
                    return ActionType.SHAKE;
                }

                else if (stabilizer.AccellerometerStabilizer(data).YAcc > 5)
                {
                    //    Debug.WriteLine("Volume up!");

                    b = false;
                    return ActionType.UP;
                }
                else if (stabilizer.AccellerometerStabilizer(data).YAcc < -5)
                {
                    //   Debug.WriteLine("Volume down!");
                    b = false;
                    return ActionType.DOWN;
                }

                else if (stabilizer.AccellerometerStabilizer(data).XAcc > 5)
                {
                    //      Debug.WriteLine("Previous song!");

                    b = false;
                    return ActionType.LEFT;

                    //Debug.WriteLine("Current: " + player.getTitle());

                }
                else if (stabilizer.AccellerometerStabilizer(data).XAcc < -5)
                {

                    //      Debug.WriteLine("Next Song!");

                    b = false;
                    return ActionType.RIGHT;

                    //Debug.WriteLine("Current: " + player.getTitle());

                }
                else
                {
                    b = true;
                    return ActionType.NoAction;
                }
            }
        }
    }
}
