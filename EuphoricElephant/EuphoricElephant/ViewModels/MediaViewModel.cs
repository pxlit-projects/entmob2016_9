using EuphoricElephant.Custom;
using EuphoricElephant.Data;
using EuphoricElephant.Enumerations;
using EuphoricElephant.Helpers;
using EuphoricElephant.Messaging;
using EuphoricElephant.Model;
using EuphoricElephant.Models;
using EuphoricElephant.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
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
        private bool isTagListenerLoaded = false;
        private bool isPlaying = false;
        private bool isPaused = false;
        private bool isStopped = false;
        private bool isLoading = true;
        private bool isLooped = false;
        private bool pressed;
        private int TrackIndex;
        private SensorTagDataCheck checker = new SensorTagDataCheck();
        private SensorTag activeSensor;

        private User currentUser;
        private Profile userProfile;

        private DispatcherTimer dispatcherTimer;
        #endregion

        #region Public Members  
        // public IMediaView View { get; set; }

        public string PlayButtonText
        {
            get { return playButtonText; }
            set { SetProperty(ref playButtonText, value); }
        }

        public bool IsLooped
        {
            get { return isLooped; }
            set { SetProperty(ref isLooped, value); }
        }

        public bool IsLoading
        {
            get { return isLoading; }
            set { SetProperty(ref isLoading, value); }
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
        public ICommand OpenFolderCommand { get; set; }

        public ICommand LoadProfilesCommand { get; set; }
        #endregion

        #region Constructor
        public MediaViewModel()
        {
            LoadCommands();
            RegisterMessages();
        }
        #endregion

        #region Init

        private void RegisterMessages()
        {
            Messenger.Default.Register<NavigationMessage>(this, OnNavigationMessageRecieved);
            Messenger.Default.Register<BackRequestedMessage>(this, OnBackRequestedMessageRecieved);
        }

        private void OnNavigationMessageRecieved(NavigationMessage m)
        {
            if (m.Type == Enumerations.ViewType.MediaPlayerViewType)
            {
                Init();
            }
        }

        private void OnBackRequestedMessageRecieved(BackRequestedMessage obj)
        {
            StopTrackAction(null);
        }

        private void Init()
        {
            if (ApplicationSettings.Contains("MediaPlayer"))
            {
                player = (MusicPlayer)ApplicationSettings.GetItem("MediaPlayer");

                if (!isTagListenerLoaded)
                {
                    LoadTagListener();
                }
            }
            else
            {
                PlayButtonText = "Play";
                player = new MusicPlayer();
                ApplicationSettings.AddItem("MediaPlayer", player);

                if (!isTagListenerLoaded)
                {
                    LoadTagListener();
                }

                CurrentFolder = KnownFolders.MusicLibrary;
                LoadMusic("init");

                SetTrackProperties(null);
            }

            IsLoading = false;
        }

        private void LoadCommands()
        {
            PlayTrackCommand = new CustomCommand(PlayTrackAction);
            PreviousTrackCommand = new CustomCommand(PreviousTrackAction);
            NextTrackCommand = new CustomCommand(NextTrackAction);
            StopTrackCommand = new CustomCommand(StopTrackAction);

            ToggleLoopCommand = new CustomCommand(ToggleLoopAction);
            ShuffleCommand = new CustomCommand(ShuffleAction);
            OpenFolderCommand = new CustomCommand(OpenFolder);

            LoadProfilesCommand = new CustomCommand(LoadProfilesAction);
        }

        private async void LoadMusic(String instance)
        {
            Tracks = await AudioService.GetAudioTracks(instance, CurrentFolder);

            Tracks = AudioFilterService.FilterAudio(Tracks);
            if (Tracks.Count() != 0)
            {
                SelectedTrack = Tracks[0];
                TrackIndex = 0;
                CurrentTrackTime = 0;
            }
        }

        private async void LoadTagListener()
        {
            try
            {
                if (ApplicationSettings.Contains("ActiveSensor") && userProfile != null)
                {
                    activeSensor = (SensorTag)ApplicationSettings.GetItem("ActiveSensor");

                    activeSensor.Accelerometer = new CustomAccelerometer();
                    activeSensor.SimpleKey = new CustomSimpleKey();

                    SensorListener();

                }
                else
                {
                    isTagListenerLoaded = false;
                }
            }
            catch (Exception e)
            {
                await ErrorService.showError(e.Message);
            }
        }

        private async void SensorListener()
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    async () =>
                    {
                        try
                        {
                            if (activeSensor != null)
                            {
                                var c = (await GattUtils.GetDevicesOfService(String.Format(Constants.BASE_ID, "aa80")));

                                await activeSensor.Accelerometer.Initialize(c[0]);
                                await activeSensor.Accelerometer.CustomEnableSensor();

                                CustomSimpleKey SimpleKey = new CustomSimpleKey();

                                SimpleKey.SensorValueChanged += sk_sensorValueChanged;

                                var k = (await GattUtils.GetDevicesOfService(String.Format(Constants.SERVICE_ID, "ffe0")));

                                await SimpleKey.Initialize(k[0]);
                                await SimpleKey.EnableNotifications();

                                isTagListenerLoaded = true;
                            }
                        }
                        catch (Exception e)
                        {
                            await ErrorService.showError(e.Message);
                        }
                    });
        }

        private async Task sk_elementValueChanged()
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                async () =>
                {
                    while (true)
                    {
                        var e = player.GetElement();

                        var x = new ObservableCollection<StorageFile>(await CurrentFolder.GetFilesAsync(Windows.Storage.Search.CommonFileQuery.DefaultQuery));

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

                        if (isLoading) //change true to pressed
                        {
                            byte[] accData = await activeSensor.Accelerometer.ReadValue();
                            ActionType accAction = checker.MovementCheck(accData, player);

                            lasthope.MixerInfo mi = lasthope.GetMixerControls();

                            switch (ProfileService.getCommandType(userProfile, accAction))
                            {
                                case CommandType.PreviousTrack:
                                    PreviousTrackAction(null);
                                    break;
                                case CommandType.NextTrack:
                                    NextTrackAction(null);
                                    break;
                                case CommandType.VolumeUp:
                                    lasthope.AdjustVolume(mi, (mi.maxVolume - mi.minVolume) / 50);
                                    break;
                                case CommandType.VolumeDown:
                                    lasthope.AdjustVolume(mi, -(mi.maxVolume - mi.minVolume) / 50);
                                    break;
                                case CommandType.Shuffle:
                                    ShuffleAction(null);
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            pressed = false;
                        }
                    }
                });
        }

        private void dispatcherTimer_Tick(object sender, object e)
        {
            if (CurrentTrackTime > 0)
            {
                CurrentTrackTime--;
            }
            else
            {
                StopTrackAction(null);
            }
        }
        #endregion

        #region Private Methods
        private void PlayTrackAction(object param)
        {
            if (!isPaused && !isPlaying)
            {
                TrackIndex = Tracks.IndexOf(SelectedTrack);

                byte[] data = Task.Run(() => player.Play(SelectedTrack)).Result;

                PlayButtonText = "Pause";

                isPlaying = true;

                SetTrackProperties(data);

                dispatcherTimer = new DispatcherTimer();
                dispatcherTimer.Tick += dispatcherTimer_Tick;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                dispatcherTimer.Start();
            }
            else if (!isPaused && isPlaying)
            {
                isPlaying = false;
                isPaused = true;
                player.Pause();
                PlayButtonText = "Play";

                dispatcherTimer.Stop();
            }
            else if (isPaused && !isPlaying)
            {
                player.Resume();
                PlayButtonText = "Pause";
                isPlaying = true;
                isPaused = false;

                dispatcherTimer = new DispatcherTimer();
                dispatcherTimer.Tick += dispatcherTimer_Tick;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                dispatcherTimer.Start();
            }

            isStopped = false;
        }

        private void StopTrackAction(object param)
        {
            dispatcherTimer.Stop();
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

            if (SelectedTrack != null)
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
        }

        private async void LoadProfilesAction(object param)
        {
            if (ApplicationSettings.Contains("CurrentUser"))
            {
                IsLoading = true;
                currentUser = (User)ApplicationSettings.GetItem("CurrentUser");

                ObservableCollection<Profile> profiles = new ObservableCollection<Profile>(Task.Run(() => JSonParseService2<List<Profile>>.DeserializeDataFromJson(Constants.PROFILE_BY_USERID_URL, currentUser.userId.ToString())).Result);

                userProfile = profiles.Where(x => x.profileId == currentUser.defaultProfileId).SingleOrDefault();

                IsLoading = false;

                if (!isTagListenerLoaded)
                {
                    LoadTagListener();
                }
            }
            else
            {
                await ErrorService.showError("No user selected.");
            }
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
        }

        private async void OpenFolder(object param)
        {
            FolderPicker fp = new FolderPicker();
            fp.SuggestedStartLocation = PickerLocationId.Desktop;
            fp.FileTypeFilter.Add(".m4a");
            fp.FileTypeFilter.Add(".mp3");
            fp.FileTypeFilter.Add(".wma");
            fp.FileTypeFilter.Add(".flac");

            Windows.Storage.StorageFolder folder = await fp.PickSingleFolderAsync();
            if (folder != null)
            {
                // Application now has read/write access to all contents in the picked folder
                // (including other sub-folder contents)
                Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);

                CurrentFolder = folder;
                LoadMusic("folder");
            }
        }
        #endregion
    }
}
