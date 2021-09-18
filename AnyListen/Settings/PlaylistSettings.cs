﻿using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using AnyListen.Music.Playlist;

namespace AnyListen.Settings
{
    [Serializable, XmlType(TypeName = "Playlists")]
    public class PlaylistSettings : SettingsBase
    {
        protected const string Filename = "playlists.xml";

        [XmlElement("List")]
        public ObservableCollection<NormalPlaylist> Playlists { get; set; }

        public override void SetStandardValues()
        {
            Playlists = new ObservableCollection<NormalPlaylist> { new NormalPlaylist() { Name = "Default" } };
        }

        public override void Save(string programPath)
        {
            var tempFile = new FileInfo(Path.GetTempFileName());
            Save<PlaylistSettings>(tempFile.FullName);

            var settingsFile = new FileInfo(Path.Combine(programPath, Filename));
            if (settingsFile.Exists) settingsFile.Delete();
            tempFile.MoveTo(settingsFile.FullName);
        }

        public static PlaylistSettin