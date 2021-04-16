using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Xml.Serialization;
using AnyListen.Music.CustomEventArgs;
using AnyListen.Music.Track;

namespace AnyListen.Music.Playlist
{
    [Serializable, XmlType(TypeName = "Playlist")]
    public class NormalPlaylist : PlaylistBase
    {
        private string _name;
        public override string Name
        {
       