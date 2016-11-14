﻿using EuphoricElephant.Enumerations;
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

            if (b)
            {
                if (Math.Abs(stabilizer.GyroscopeStabilizer(data).XGyr) >= 200 && Math.Abs(stabilizer.GyroscopeStabilizer(data).ZGyr) >= 200 && Math.Abs(stabilizer.GyroscopeStabilizer(data).YGyr) >= 200)
                {
                    b = false;
                    return ActionType.SHAKE;
                }

                if (stabilizer.AccellerometerStabilizer(data).YAcc > 5)
                {
                    //    Debug.WriteLine("Volume up!");
                    return ActionType.UP;
                }
                else if (stabilizer.AccellerometerStabilizer(data).YAcc < 5 && stabilizer.AccellerometerStabilizer(data).YAcc > -5)
                {
                    //   Debug.WriteLine("No movement!");
                }
                else if (stabilizer.AccellerometerStabilizer(data).YAcc < -5)
                {
                    //   Debug.WriteLine("Volume down!");
                    return ActionType.DOWN;
                }
            }

            if (stabilizer.AccellerometerStabilizer(data).XAcc > 5)
            {
                if (b)
                {
                    //      Debug.WriteLine("Previous song!");

                    b = false;
                    return ActionType.LEFT;
                    
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
                    return ActionType.RIGHT;
                    
                    //Debug.WriteLine("Current: " + player.getTitle());
                }
            }

            return ActionType.NoAction;

        }

    }
}