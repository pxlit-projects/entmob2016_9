using FanaticFirefly.Services;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanaticFirefly.Test
{
    [TestClass]
    public class LoginTest
    {
        [TestMethod]
        public void TestGetLoginStatus()
        {
            string UserName = "Derpster";
            string PassWord = "1111";

            string result = LoginService.GetLoginStatus(UserName, PassWord).Result;

            Assert.AreEqual(result, "1");
        }

        [TestMethod]
        public void TestGetLogedInUser()
        {

        }

        [TestMethod]
        public void TestGetEncriptedPassword()
        {

        }
    }
}
