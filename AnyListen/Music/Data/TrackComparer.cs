﻿using System.Collections.Generic;
using AnyListen.Music.Track;

namespace AnyListen.Music.Data
{
    class TrackComparer : IEqualityComparer<PlayableBase>
    {
        public Dictionary<string, string> FileHashes { get; set; }
        public bool Equals(PlayableBase x, PlayableBase y)
        {
            if (x == null || y == null) return false; //would crash if it needs to compute the hash
            return x.Equals(y);
        }

        public int GetHashCode(PlayableBase track)
        {
            //Check whether the object is null 
            if (ReferenceEquals(track, null)) return 0;

            //Get hash code for the Title field if it is not null. 
            int hashProductName = track.Title == null ? 0