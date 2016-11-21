using EuphoricElephant.Helpers;
using EuphoricElephant.Services;
using EuphoricElephant.ViewModels;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;

namespace UnitTestProject
{
    [TestClass]
    public class DeviceModelTest
    {
        [TestMethod]
        public void TestDeviceModel()
        {
            DeviceViewModel dvm = new DeviceViewModel();
            Assert.IsNotNull(dvm.UnpairCommand);
        }

        [TestMethod]
        public void TestUnpair()
        {
            DeviceViewModel dvm = new DeviceViewModel();

            dvm.UnpairCommand.Execute(null);

            Assert.IsNull(dvm.SelectedTag);
            Assert.IsFalse(ApplicationSettings.Contains("ActiveSensor"));
        }

        [TestMethod]
        public async Task TestSetSensorTag()
        {
            ApplicationSettings.Remove("ActiveSensor");
            DeviceViewModel dvm = new DeviceViewModel();

            DeviceInformationCollection devices = await DeviceInformation.FindAllAsync(GattDeviceService.GetDeviceSelectorFromUuid(GattServiceUuids.GenericAccess));

            dvm.SelectedTag = devices[0];

            Assert.IsTrue(ApplicationSettings.Contains("ActiveSensor"));
        }
    }
}
