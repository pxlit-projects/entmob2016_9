using EuphoricElephant.Data;
using EuphoricElephant.Enumerations;
using EuphoricElephant.Helpers;
using EuphoricElephant.Services;
using EuphoricElephant.ViewModels;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    [TestClass]
    public class UserModelTest
    {
        User u;
        User newUser;
        Profile p1;
        Profile p2;
        Profile p3;
        Profile p4;

        [TestInitialize]
        public async Task Init()
        {
            u = new User()
            {
                userId = 395,
                firstName = "test",
                lastName = "test",
                userName = "test",
                password = "test",
                defaultProfileId = 324
            };

            p1 = new Profile()
            {
                profileName = "Test Profile",
                profileId = 325,
                userId = 395,
                pairings = "1,2,3,4,5;1,2,3,4,5"
            };

            p2 = new Profile()
            {
                profileName = "Default Profile",
                profileId = 324,
                userId = 395,
                pairings = "1,2,3,4,5;1,2,3,4,5"
            };

            p3 = new Profile()
            {
                profileName = "Unknown Profile",
                profileId = 0,
                userId = 0,
                pairings = "1,2,3,4,5;1,2,3,4,5"
            };

            p4 = new Profile()
            {
                profileName = "Unknown Profile",
                profileId = 0,
                userId = 0,
                pairings = "1,2,3,4,5;1,2,3,4,5"
            };

            newUser = new User()
            {
                country = "NEW COUNTRY",
                defaultProfileId = u.defaultProfileId,
                lastName = "NEW LASTNAME",
                email = "NEW EMAIL",
                firstName = "NEW FIRSTNAME",
                joinedOn = "5247837800209257813",
                phone = "4242424242",
                password = u.password,
                userId = u.userId,
                userName = u.userName
            };

            await JSonParseService2<Profile>.SerializeDataToJson(Constants.USER_ADD_URL, u, SerializeType.Post);
            await JSonParseService2<Profile>.SerializeDataToJson(Constants.PROFILE_ADD_URL, p1, SerializeType.Post);
            await JSonParseService2<Profile>.SerializeDataToJson(Constants.PROFILE_ADD_URL, p2, SerializeType.Post);
        }

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

        [TestMethod]
        public void TestDefault()
        {
            UserViewModel uvm = new UserViewModel();

            uvm.currentUser = u;
            uvm.SelectedProfile = p1;

            uvm.DefaultCommand.Execute(null);

            Assert.IsNotNull(uvm.ProfileItems);
            Assert.IsTrue(uvm.ProfileItems.Count > 0);

            Assert.IsTrue(uvm.currentUser.defaultProfileId == 325);

            //Set back
            p1.profileId = 324;
            uvm.SelectedProfile = p1;
            uvm.DefaultCommand.Execute(null);
        }

        [TestMethod]
        public void TestDefaultFailure()
        {
            UserViewModel uvm = new UserViewModel();

            uvm.currentUser = u;
            uvm.SelectedProfile = p3;

            uvm.DefaultCommand.Execute(null);

            Assert.IsNotNull(uvm.ProfileItems);
            Assert.IsTrue(uvm.ProfileItems.Count > 0);

            Assert.IsTrue(uvm.currentUser.defaultProfileId == 0);
        }


        [TestMethod]
        public async Task TestDelete()
        {
            UserViewModel uvm = new UserViewModel();
            uvm.Profiles = new ObservableCollection<Profile>() { p1, p2, p3 };
            uvm.currentUser = u;
            uvm.DefaultProfileName = p2.profileName; //Default Profile
            uvm.SelectedProfile = p1; //Test Profile =>Delete this

            uvm.DeleteCommand.Execute(null);

            Assert.AreEqual(p2.profileName, uvm.DefaultProfileName);
            Assert.IsTrue(!uvm.Profiles.Any<Profile>(x => x.profileId == p1.profileId));

            //SetBack
            await JSonParseService2<Profile>.SerializeDataToJson(Constants.PROFILE_ADD_URL, p1, SerializeType.Post);
        }

        [TestMethod]
        public void TestDeleteDefault()
        {
            UserViewModel uvm = new UserViewModel();
            uvm.Profiles = new ObservableCollection<Profile>() { p1, p2 };
            uvm.currentUser = u;
            uvm.DefaultProfileName = p2.profileName;
            uvm.SelectedProfile = p2;

            uvm.DeleteCommand.Execute(null);

            Assert.IsTrue(uvm.Profiles.Any<Profile>(x => x.profileId == p2.profileId));
        }

        [TestMethod]
        public void TestEdit()
        {
            UserViewModel uvm = new UserViewModel();
            uvm.IsNotEditing = true;
            uvm.EditCommand.Execute(null);

            Assert.IsFalse(uvm.IsNotEditing);
            Assert.IsTrue(uvm.EditButtonText.Equals("Cancel"));
        }

        [TestMethod]
        public void TestRefresh()
        {
            UserViewModel uvm = new UserViewModel();
            uvm.currentUser = u;
            uvm.Profiles = new ObservableCollection<Profile>() { p3, p4 };
            uvm.SelectedProfile = uvm.Profiles[0];

            uvm.RefreshCommand.Execute(null);

            Assert.AreEqual("Default Profile", uvm.DefaultProfileName);
            Assert.AreEqual("Default Profile", uvm.SelectedProfile.profileName);
            Assert.IsTrue(uvm.Profiles.Any<Profile>(x => x.profileId == p1.profileId));
            Assert.IsTrue(uvm.Profiles.Any<Profile>(x => x.profileId == p2.profileId));
            Assert.IsFalse(uvm.Profiles.Any<Profile>(x => x.profileId == p3.profileId));
            Assert.IsFalse(uvm.Profiles.Any<Profile>(x => x.profileId == p4.profileId));
        }

        [TestMethod]
        public async Task TestSave()
        {
            UserViewModel uvm = new UserViewModel();
            uvm.SelectedProfile = p1;
            uvm.DefaultProfileName = "NEW PROFILE NAME";

            uvm.currentUser = u;
            uvm.FirstName = newUser.firstName;
            uvm.LastName = newUser.lastName;
            uvm.Country = newUser.country;
            uvm.Email = newUser.email;
            uvm.Phone = newUser.phone;
            uvm.JoinedOn = newUser.joinedOn;
            uvm.Profiles = new ObservableCollection<Profile>() { p1, p2 };

            uvm.SaveCommand.Execute(null);

            User user = await JSonParseService2<User>.DeserializeDataFromJson(Constants.USER_BY_ID_URL, Convert.ToString(u.userId));

            Assert.AreEqual(newUser.firstName, user.firstName);
            Assert.AreEqual(newUser.lastName, user.lastName);
            Assert.AreEqual(newUser.country, user.country);
            Assert.AreEqual(newUser.email, user.email);
            Assert.AreEqual(newUser.phone, user.phone);
            Assert.AreEqual(newUser.joinedOn, user.joinedOn);

            Profile profile = (await JSonParseService2<Profile>.DeserializeDataFromJson(Constants.PROFILE_BY_ID_URL, Convert.ToString(p1.profileId)));

            Assert.AreEqual(uvm.DefaultProfileName, profile.profileName);

            //SetBack
            await JSonParseService2<User>.SerializeDataToJson(Constants.USER_UPDATE_URL, u, SerializeType.Put);
            await JSonParseService2<User>.SerializeDataToJson(Constants.PROFILE_UPDATE_URL, p1, SerializeType.Put);

        }
    }
}
