using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml.Controls;

namespace EuphoricElephant.Helpers
{
    public class MusicPlayer
    {
        private MediaElement element = new MediaElement();
        private Windows.Storage.Streams.IRandomAccessStream stream;

        public async void Play(StorageFile track)
        {
            stream = await track.OpenAsync(Windows.Storage.FileAccessMode.Read);

            element.SetSource(stream, "");
            element.Play();            
        }

        public void Stop()
        {
            element.Stop();
        }

        public async void LoadNewTrack(StorageFile track)
        {
            Stop();
            stream.Dispose();
            stream = await track.OpenAsync(Windows.Storage.FileAccessMode.Read);
            element.SetSource(stream, "");
            Play(track);
        }

    }
}
