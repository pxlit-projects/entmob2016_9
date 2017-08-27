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
    public class TestProfilesViewModel
    {
        ProfilesViewModel _vm;

        int _userId;
        User _u;

        [SetUp]
        public void SetUp()
        {
            _userId = 402;
            _u = new User()
            {
                userId = _userId
            };

            _vm = new ProfilesViewModel();
        }

        [Test]
        public void TestViewModel()
        {
            Assert.IsNull(_vm.Profiles);
        }
    }
}