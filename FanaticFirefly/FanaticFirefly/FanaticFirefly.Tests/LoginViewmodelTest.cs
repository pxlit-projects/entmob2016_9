// <copyright file="LoginViewmodelTest.cs">Copyright ©  2014</copyright>
using System;
using FanaticFirefly.ViewModels;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using NUnit.Framework;
using FanaticFirefly.Data;

namespace FanaticFirefly.ViewModels.Tests
{
    /// <summary>This class contains parameterized unit tests for LoginViewmodel</summary>
    [PexClass(typeof(LoginViewmodel))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestFixture]
    public partial class LoginViewmodelTest
    {
        private User user;
        private string userName;
        private string password;
        private LoginViewmodel vm;

        [SetUp]
        protected void SetUp()
        {
            userName = "Djuke";
            password = "hArtenaas@2";

            vm = new LoginViewmodel();
        }

        [Test]
        public void foo()
        {
            vm.UserName = userName;
            vm.PassWord = password;

            vm.LoginCommand.Execute(null);

            Assert.IsTrue(vm.IsLoggedIn);
        }
    }
}
