using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using AnyListen.Music.Track;

namespace AnyListen.Music.Playlist
{
   public class FavoriteList : PlaylistBase
    {
       public void Initalize(IEnumerable<NormalPlaylist> playlists)
       {
           foreach (var playlist in playlists)
           {
               foreach (var track in playlist.Tracks.Where(track => track.IsFavorite))
                   Tracks.Add(track);
           }
       }

       public override void A