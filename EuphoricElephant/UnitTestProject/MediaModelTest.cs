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

        //[TestMethod]
        //public async Task TestPlayTrack()
        //{
        //    MediaViewModel mvm = new MediaViewModel();

        //    mvm.CurrentFolder = KnownFolders.MusicLibrary;

        //    mvm.Tracks = await AudioService.GetAudioTracks("init", mvm.CurrentFolder);

        //    mvm.PlayTrackCommand.Execute(null);

        //    Assert.AreEqual("Pause", mvm.PlayButtonText);
        //}

        //[TestMethod]
        //public void TestStopTrack()
        //{
        //    MediaViewModel mvm = new MediaViewModel();

        //    mvm.StopTrackCommand.Execute(null);
        //}

        //[TestMethod]
        //public void TestOpenFolder()
        //{
        //    MediaViewModel mvm = new MediaViewModel();

        //    mvm.OpenFolderCommand.Execute(null);

        //}

        [TestMethod]
        public void TestLoadProfiles()
        {
            MediaViewModel mvm = new MediaViewModel();

            mvm.IsLoading = true;

            mvm.LoadProfilesCommand.Execute(null);

            Assert.IsFalse(mvm.IsLoading);
        }
    }
}
