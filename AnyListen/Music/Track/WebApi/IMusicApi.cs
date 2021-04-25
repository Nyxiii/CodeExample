using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace AnyListen.Music.Track.WebApi
{
    public interface IMusicApi
    {
        Task<Tuple<bool, List<WebTrackResultBa