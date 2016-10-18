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

        public void MovementCheck(byte[] data, MusicPlayer player)
        {
            SensorTagDataStabilizer stabilizer = new SensorTagDataStabilizer();

            lasthope.MixerInfo mi = lasthope.GetMixerControls();
            

            if (stabilizer.AccellerometerStabilizer(data).YAcc > 5)
            {
            //    Debug.WriteLine("Volume up!");
                lasthope.AdjustVolume(mi, (mi.maxVolume - mi.minVolume) / 50);
            }
            else if (stabilizer.AccellerometerStabilizer(data).YAcc < 5 && stabilizer.AccellerometerStabilizer(data).YAcc > -5)
            {
             //   Debug.WriteLine("No movement!");
            }
            else if (stabilizer.AccellerometerStabilizer(data).YAcc < -5)
            {
             //   Debug.WriteLine("Volume down!");
                lasthope.AdjustVolume(mi, -(mi.maxVolume - mi.minVolume) / 50);
            }

            

            if (stabilizer.AccellerometerStabilizer(data).XAcc > 5)
            {
                if (b)
                {
              //      Debug.WriteLine("Previous song!");
                   
                    //player.previous();
                    //b = false;
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
                    
                    //player.next();
                    //b = false;
                    //Debug.WriteLine("Current: " + player.getTitle());
                }
            }

        }

    }
}
