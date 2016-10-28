using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace EuphoricElephant.Services
{
    public static class AudioFilterService
    {
        private static List<string> ApprovedExtensions = new List<string> { ".mp3", ".flac", ".m4a", ".wma" };

        public static ObservableCollection<StorageFile> FilterAudio(ObservableCollection<StorageFile> Tracks)
        {
            ObservableCollection<StorageFile> filteredTracks = new ObservableCollection<StorageFile>();

            foreach(var t in Tracks)
            {
                if (ApprovedExtensions.Contains(t.FileType.ToString()))
                {
                    filteredTracks.Add(t);
                }
            }

            return filteredTracks;
        }
    }
}
