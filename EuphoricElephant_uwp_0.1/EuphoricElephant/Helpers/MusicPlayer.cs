﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;

namespace EuphoricElephant.Helpers
{
    public class MusicPlayer
    {
        private MediaElement element = new MediaElement();

        public async Task<byte[]> Play(StorageFile track)
        {
            byte[] bytes = await SetElement(track); 
            element.Play();

            return bytes;
        }

        public void Stop()
        {
            element.Stop();
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
           IRandomAccessStream stream = await track.OpenAsync(Windows.Storage.FileAccessMode.Read);
           element.SetSource(stream, "");

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
                Debug.WriteLine(e.Message);
            }

            return bytes;
        }

    }
}