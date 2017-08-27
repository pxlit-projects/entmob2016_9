using System.Collections.ObjectModel;
using FanaticFirefly.Data;
using System;
using FanaticFirefly.ViewModels;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using NUnit.Framework;

namespace FanaticFirefly.ViewModels.Tests
{
    /// <summary>This class contains parameterized unit tests for ProfilesViewModel</summary>
    [TestFixture]
    [PexClass(typeof(ProfilesViewModel))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class ProfilesViewModelTest
    {

        /// <summary>Test stub for .ctor()</summary>
        [PexMethod]
        public ProfilesViewModel ConstructorTest()
        {
            ProfilesViewModel target = new ProfilesViewModel();
            return target;
            // TODO: add assertions to method ProfilesViewModelTest.ConstructorTest()
        }

        /// <summary>Test stub for get_Profiles()</summary>
        [PexMethod]
        public ObservableCollection<Profile> ProfilesGetTest([PexAssumeUnderTest]ProfilesViewModel target)
        {
            ObservableCollection<Profile> result = target.Profiles;
            return result;
            // TODO: add assertions to method ProfilesViewModelTest.ProfilesGetTest(ProfilesViewModel)
        }

        /// <summary>Test stub for get_SelectedProfile()</summary>
        [PexMethod]
        public Profile SelectedProfileGetTest([PexAssumeUnderTest]ProfilesViewModel target)
        {
            Profile result = target.SelectedProfile;
            return result;
            // TODO: add assertions to method ProfilesViewModelTest.SelectedProfileGetTest(ProfilesViewModel)
        }

        /// <summary>Test stub for set_Profiles(ObservableCollection`1&lt;Profile&gt;)</summary>
        [PexMethod]
        public void ProfilesSetTest([PexAssumeUnderTest]ProfilesViewModel target, ObservableCollection<Profile> value)
        {
            target.Profiles = value;
            // TODO: add assertions to method ProfilesViewModelTest.ProfilesSetTest(ProfilesViewModel, ObservableCollection`1<Profile>)
        }

        /// <summary>Test stub for set_SelectedProfile(Profile)</summary>
        [PexMethod]
        public void SelectedProfileSetTest([PexAssumeUnderTest]ProfilesViewModel target, Profile value)
        {
            target.SelectedProfile = value;
            // TODO: add assertions to method ProfilesViewModelTest.SelectedProfileSetTest(ProfilesViewModel, Profile)
        }
    }
}
