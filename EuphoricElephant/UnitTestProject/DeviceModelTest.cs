using EuphoricElephant.ViewModels;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
