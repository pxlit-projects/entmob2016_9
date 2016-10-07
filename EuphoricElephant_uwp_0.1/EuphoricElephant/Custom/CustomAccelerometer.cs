using EuphoricElephant.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using X2CodingLab.SensorTag;
using X2CodingLab.SensorTag.Exceptions;
using X2CodingLab.SensorTag.Sensors;

namespace EuphoricElephant.Custom
{
    public class CustomAccelerometer : SensorBase
    {
        public CustomAccelerometer() 
            : base(SensorName.Accelerometer, String.Format(Constants.BASE_ID, "aa80"), String.Format(Constants.BASE_ID, "aa82"), String.Format(Constants.BASE_ID, "aa81"))
        {

        }

        /// <summary>
        /// Extracts the values of the 3 axis from the raw data of the sensor.
        /// </summary>
        /// <param name="sensorData">Complete array of data retrieved from the sensor</param>
        /// <returns>Array of doubles with the size of 3</returns>
        public static double[] CalculateCoordinates(byte[] sensorData)
        {
            return CalculateCoordinates(sensorData, 1.0);
        }

        /// <summary>
        /// Extracts the values of the 3 axis from the raw data of the sensor,
        /// </summary>
        /// <param name="sensorData">Complete array of data retrieved from the sensor</param>
        /// <param name="scale">Allows you to scale the accelerometer values</param>
        /// <returns>Array of doubles with the size of 3</returns>
        public static double[] CalculateCoordinates(byte[] sensorData, double scale)
        {
            CustomValidator.RequiresNotNull(sensorData, "sensorData");
            CustomValidator.RequiresArgument(!double.IsNaN(scale), "scale cannot be double.NaN");
            if (scale == 0)
                throw new ArgumentOutOfRangeException("scale", "scale cannot be 0");
            return new double[] { sensorData[0] * scale, sensorData[1] * scale, sensorData[2] * scale };
        }

        /// <summary>
        /// Sets the period the sensor reads data. Default is 1s. Lower limit is 100ms.
        /// </summary>
        /// <param name="time">Period in 10 ms.</param>
        /// <exception cref="DeviceUnreachableException">Thrown if it wasn't possible to communicate with the device.</exception>
        /// <exception cref="DeviceNotInitializedException">Thrown if sensor has not been initialized successfully.</exception>
        public async Task SetReadPeriod(byte time)
        {
            CustomValidator.Requires<DeviceNotInitializedException>(deviceService != null);

            if (time < 10)
                throw new ArgumentOutOfRangeException("time", "Period can't be lower than 100ms");

            GattCharacteristic dataCharacteristic = deviceService.GetCharacteristics(new Guid(SensorTagUuid.UUID_ACC_PERI))[0];

            byte[] data = new byte[] { time };
            GattCommunicationStatus status = await dataCharacteristic.WriteValueAsync(data.AsBuffer());
            if (status == GattCommunicationStatus.Unreachable)
            {
                throw new DeviceUnreachableException("Device Unreachable");
            }
        }

        public async Task CustomEnableSensor()
        {
            await CustomEnableSensor(new byte[] { 0xfe, 0xc0 });
        }


        protected async Task CustomEnableSensor(byte[] sensorEnableData)
        {
            CustomValidator.Requires<ArgumentNullException>(sensorEnableData != null);

            CustomValidator.Requires<DeviceNotInitializedException>(deviceService != null);

            GattCharacteristic configCharacteristic = deviceService.GetCharacteristics(new Guid(String.Format(Constants.BASE_ID, "aa82")))[0];

            GattCommunicationStatus status = await configCharacteristic.WriteValueAsync(sensorEnableData.AsBuffer());
            if (status == GattCommunicationStatus.Unreachable)
                throw new DeviceUnreachableException("Error");
        }

        
    }
}
