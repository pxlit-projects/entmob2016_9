using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Collections.Generic;
using EuphoricElephant.ViewModels;
using EuphoricElephant.Services;
using System.Threading.Tasks;
using System.Diagnostics;
using EuphoricElephant.Model;

namespace UnitTestProject
{
    [TestClass]

    public class UnitTest1
    {

        [TestMethod]

        public void TestMethod1()
        {
            HubViewModel hvm = new HubViewModel();
            Assert.IsNotNull(hvm.NavigateCommand);
            Assert.IsNotNull(hvm.LoginCommand);
            Assert.IsNotNull(hvm.RegisterCommand);
        }

      /*  [TestMethod]

        public void TestMethod2()
        {
            
            HubViewModel hubViewModel = new HubViewModel();
            String userName1 = "1";
            String password1 = "1";
            hubViewModel.IsLoggedIn = false;
            hubViewModel.myUser = null;

            hubViewModel.UserName = userName1;
            hubViewModel.PassWord = password1;
            Task t = Task.Run(()=> hubViewModel.LoginAction(null));
            Task.WaitAll();

            //hubViewModel.IsLoggedIn = true;

            //Assert.Equals(hubViewModel.myUser.lastName, "1");
            Assert.IsTrue(hubViewModel.IsNotBusy == true);
        }*/

        

         [TestMethod]

         public void TestMethod2()
         {
            DeviceViewModel deviceViewModel = new DeviceViewModel();
            deviceViewModel.activeSensor= new SensorTag();
            deviceViewModel.UnpairAction(null);

            Assert.AreEqual(deviceViewModel.SelectedTag, null);
        }


        [TestMethod]

        public void TestMethod3()
        {
            DeviceViewModel deviceViewModel = new DeviceViewModel();
            

            Assert.IsNotNull(deviceViewModel.Sensors);
        }

    }
}
