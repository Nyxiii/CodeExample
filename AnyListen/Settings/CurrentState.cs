using System;
using System.IO;
using System.Xml.Serialization;
using AnyListen.MagicArrow.DockManager;
using AnyListen.Music;
using AnyListen.Music.MusicEqualizer;

namespace AnyListen.Settings
{
    [Serializable]
    public class CurrentState : SettingsBase
    {
        private const string Filename = "current.xml";

        public float Volume { get; set; }

        public long TrackPosition { get; set; }
        public int LastPlaylistIndex { get; set; }
        public int LastTrackIndex { get; set; }
        public int SelectedPlaylist { get; set; }
        public int SelectedTrack { get; set; }
        public QueueManager Queue { get; set; }
        public bool IsLoopEnabled { get; set; }
        public bool IsShuffleEnabled { get; set; }
        public EqualizerSettings EqualizerSettings { get; set; }
        public DockingApplicationState ApplicationState { get; set; }
        public int MainTabControlIndex { get; set; }

        private bool _equalizerIsOpen;
        public bool EqualizerIsOp