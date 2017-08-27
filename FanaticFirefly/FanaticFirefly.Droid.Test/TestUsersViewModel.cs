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

namespace FanaticFirefly.Droid.Test
{
    [TestFixture]
    public class TestUsersViewModel
    {
        UsersViewModel _vm;

        [SetUp]
        public void SetUp()
        {
            _vm = new UsersViewModel();
        }

        [Test]
        public void TestViewModel()
        {
            Assert.IsNotNull(_vm.EditCommand);
            Assert.IsNotNull(_vm.SortByJoinedDateCommand);
            Assert.IsNotNull(_vm.SearchCommand);
            Assert.IsNotNull(_vm.SortByUserNameCommand);
        }
    }
}