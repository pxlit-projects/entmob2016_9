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
    public class HubModelTest
    {
        [TestMethod]
        public void TestHubViewModel()
        {
            HubViewModel hvm = new HubViewModel();
            Assert.IsNotNull(hvm.NavigateCommand);
            Assert.IsNotNull(hvm.LoginCommand);
            Assert.IsNotNull(hvm.RegisterCommand);
        }
    }
}
