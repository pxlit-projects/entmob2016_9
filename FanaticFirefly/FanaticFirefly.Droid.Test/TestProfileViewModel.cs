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
using FanaticFirefly.Data;
using FanaticFirefly.Helpers;

namespace FanaticFirefly.Droid.Test
{
    [TestFixture]
    public class TestProfileViewModel
    {
        ProfileViewModel _vm;

        Profile _p;

        [SetUp]
        public void Setup()
        {
            _p = new Profile(0,0,"default", "1,2,3,4,5;1,2,4,3,5");

            ApplicationSettings.AddItem("SelectedProfile", _p);

            _vm = new ProfileViewModel();
        }

        [Test]
        public void TestViewModel()
        {
            Assert.AreEqual(_vm.SelectedProfile, _p);

            Assert.AreEqual(_vm.Action1, "1");
            Assert.AreEqual(_vm.Action2, "2");
            Assert.AreEqual(_vm.Action3, "3");
            Assert.AreEqual(_vm.Action4, "4");
            Assert.AreEqual(_vm.Action5, "5");
            Assert.AreEqual(_vm.Command1, "1");
            Assert.AreEqual(_vm.Command2, "2");
            Assert.AreEqual(_vm.Command3, "3");
            Assert.AreEqual(_vm.Command4, "4");
            Assert.AreEqual(_vm.Command5, "5");

            Assert.AreEqual(_vm.ProfileName, "default");
        }

        [Test]
        public void AssureFailure()
        {
            ApplicationSettings.Remove("SelectedProfile");

            try
            {
                ProfileViewModel vm = new ProfileViewModel();
                Assert.Fail("no exception thrown");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is Exception);
            }
        }
    }
}