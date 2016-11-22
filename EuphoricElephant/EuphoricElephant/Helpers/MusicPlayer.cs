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

        //#region 1
        //private MediaElement element = new MediaElement();

        //public byte[] Play(StorageFile track)
        //{
        //    byte[] bytes = SetElement(track);
        //    element.Play();

        //    return bytes;
        //}

        //public void Pause()
        //{
        //    element.Pause();
        //}

        //public void Resume()
        //{
        //    element.Play();
        //}

        //public void Stop()
        //{
        //    element.Stop();
        //}

        //public byte[] LoadNewTrack(StorageFile track)
        //{
        //    Stop();
        //    byte[] bytes = SetElement(track);
        //    element.Play();
        //    element.MediaEnded += OnMediaEnded;


        //    return bytes;
        //}

        //private byte[] SetElement(StorageFile track)
        //{
        //    IRandomAccessStream stream = (track.OpenAsync(FileAccessMode.Read)).AsTask().Result;
        //    element.SetSource(stream, "");

        //    return GetStreamAsByteArray(stream);
        //}

        //public byte[] GetStreamAsByteArray(IRandomAccessStream stream)
        //{
        //    var bytes = new byte[stream.Size];

        //    try
        //    {
        //        var v = Task.Run(()=>stream.ReadAsync(bytes.AsBuffer(), (uint)stream.Size, Windows.Storage.Streams.InputStreamOptions.None));
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.WriteLine(e.Message);
        //    }

        //    return bytes;
        //}

        //public void OnMediaEnded(object sender, RoutedEventArgs e)
        //{
        //    Messenger.Default.Send<OnMediaEndedMessage>(new OnMediaEndedMessage());
        //}

        //private void Media_State_Changed(object sender, RoutedEventArgs e)
        //{

        //}

        //public string getstate()
        //{
        //    return element.CurrentState.ToString();
        //}

        //public MediaElement GetElement()
        //{
        //    return element;
        //}
        //#endregion

        #region 2
        private MediaPlayer element = new MediaPlayer();

        public byte[] Play(StorageFile track)
        {
            byte[] bytes = SetElement(track);
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

        public byte[] LoadNewTrack(StorageFile track)
        {
            Stop();
            byte[] bytes = SetElement(track);
            element.Play();

            return bytes;
        }

        private byte[] SetElement(StorageFile track)
        {
            IRandomAccessStreamWithContentType stream = (IRandomAccessStreamWithContentType)(track.OpenAsync(Windows.Storage.FileAccessMode.Read)).AsTask().Result;
            element.Source = (MediaSource.CreateFromStream(stream, stream.ContentType));

            return GetStreamAsByteArray(stream);
        }

        public byte[] GetStreamAsByteArray(IRandomAccessStream stream)
        {
            var bytes = new byte[stream.Size];

            try
            {
                var v = stream.ReadAsync(bytes.AsBuffer(), (uint)stream.Size, Windows.Storage.Streams.InputStreamOptions.None);
            }
            catch (Exception e)
            {
                ErrorService.showError(e.Message);
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
                else if (element.Volume > 1)
                {
                    element.Volume = 1;
                }
                else
                {
                    element.Volume = volume;
                }
            }

        }
    }
    #endregion
}
