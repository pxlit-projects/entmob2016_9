using EuphoricElephant.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;

namespace EuphoricElephant.ViewModels
{
    public class MediaViewModel : BaseModel
    {
        private string currentTrack;
        private string selectedTrack;
        private ObservableCollection<string> tracks;

        public string CurrentTrack
        {
            get { return currentTrack; }
            set { SetProperty(ref currentTrack, value); }
        }

        public string SelectedTrack
        {
            get { return currentTrack; }
            set { SetProperty(ref selectedTrack, value); }
        }

        public ObservableCollection<string> Tracks
        {
            get { return tracks; }
            set { SetProperty(ref tracks, value); }
        }

        public MediaViewModel()
        {
            
        }
    }
}
