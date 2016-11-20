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

        public void TestHubViewModel()
        {
            HubViewModel hvm = new HubViewModel();
            Assert.IsNotNull(hvm.NavigateCommand);
            Assert.IsNotNull(hvm.LoginCommand);
            Assert.IsNotNull(hvm.RegisterCommand);
        }

        [TestMethod]
        public void TestDeviceModel()
        {
            DeviceViewModel dvm = new DeviceViewModel();
            Assert.IsNotNull(dvm.UnpairCommand);
        }

        [TestMethod]
        public void TestMediaViewModel()
        {
            MediaViewModel mvm = new MediaViewModel();
            Assert.IsNotNull(mvm.LoadProfilesCommand);
            Assert.IsNotNull(mvm.NextTrackCommand);
            Assert.IsNotNull(mvm.OpenFolderCommand);
            Assert.IsNotNull(mvm.PlayTrackCommand);
            Assert.IsNotNull(mvm.PreviousTrackCommand);
            Assert.IsNotNull(mvm.ShuffleCommand);
            Assert.IsNotNull(mvm.StopTrackCommand);
            Assert.IsNotNull(mvm.ToggleLoopCommand);
        }

        [TestMethod]
        public void TestUserViewModel()
        {
            UserViewModel uvm = new UserViewModel();
            Assert.IsNotNull(uvm.DefaultCommand);
            Assert.IsNotNull(uvm.DeleteCommand);
            Assert.IsNotNull(uvm.EditCommand);
            Assert.IsNotNull(uvm.NewCommand);
            Assert.IsNotNull(uvm.RefreshCommand);
            Assert.IsNotNull(uvm.SaveCommand);
        }
    }
}
