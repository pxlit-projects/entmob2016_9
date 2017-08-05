using EuphoricElephant.Data;
using EuphoricElephant.Enumerations;
using EuphoricElephant.Helpers;
using EuphoricElephant.Services;
using EuphoricElephant.ViewModels;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace UnitTestProject
{
    [TestClass]
    public class MediaModelTest
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
        public void TestMediaViewModel()
        {
            MediaViewModel mvm = new MediaViewModel();
            Assert.IsNotNull(mvm.LoadProfilesCommand);
            Assert.IsNotNull(mvm.NextTrackCommand);
            Assert.IsNotNull(mvm.OpenFolderCommand);
            Assert.IsNotNull(mvm.PlayTrackCommand);
            Assert.IsNotNull(mvm.PreviousTrackCommand);
            Assert.IsNotNull(mvm.ShuffleCommand);
            Assert.IsNotNull(mvm.StopTrackCommand);
            Assert.IsNotNull(mvm.ToggleLoopCommand);
        }

        [TestMethod]
        public void TestLoadProfiles()
        {
            if(!ApplicationSettings.Contains("CurrentUser"))
                ApplicationSettings.AddItem("CurrentUser", u);

            MediaViewModel mvm = new MediaViewModel();

            mvm.IsLoading = true;

            mvm.LoadProfilesCommand.Execute(null);

            Assert.IsFalse(mvm.IsLoading);
        }
    }
}
