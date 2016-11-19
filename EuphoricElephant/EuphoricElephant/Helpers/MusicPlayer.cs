using EuphoricElephant.Messaging;
using EuphoricElephant.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace EuphoricElephant.Helpers
{
    public class MusicPlayer
    {
        private MediaPlayer element = new MediaPlayer();

        //private MediaElement element = new MediaElement();
        

        public async Task<byte[]> Play(StorageFile track)
        {
            byte[] bytes = await SetElement(track);
            element.Play();

            return bytes;
        }

        public void Pause()
        {
            element.Pause();
        }

        public void Resume()
        {
            element.Play();
        }

        public void Stop()
        {
            element.Pause();
        }

        public async Task<byte[]> LoadNewTrack(StorageFile track)
        {
            Stop();
            byte[] bytes = await SetElement(track);
            element.Play();

            return bytes;
        }

        private async Task<byte[]> SetElement(StorageFile track)
        {
            IRandomAccessStreamWithContentType stream = (IRandomAccessStreamWithContentType)await track.OpenAsync(Windows.Storage.FileAccessMode.Read);
            element.Source = (MediaSource.CreateFromStream(stream, stream.ContentType));

            return await GetStreamAsByteArray(stream);
        }

        public async Task<byte[]> GetStreamAsByteArray(IRandomAccessStream stream)
        {
            var bytes = new byte[stream.Size];

            try
            {
                var v = await stream.ReadAsync(bytes.AsBuffer(), (uint)stream.Size, Windows.Storage.Streams.InputStreamOptions.None);
            }
            catch (Exception e)
            {
                await ErrorService.showError(e.Message);
            }

            return bytes;
        }

        public string getstate()
        {
            return element.PlaybackSession.PlaybackState.ToString();
        }
        
        public MediaPlayer GetElement()
        {
            return element;
        }

        public void SetVolume(double delta)
        {
            double volume = element.Volume;

            if (element.Volume <= 1 && element.Volume >= 0)
            {
                volume += delta;

                if (element.Volume < 0)
                {
                    element.Volume = 0;
                }
                else if(element.Volume > 1)
                {
                    element.Volume = 1;
                }
                else
                {
                    element.Volume = volume;
                }

                //element.Play();
            }
            
        }
    }
}
