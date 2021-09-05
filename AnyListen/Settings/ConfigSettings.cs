﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Xml.Serialization;
using CSCore.SoundOut;
using AnyListen.Music.AudioEngine;
using AnyListen.Music.Download;
using AnyListen.Notification;
using AnyListen.Settings.Themes;
using MahApps.Metro.Controls;

// ReSharper disable InconsistentNaming
namespace AnyListen.Settings
{
    [Serializable, XmlType(TypeName = "Settings")]
    public class ConfigSettings : SettingsBase
    {
        public const string Filename = "config.xml";

        //CSCore
        public string SoundOutDeviceID { get; set; }
        public SoundOutMode SoundOutMode { get; set; }
        public int Latency { get; set; }
        public bool IsCrossfadeEnabled { get; set; }
        public int CrossfadeDuration { get; set; }

        //Playback
        public int WaveSourceBits { get; set; }
        public int SampleRate { get; set; }

        //Magic Arrow
        public bool ShowMagicArrowBelowCursor { get; set; }


        //Search
        public string PersonalCode { get; set; }
        public int DownloadBitrate { get; set; }
        public int LosslessPrefer { get; set; }
        public int FileNameFormat { get; set; }
        public int FileFloderFormat { get; set; }

        //Design
        public ApplicationDesign ApplicationDesign { get; set; }

        private bool _useThinHeaders;
        public bool UseThinHeaders
        {
            get { return _useThinHeaders; }
            set
            {
                SetProperty(value, ref _useThinHeaders);
            }
        }
        
        private TransitionType _tabControlTransition;
        public TransitionType TabControlTransition
        {
            get { return _tabControlTransition; }
            set
            {
                SetProperty(value, ref _tabControlTransition);
            }
        }

        //General
        public string Language { get; set; }
        public bool RememberTrackImportPlaylist { get; set; }
        public string PlaylistToImportTrack { get; set; }
        public bool ShufflePreferFavoriteTracks { get; set; }
        public bool ShowArtistAndTitle { get; set; }
        public bool MinimizeToTray { get; set; }
        public bool ShowNotificationIfMinimizeToTray { get; set; }
        public bool ShowProgressInTaskbar { get; set; }

        public List<PasswordEntry> Passwords { get; set; }

        //Notifications
        public NotificationType Notification { get; set; }
        public bool DisableNotificationInGame { get; set; }
        public int NotificationShowTime { get; set; }

        //Music
        public bool LoadAlbumCoverFromInternet { get; set; }
        public ImageQuality DownloadAlbumCoverQuality { get; set; }
        public bool SaveCoverLocal { get; set; }
        public bool TrimTrackname { get; set; }
        public DownloadManager Downloader { get; set; }
        public DownloadSettings DownloadSettings { get; set; }

        //Updates
        public bool CheckForAnyListenUpdates { get; set; }
        public bool CheckForYoutubeDlUpdates { get; set; }

        private List<LanguageInfo> _languages;
        [XmlIgnore]
        public List<LanguageInfo> Languages => _languages ?? (_languages = new List<LanguageInfo>
        {
            new LanguageInfo("简体中文", "/Resources/Languages/AnyListen.zh-cn.xaml",
                new Uri("/Resources/Languages/Icons/cn.png", UriKind.Relative), "Shelher", "zh"),
            new LanguageInfo("Deutsch", "/Resources/Languages/AnyListen.de-de.xaml",
                new Uri("/Resources/Languages/Icons/de.png", UriKind.Relative), "Alkaline", "de"),
            new LanguageInfo("English", "/Resources/Languages/AnyListen.en-us.xaml",
                new Uri("/Resources/Languages/Icons/us.png", UriKind.Relative), "Alkaline", "en"),
            new LanguageInfo("Nederlands", "/Resources/Languages/AnyListen.nl-nl.xaml",
                new Uri("/Resources/Languages/Icons/nl.png", UriKind.Relative), "DrawCase", "nl"),
            new LanguageInfo("Suomi", "/Resources/Languages/AnyListen.fi-fi.xaml",
                new Uri("/Resources/Languages/Icons/fi.png", UriKind.Relative), "Väinämö Vettenranta", "fi"),
   