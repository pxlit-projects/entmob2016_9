using EuphoricElephant.ViewModels;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
