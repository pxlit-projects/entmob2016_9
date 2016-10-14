using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace EuphoricElephant.Helpers
{
    public class MusicPlayer
    {

        private MediaElement element = new MediaElement();
        private StorageFolder folder;
        private StorageFile file;
        private Windows.Storage.Streams.IRandomAccessStream stream;
        private int song;
        private int max_song;
        private IReadOnlyList<StorageFile> album;

        public async void init()
        {
            folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("MyFolder");
            album = await folder.GetFilesAsync();
            song = 0;
            max_song = album.Count - 1;
            file = album.ElementAt(song);
            stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
            element.SetSource(stream, "");            
        }

        public void play()
        {
            element.Play();            
        }

        public void stop()
        {
            element.Stop();
        }

        public async void next()
        {
            stop();
            stream.Dispose();
            if (song < max_song)
            {
                song += 1;
            }
            else
            {
                song = 0;
            }
            file = album.ElementAt(song);
            stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
            element.SetSource(stream, "");
            play();
        }

        

        public async void previous()
        {
            stop();
            stream.Dispose();
            if (song > 0)
            {
                song -= 1;
            }
            else
            {
                song = max_song;
            }
            file = album.ElementAt(song);
            stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
            element.SetSource(stream, "");
            play();
        }


        internal string getTitle()
        {
            return album.ElementAt(song).DisplayName;
        }
    }
}
