
﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using AnyListen.Music.AudioEngine;
using AnyListen.Music.CustomEventArgs;
using AnyListen.Music.Data;
using AnyListen.Music.Download;
using AnyListen.Music.Playlist;
using AnyListen.Music.Track;
using AnyListen.Music.Track.WebApi;
using AnyListen.Notification;
using AnyListen.Settings;
using AnyListen.ViewModelBase;
// ReSharper disable ExplicitCallerInfoArgument

namespace AnyListen.Music
{
   public class MusicManager : PropertyChangedBase, IDisposable
    {
        #region Public Properties
        private PlayableBase _selectedtrack;
        public PlayableBase SelectedTrack
        {
            get { return _selectedtrack; }
            set
            {
                SetProperty(value, ref _selectedtrack);
            }
        }

        private bool _isloopenabled;
        public bool IsLoopEnabled
        {
            get { return _isloopenabled; }
            set
            {
                if (SetProperty(value, ref _isloopenabled) && value) IsShuffleEnabled = false;
            }
        }

        private bool _isshuffleenabled;
        public bool IsShuffleEnabled
        {
            get { return _isshuffleenabled; }
            set
            {
                if (SetProperty(value, ref _isshuffleenabled) && value) IsLoopEnabled = false;
            }
        }

        private IPlaylist _selectedplaylist;
        public IPlaylist SelectedPlaylist
        {
            get { return _selectedplaylist; }
            set
            {
                if (SetProperty(value, ref _selectedplaylist))
                    OnPropertyChanged("FavoriteListIsSelected");
            }
        }

        private String _searchtext;
        public String SearchText
        {
            get { return _searchtext; }
            set
            {
                SetProperty(value, ref _searchtext);
                SelectedPlaylist?.ViewSource?.Refresh();
            }
        }

        private ObservableCollection<NormalPlaylist> _playlists;
        public ObservableCollection<NormalPlaylist> Playlists
        {
            get { return _playlists; }
            set
            {
                SetProperty(value, ref _playlists);
            }
        }

        private TrackSearcher _trackSearcher;
        public TrackSearcher TrackSearcher
        {
            get { return _trackSearcher; }
            set
            {
                SetProperty(value, ref _trackSearcher);
            }
        }

        //WARNING: The different between the Current- and the SelectedPlaylist is, that the current playlist is the playlist who is played. The selected playlist is the playlist the user sees (can be the same)
        private IPlaylist _currentplaylist;
        public IPlaylist CurrentPlaylist
        {
            get { return _currentplaylist; }
            set
            {
                SetProperty(value, ref _currentplaylist);
            }
        }

       // ReSharper disable once InconsistentNaming
        public CSCoreEngine CSCoreEngine { get; protected set; }

        public NotificationService Notification { get; set; }

        public MusicManagerCommands Commands { get; protected set; }

        public QueueManager Queue { get; set; }

        private DownloadManager _downloadManager;
        public DownloadManager DownloadManager
        {
            get { return _downloadManager; }
            set
            {
                SetProperty(value, ref _downloadManager);
            }
        }

        public FavoriteList FavoritePlaylist { get; private set; }

        public bool FavoriteListIsSelected
        {
            get { return SelectedPlaylist == FavoritePlaylist; }
            set
            {
                if (value)
                {
                    SelectedPlaylist = null;
                    SelectedPlaylist = FavoritePlaylist;
                }
                else
                {
                    SelectedPlaylist = Playlists[0];
                }

            }
        }

        #endregion

        #region Contructor and Loading
        public MusicManager()
        {
            CSCoreEngine = new CSCoreEngine();
            Playlists = new ObservableCollection<NormalPlaylist>();
            CSCoreEngine.TrackFinished += CSCoreEngine_TrackFinished;
            CSCoreEngine.TrackChanged += CSCoreEngine_TrackChanged;
            Notification = new NotificationService(CSCoreEngine);
            Commands = new MusicManagerCommands(this);

            Random = new Random();
            Lasttracks = new List<TrackPlaylistPair>();
            Queue = new QueueManager();
            DownloadManager = new DownloadManager();
            TrackSearcher = new TrackSearcher(this, (MainWindow)Application.Current.MainWindow);
        }

        public async void LoadFromSettings()
        {
            AnyListenSettings settings = AnyListenSettings.Instance;
            Playlists = settings.Playlists.Playlists;
            var currentState = settings.CurrentState;
            CSCoreEngine.EqualizerSettings = currentState.EqualizerSettings;
            CSCoreEngine.EqualizerSettings.Loaded();
            CSCoreEngine.Volume = currentState.Volume;
            DownloadManager = settings.Config.Downloader;

            FavoritePlaylist = new FavoriteList();
            FavoritePlaylist.Initalize(Playlists);

            if (currentState.LastPlaylistIndex > -10)
            {
                CurrentPlaylist = IndexToPlaylist(currentState.LastPlaylistIndex);
            }

            SelectedPlaylist = IndexToPlaylist(currentState.SelectedPlaylist);

            if (currentState.SelectedTrack > -1 && currentState.SelectedTrack < SelectedPlaylist.Tracks.Count)
            {
                SelectedTrack = SelectedPlaylist.Tracks[currentState.SelectedTrack];
            }
            IsLoopEnabled = currentState.IsLoopEnabled;
            IsShuffleEnabled = currentState.IsShuffleEnabled;
            foreach (NormalPlaylist lst in Playlists)
            {
                lst.LoadList();
            }
            FavoritePlaylist.LoadList();
            if (currentState.Queue != null) { Queue = currentState.Queue; Queue.Initialize(Playlists); }

            if (currentState.LastTrackIndex > -1 && currentState.LastTrackIndex < SelectedPlaylist.Tracks.Count)
            {
                PlayableBase t = CurrentPlaylist.Tracks[currentState.LastTrackIndex];
                if (t.TrackExists && currentState.TrackPosition >= 0)
                {
                    await CSCoreEngine.OpenTrack(t);
                    CSCoreEngine.Position = currentState.TrackPosition;
                    CSCoreEngine.OnPropertyChanged("Position");
                }
            }

            AsyncTrackLoader.Instance.RunAsync(Playlists.Cast<IPlaylist>().ToList());
        }
        #endregion

        #region Event Handler
        async void CSCoreEngine_TrackFinished(object sender, EventArgs e)
        {
            if (IsLoopEnabled)
            {
                if (await CSCoreEngine.OpenTrack(CSCoreEngine.CurrentTrack))
                    CSCoreEngine.TogglePlayPause();
            }
            else
            {
                GoForward();
            }
        }

        void CSCoreEngine_TrackChanged(object sender, TrackChangedEventArgs e)
        {
            if (_openedTrackWithStandardBackward) { _openedTrackWithStandardBackward = false; return; }
            if (Lasttracks.Count == 0 || Lasttracks.Last().Track != e.NewTrack)
                Lasttracks.Add(new TrackPlaylistPair(e.NewTrack, CurrentPlaylist));
        }
        #endregion

        #region Protected Members and Methods
        protected Random Random;
        protected List<TrackPlaylistPair> Lasttracks;

        #endregion

        #region Public Methods
        public void RegisterPlaylist(NormalPlaylist playlist)
        {
            playlist.LoadList();
        }

        public async void PlayTrack(PlayableBase track, IPlaylist playlist)
        {