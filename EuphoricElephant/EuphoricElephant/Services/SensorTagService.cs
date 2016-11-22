using EuphoricElephant.Enumerations;
using EuphoricElephant.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Devices.Enumeration;

namespace EuphoricElephant.Services
{
    public static class SensorTagService
    {
        private static Guid AccelServiceGuid = new Guid("F000AA10-0451-4000-B000-00000000000");
        private static Guid AccelDataGuid = new Guid("F000AA11-0451-4000-B000-000000000000");
        private static Guid AccelConfidGuid = new Guid("F000AA12-0451-4000-B000-000000000000");

        public async static Task<List<DeviceInformation>> FindAllTagNames()
        {
            List<DeviceInformation> result = new List<DeviceInformation>();

            // Get all the devices that expose generic access
            DeviceInformationCollection devices = await DeviceInformation.FindAllAsync(GattDeviceService.GetDeviceSelectorFromUuid(GattServiceUuids.GenericAccess));
            //DeviceInformationCollection devices = await DeviceInformation.FindAllAsync(RfcommDeviceService.GetDeviceSelector(RfcommServiceId.SerialPort)); GattServiceUuids.GenericAccess

            // Return an empty list if there are no devices
            if (devices.Count == 0)
                return result;

            foreach (DeviceInformation device in devices)
            {
                // put the name in the list
                result.Add(device);
            }

            return result;
        }

        internal async static Task<DeviceInformation> FindDeviceByGuid(string guid)
        {

            DeviceInformation device = null;

            try
            {
                Guid g = new Guid(guid);

                string s = GattDeviceService.GetDeviceSelectorFromUuid(g);

                device = await DeviceInformation.CreateFromIdAsync(s);
            }
            catch (Exception e)
            {
                ErrorService.showError(e.Message);
            }
            

            return device;
        }
    }
}
