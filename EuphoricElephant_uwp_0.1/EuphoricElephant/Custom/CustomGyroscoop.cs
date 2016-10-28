using EuphoricElephant.Custom;
using EuphoricElephant.Helpers;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using X2CodingLab.SensorTag;
using X2CodingLab.SensorTag.Exceptions;
using X2CodingLab.SensorTag.Sensors;

namespace EuphoricElephant.Model
{
    public class CustomGyroscoop : SensorBase
    {

        public CustomGyroscoop()
            : base(SensorName.Gyroscope, String.Format(Constants.BASE_ID, "aa80"), String.Format(Constants.BASE_ID, "aa82"), String.Format(Constants.BASE_ID, "aa81"))
        {

        }

        /// <summary>
        /// Enables the sensor to read all axis
        /// </summary>
        /// <returns></returns>
        /// <exception cref="DeviceUnreachableException">Thrown if it wasn't possible to communicate with the device.</exception>
        /// <exception cref="DeviceNotInitializedException">Thrown if sensor has not been initialized successfully.</exception>
        public override async Task EnableSensor()
        {
            await EnableSensor(new byte[] { 0x7F, 0x00 });
        }
    }
}