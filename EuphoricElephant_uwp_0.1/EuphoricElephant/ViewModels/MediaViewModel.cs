using EuphoricElephant.Custom;
using EuphoricElephant.Enumerations;
using EuphoricElephant.Helpers;
using EuphoricElephant.Interfaces;
using EuphoricElephant.Messaging;
using EuphoricElephant.Model;
using EuphoricElephant.Models;
using EuphoricElephant.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Core;
using Windows.UI.Input;
using Windows.UI.Xaml;
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
        private bool isPlaying = false;
        private bool isPaused = false;
        private bool isLoaded = false;
        private bool isStopped = false;
        private bool isLooped = false;
        private bool pressed;
        private int TrackIndex;
        private SensorTagDataCheck checker = new SensorTagDataCheck();
        private SensorTag activeSensor;

        #endregion

        #region Public Members  
        public IMediaView View { get; set; }

        public string PlayButtonText
        {
            get {
                if (!isLoaded)
                {
                    LoadTagListener();
                    isLoaded = true;
                }

                return playButtonText; }
            set { SetProperty(ref playButtonText, value); }
        }

        public bool IsLooped
        {
            get { return isLooped; }
            set { SetProperty(ref isLooped, value); }
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

        public ICommand ToggleLoopCommand { get; set; }
        public ICommand ShuffleCommand { get; set; }
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

        }

        private void LoadCommands()
        {
            PlayTrackCommand = new CustomCommand(PlayTrackAction);
            PreviousTrackCommand = new CustomCommand(PreviousTrackAction);
            NextTrackCommand = new CustomCommand(NextTrackAction);
            StopTrackCommand = new CustomCommand(StopTrackAction);

            ToggleLoopCommand = new CustomCommand(ToggleLoopAction);
            ShuffleCommand = new CustomCommand(ShuffleAction);
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
                if (ApplicationSettings.Contains("ActiveSensor"))
                {
                    activeSensor = (SensorTag)ApplicationSettings.GetItem("ActiveSensor");

                    activeSensor.Accelerometer = new CustomAccelerometer();
                    activeSensor.SimpleKey = new CustomSimpleKey();
                }

                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                async () =>
                {
                    Task t = sk_elementValueChanged();

                    try
                    {
                        if(activeSensor != null)
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

        private async Task sk_elementValueChanged()
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                async () =>
                {
                    while (true)
                    {
                        var e = player.GetElement();

                        var x = new ObservableCollection<StorageFile>(await CurrentFolder.GetFilesAsync(Windows.Storage.Search.CommonFileQuery.OrderByMusicProperties));

                        if (e.CurrentState.ToString().Equals("Paused"))
                        {
                            if (!isPaused && isPlaying)
                            {
                                if (!IsLooped)
                                {
                                    NextTrackAction(null);
                                }
                                else
                                {
                                    isPlaying = false;
                                    PlayTrackAction(null);
                                }
                            }
                        }                  
                    }
                }
            );
        }


        private async void sk_sensorValueChanged(object sender, SensorValueChangedEventArgs e)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                async () =>
                {
                    byte[] raw = e.RawData;

                    pressed = Convert.ToBoolean(raw[0]);                    

                    while (ApplicationSettings.Contains("ActiveSensor"))
                    {

                        if (true)
                        {
                            byte[] accData = await activeSensor.Accelerometer.ReadValue();
                            //byte[] gyrData = await activeSensor.Accelerometer.ReadValue();
                            ActionType accAction = checker.MovementCheck(accData, player);
                            //ActionType gyrAction = checker.MovementCheck(accData, player);

                            lasthope.MixerInfo mi = lasthope.GetMixerControls();

                            switch (accAction)
                            {
                                case ActionType.Left:
                                    PreviousTrackAction(null);
                                    break;
                                case ActionType.Right:
                                    NextTrackAction(null);
                                    break;
                                case ActionType.Up:
                                    lasthope.AdjustVolume(mi, (mi.maxVolume - mi.minVolume) / 50);
                                    //player.changeVolume(0.1);
                                    break;
                                case ActionType.Down:
                                    //player.changeVolume(-0.1);
                                    lasthope.AdjustVolume(mi, -(mi.maxVolume - mi.minVolume) / 50);
                                    break;
                                case ActionType.Shake:
                                    ShuffleAction(null);
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            pressed = false;
                          //  c = false;
                        }
                    }
                }
                );
        }
        #endregion

        #region Private Methods
        private async void PlayTrackAction(object param)
        {
            if(!isPaused && !isPlaying)
            {
                TrackIndex = Tracks.IndexOf(SelectedTrack);

                byte[] data = await player.Play(SelectedTrack);

                PlayButtonText = "Pause";

                isPlaying = true;

                SetTrackProperties(data);
            }
            else if(!isPaused && isPlaying)
            {
                isPlaying = false;
                isPaused = true;
                player.Pause();
                PlayButtonText = "Play";           
            }
            else if(isPaused && !isPlaying)
            {
                player.Resume();
                PlayButtonText = "Pause";
                isPlaying = true;
                isPaused = false;
            }

            isStopped = false;
        }

        private void StopTrackAction(object param)
        {
            player.Stop();

            isPlaying = false;
            isPaused = false;
            isStopped = true;

            PlayButtonText = "Play";

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

                isPaused = false;
                isPlaying = false;
                isStopped = false;

                PlayButtonText = "Pause";

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

        private void ToggleLoopAction(object param)
        {
            IsLooped = !IsLooped;
        }

        private void ShuffleAction(object param)
        {
           StorageFile track = SelectedTrack;

           AudioShuffleService.ShuffleAudio(Tracks, TrackIndex);

           TrackIndex = Tracks.IndexOf(track);
           SelectedTrack = Tracks.ElementAt(TrackIndex);      

           foreach(var t in Tracks)
            {
                Debug.WriteLine(t.DisplayName);
            }

           // LoadMusic();
           
        }
            #endregion

        }
}
