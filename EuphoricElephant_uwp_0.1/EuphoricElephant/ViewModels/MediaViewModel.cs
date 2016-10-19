using EuphoricElephant.Custom;
using EuphoricElephant.Enumerations;
using EuphoricElephant.Helpers;
using EuphoricElephant.Interfaces;
using EuphoricElephant.Model;
using EuphoricElephant.Models;
using EuphoricElephant.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Core;
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
        private int TrackIndex;
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

            SetTrackProperties(null);

            LoadTagListener();
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
            Tracks = AudioFilterService.FilterAudio(Tracks);

            SelectedTrack = Tracks[0];
            TrackIndex = 0;
            CurrentTrackTime = 0;
        }

        private async void LoadTagListener()
        {
            try
            {
                activeSensor = (SensorTag)ApplicationSettings.GetItem("ActiveSensor");

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
                            ActionType action = checker.MovementCheck(data, player);

                            lasthope.MixerInfo mi = lasthope.GetMixerControls();

                            switch (action)
                            {
                                case ActionType.Left:
                                    PreviousTrackAction(null);
                                    break;
                                case ActionType.Right:
                                    NextTrackAction(null);
                                    break;
                                case ActionType.Up:
                                    lasthope.AdjustVolume(mi, (mi.maxVolume - mi.minVolume) / 50);
                                    break;
                                case ActionType.Down:
                                    lasthope.AdjustVolume(mi, -(mi.maxVolume - mi.minVolume) / 50);
                                    break;
                                default:
                                    break;
                            }
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
        private async void PlayTrackAction(object param)
        {
            TrackIndex = Tracks.IndexOf(SelectedTrack);

            byte[] data = await player.Play(SelectedTrack);           

            isPlaying = true;

            SetTrackProperties(data);
        }

        private void StopTrackAction(object param)
        {
            player.Stop();

            isPlaying = false;

            SetTrackProperties(null);
        }

        private async void PreviousTrackAction(object param)
        {
            if (isPlaying)
            {
                if (TrackIndex == 0)
                {
                    TrackIndex = tracks.Count() - 1;
                }
                else
                {
                    TrackIndex--;
                }

                SelectedTrack = tracks.ElementAt(TrackIndex);
                byte[] data = await player.LoadNewTrack(SelectedTrack);

                SetTrackProperties(data);
            }
        }

        private async void NextTrackAction(object param)
        {
            if (isPlaying)
            {
                if (TrackIndex == tracks.Count() - 1)
                {
                    TrackIndex = 0;
                }
                else
                {
                    TrackIndex++;
                }

                SelectedTrack = tracks.ElementAt(TrackIndex);
                byte[] data = await player.LoadNewTrack(SelectedTrack);

                SetTrackProperties(data);
            }
        }

        private async void SetTrackProperties(byte[] data)
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

            CreateAudioImage(data);          
        }

        private void CreateAudioImage(byte[] data)
        {
            if (View == null) return;
            View.DrawOnCanvas(data);
        }
        #endregion
    }
}
