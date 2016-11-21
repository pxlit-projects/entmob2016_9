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
    public class UserModelTest
    {
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
