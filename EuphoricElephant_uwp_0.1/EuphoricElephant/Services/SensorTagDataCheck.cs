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

        Boolean b = true;

        public ActionType MovementCheck(byte[] data, MusicPlayer player)
        {
            SensorTagDataStabilizer stabilizer = new SensorTagDataStabilizer();

            if (stabilizer.AccellerometerStabilizer(data).YAcc > 5)
            {
                //    Debug.WriteLine("Volume up!");
                return ActionType.Up;
            }
            else if (stabilizer.AccellerometerStabilizer(data).YAcc < 5 && stabilizer.AccellerometerStabilizer(data).YAcc > -5)
            {
             //   Debug.WriteLine("No movement!");
            }
            else if (stabilizer.AccellerometerStabilizer(data).YAcc < -5)
            {
                //   Debug.WriteLine("Volume down!");
                return ActionType.Down;
            }

            

            if (stabilizer.AccellerometerStabilizer(data).XAcc > 5)
            {
                if (b)
                {
                    //      Debug.WriteLine("Previous song!");

                    b = false;
                    return ActionType.Left;
                    
                    //Debug.WriteLine("Current: " + player.getTitle());
                }
            }
            else if (stabilizer.AccellerometerStabilizer(data).XAcc < 5 && stabilizer.AccellerometerStabilizer(data).XAcc > -5)
            {
            //    Debug.WriteLine("No movement!");
                b = true;
            }
            else if (stabilizer.AccellerometerStabilizer(data).XAcc < -5)
            {
                if (b)
                {
                    //      Debug.WriteLine("Next Song!");

                    b = false;
                    return ActionType.Right;
                    
                    //Debug.WriteLine("Current: " + player.getTitle());
                }
            }

            return ActionType.NoAction;

        }

    }
}
