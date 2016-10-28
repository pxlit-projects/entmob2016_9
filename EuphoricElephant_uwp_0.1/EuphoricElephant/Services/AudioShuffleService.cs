using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace EuphoricElephant.Services
{
    public static class AudioShuffleService
    {
        public static void ShuffleAudio(ObservableCollection<StorageFile> Tracks, int ndx)
        {
            ObservableCollection<StorageFile> intermediate = new ObservableCollection<StorageFile>();
            foreach (var t in Tracks)
            {
                intermediate.Add(t);
            }
            Tracks.Clear();

            Tracks.Add(intermediate[ndx]);
            intermediate.RemoveAt(ndx);

            Random R = new Random();
            while (intermediate.Count > 0)
            {
                int index = R.Next(0, intermediate.Count - 1);
                Tracks.Add(intermediate[index]);
                intermediate.RemoveAt(index);
            }

        }
    }
}
