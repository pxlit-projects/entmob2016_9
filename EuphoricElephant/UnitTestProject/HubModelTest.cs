using EuphoricElephant.Data;
using EuphoricElephant.Helpers;
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

        [TestMethod]
        public void TestLogin()
        {
            ApplicationSettings.Remove("CurrentUser");
            HubViewModel hvm = new HubViewModel();
            
            hvm.UserName = "Djuke";
            hvm.PassWord = "hArtenaas@2";
            hvm.LoginCommand.Execute(null);

            Assert.IsTrue(hvm.IsLoggedIn);
            Assert.AreEqual(string.Empty, hvm.UserName);
            Assert.AreEqual(string.Empty, hvm.PassWord);
            Assert.AreEqual("Log out", hvm.LogButtonText);

            Assert.IsTrue(ApplicationSettings.Contains("CurrentUser"));
        }

        [TestMethod]
        public void TestWrongPassword()
        {
            ApplicationSettings.Remove("CurrentUser");

            HubViewModel hvm = new HubViewModel();

            hvm.UserName = "d";
            hvm.PassWord = "niet juist passwoord";

            hvm.LoginCommand.Execute(null);
            Assert.IsFalse(hvm.IsLoggedIn);
            Assert.AreNotEqual(string.Empty, hvm.UserName);
            Assert.AreNotEqual(string.Empty, hvm.PassWord);
            Assert.AreNotEqual("Log out", hvm.LogButtonText);

            Assert.IsFalse(ApplicationSettings.Contains("CurrentUser"));
        }

        [TestMethod]
        public void TestLogout()
        {
            HubViewModel hvm = new HubViewModel();

            hvm.UserName = "d";
            hvm.PassWord = "d";

            hvm.IsLoggedIn = true;
            hvm.LoginCommand.Execute(null);
            Assert.IsFalse(hvm.IsLoggedIn);
            Assert.AreEqual(string.Empty, hvm.UserName);
            Assert.AreEqual(string.Empty, hvm.PassWord);
            Assert.AreEqual("Log in", hvm.LogButtonText);

            Assert.IsFalse(ApplicationSettings.Contains("MediaPlayer"));
            Assert.IsFalse(ApplicationSettings.Contains("CurrentUser"));
            Assert.IsFalse(ApplicationSettings.Contains("ActiveSensor"));
        }

        [TestMethod]
        public void TestCurrentUserNotRemoved()
        {
            User u = new User
            {
                userName = "d",
                password = "d"
            }; 

            try
            {
                ApplicationSettings.AddItem("CurrentUser", u);
                Assert.Fail("no exception thrown");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is Exception);
            }
        }
    }
}
