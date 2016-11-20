using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace EuphoricElephant.Services
{
    public static class AudioService
    {
        public static async Task<ObservableCollection<StorageFile>> GetAudioTracks(string instance, StorageFolder CurrentFolder)
        {
            ObservableCollection<StorageFile> Tracks = null;
            switch (instance)
            {
                case "init":
                    Tracks = new ObservableCollection<StorageFile>(await CurrentFolder.GetFilesAsync(Windows.Storage.Search.CommonFileQuery.OrderByMusicProperties));
                    break;
                case "folder":
                    Tracks = new ObservableCollection<StorageFile>(await CurrentFolder.GetFilesAsync(Windows.Storage.Search.CommonFileQuery.DefaultQuery));
                    break;
                default:
                    Tracks = new ObservableCollection<StorageFile>(await CurrentFolder.GetFilesAsync(Windows.Storage.Search.CommonFileQuery.OrderByMusicProperties));
                    break;
            }

            return Tracks;
        }
    }
}
