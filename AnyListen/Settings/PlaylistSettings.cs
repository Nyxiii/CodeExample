using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using AnyListen.Music.Playlist;

namespace AnyListen.Settings
{
    [Serializable, XmlType(TypeName = "Playlists")]
    public class PlaylistSettings : SettingsBase
    {
        protected const string 