using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NUnit.Framework;
using FanaticFirefly.ViewModels;
using FanaticFirefly.Helpers;
using FanaticFirefly.Data;

namespace FanaticFirefly.Droid.Test
{
    [TestFixture]
    public class TestLoginViewModel
    {
        LoginViewmodel _vm;
        User _u;
        string _userName;
        string _passWord;

        [SetUp]
        public void SetUp()
        {
            _userName = "Djuke";
            _passWord = "hArtenaas@2";

            _vm = new LoginViewmodel();
            _u = new User
            {
                userName = _userName,
                password = _passWord
            };
        }

        [Test]
        public void TestViewModel()
        {
            Assert.IsNotNull(_vm.ShowUsersCommand);
            Assert.IsNotNull(_vm.LoginCommand);
        }

        [Test]
        public void TestCurrentUserNotRemoved()
        {
            ApplicationSettings.AddItem("CurrentUser", _u);

            try
            {
                ApplicationSettings.AddItem("CurrentUser", _u);
                Assert.Fail("no exception thrown");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is Exception);
            }
        }
    }
}