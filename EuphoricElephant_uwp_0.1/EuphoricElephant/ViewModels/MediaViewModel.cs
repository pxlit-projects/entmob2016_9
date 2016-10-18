using EuphoricElephant.Custom;
using EuphoricElephant.Helpers;
using EuphoricElephant.Interfaces;
using EuphoricElephant.Model;
using EuphoricElephant.Models;
using EuphoricElephant.Services;
using EuphoricElephant.Views;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;
using X2CodingLab.SensorTag;

namespace EuphoricElephant.ViewModels
{
    public class MediaViewModel : BaseModel
    {
        #region Private Members
        private StorageFile selectedTrack;
        private ObservableCollection<StorageFile> tracks;
        private double currentTrackTime = 0;

        private StorageFolder currentFolder;
        private string currentFolderName = string.Empty;

        private string playButtonText = string.Empty;

        private MusicPlayer player;
        private bool isPlaying;
        private bool pressed;
        private int trackIndex = 0;
        private SensorTagDataCheck checker = new SensorTagDataCheck();
        private SensorTag activeSensor;

        #endregion

        #region Public Members  
        public IMediaView View { get; set; }

        public string PlayButtonText
        {
            get { return playButtonText; }
            set { SetProperty(ref playButtonText, value); }
        }  

        public double CurrentTrackTime
        {
            get { return currentTrackTime; }
            set { SetProperty(ref currentTrackTime, value); }
        }

        public StorageFile SelectedTrack
        {
            get { return selectedTrack; }
            set { SetProperty(ref selectedTrack, value); }
        }

        public ObservableCollection<StorageFile> Tracks
        {
            get { return tracks; }
            set { SetProperty(ref tracks, value); }
        }

        public StorageFolder CurrentFolder
        {
            get { return currentFolder; }
            set { SetProperty(ref currentFolder, value); CurrentFolderName = value.DisplayName; }
        }

        public string CurrentFolderName
        {
            get { return currentFolderName; }
            set { SetProperty(ref currentFolderName, value); }
        }
        #endregion

        #region Commands
        public ICommand PlayTrackCommand { get; set; }
        public ICommand PreviousTrackCommand { get; set; }
        public ICommand StopTrackCommand { get; set; }
        public ICommand NextTrackCommand { get; set; }
        #endregion

        #region Constructor
        public MediaViewModel()
        {
            Init();
        }
        #endregion

        #region Init
        private void Init()
        {
            CurrentFolder = KnownFolders.MusicLibrary;
            LoadMusic();
            LoadCommands();

            player = new MusicPlayer();
            PlayButtonText = "Play";

            SetTrackProperties();

            //LoadTagListener();
        }

        private void LoadCommands()
        {
            PlayTrackCommand = new CustomCommand(PlayTrackAction);
            PreviousTrackCommand = new CustomCommand(PreviousTrackAction);
            NextTrackCommand = new CustomCommand(NextTrackAction);
            StopTrackCommand = new CustomCommand(StopTrackAction);
        }

        private async void LoadMusic()
        {
            Tracks = new ObservableCollection<StorageFile>(await CurrentFolder.GetFilesAsync(Windows.Storage.Search.CommonFileQuery.OrderByMusicProperties));
            SelectedTrack = Tracks[0];
            trackIndex = 0;
            CurrentTrackTime = 0;
        }

        private async void LoadTagListener()
        {
            try
            {
                activeSensor.Accelerometer = new CustomAccelerometer();
                activeSensor.SimpleKey = new CustomSimpleKey();

                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                async () =>
                {
                    try
                    {

                        var c = (await GattUtils.GetDevicesOfService(String.Format(Constants.BASE_ID, "aa80")));

                        await activeSensor.Accelerometer.Initialize(c[0]);
                        await activeSensor.Accelerometer.CustomEnableSensor();

                        CustomSimpleKey SimpleKey = new CustomSimpleKey();

                        SimpleKey.SensorValueChanged += sk_sensorValueChanged;

                        var k = (await GattUtils.GetDevicesOfService(String.Format(Constants.SERVICE_ID, "ffe0")));

                        await SimpleKey.Initialize(k[0]);
                        await SimpleKey.EnableNotifications();


                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                    }
                });
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private async void sk_sensorValueChanged(object sender, SensorValueChangedEventArgs e)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                async () =>
                {
                    byte[] raw = e.RawData;

                    pressed = Convert.ToBoolean(raw[0]);
                    var c = true;

                    while (c)
                    {

                        if (true)
                        {
                            byte[] data = await activeSensor.Accelerometer.ReadValue();
                            checker.MovementCheck(data, player);
                        }
                        else
                        {
                            pressed = false;
                            c = false;
                        }
                    }
                }
                );
        }
        #endregion

        #region Private Methods
        private void PlayTrackAction(object param)
        {
            player.Play(SelectedTrack);

            isPlaying = true;

            SetTrackProperties();
        }

        private void StopTrackAction(object param)
        {
            player.Stop();

            SetTrackProperties();
        }

        private void PreviousTrackAction(object param)
        {
            if (isPlaying)
            {
                if (trackIndex == 0)
                {
                    trackIndex = tracks.Count() - 1;
                }
                else
                {
                    trackIndex--;
                }

                SelectedTrack = tracks.ElementAt(trackIndex);
                player.LoadNewTrack(SelectedTrack);

                SetTrackProperties();
            }
        }

        private void NextTrackAction(object param)
        {
            if (isPlaying)
            {
                if (trackIndex == tracks.Count() - 1)
                {
                    trackIndex = 0;
                }
                else
                {
                    trackIndex++;
                }

                SelectedTrack = tracks.ElementAt(trackIndex);
                player.LoadNewTrack(SelectedTrack);

                SetTrackProperties();
            }
        }

        private async void SetTrackProperties()
        {
            MusicProperties properties = null;

            if(SelectedTrack != null)
            {
                properties = await SelectedTrack.Properties.GetMusicPropertiesAsync();
            }

            if (properties != null)
            {
                CurrentTrackTime = properties.Duration.TotalSeconds;
            }
            else
            {
                CurrentTrackTime = 0;
            }

            CreateAudioImage(); 
        }

        private void CreateAudioImage()
        {
            if (View == null) return;

            var b = player.GetStreamAsByteArray().Result;

            View.DrawOnCanvas(b);
        }
        #endregion
    }
}
