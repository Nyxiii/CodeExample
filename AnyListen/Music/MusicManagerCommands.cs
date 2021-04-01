using System.Collections;
using System.IO;
using System.Linq;
using System.Windows;
using AnyListen.Music.Download;
using AnyListen.Music.Track;
using AnyListen.ViewModelBase;
using AnyListen.Views;
// ReSharper disable ExplicitCallerInfoArgument

namespace AnyListen.Music
{
    public class MusicManagerCommands
    {
        #region "Constructor"

        protected MusicManager MusicManager;
        public MusicManagerCommands(MusicManager basedmanager)
        {
            MusicManager = basedmanager;
        }

        #endregion

        private RelayCommand _jumptoplayingtrack;
        public RelayCommand JumpToPlayingTrack
        {
            get
            {
                return _jumptoplayingtrack ?? (_jumptoplayingtrack = new RelayCommand(parameter =>
                {
                    if (MusicManager.FavoritePlaylist == MusicManager.CurrentPlaylist)
                        MusicManager.SelectedPlaylist = null;

                    MusicManager.SelectedPlaylist = MusicManager.CurrentPlaylist;
                    MusicManager.SelectedTrack = null;
                    MusicManager.SelectedTrack = MusicManager.CSCoreEngine.CurrentTrack;
                }));
            }
        }

        private RelayCommand _opentracklocation;
        public RelayCommand OpenTrackLocation
        {
            get
            {
                return _opentracklocation ?? (_opentracklocation = new RelayCommand(parameter =>
                {
                    var track = MusicManager.SelectedTrack;
                    if (track == null) return;
                    track.RefreshTrackExists();
                    if (!track.TrackExists) return;
                    track.OpenTrackLocation();
                }));
            }
        }

        private RelayCommand _gobackward;
        public RelayCommand GoBackward
        {
            get
            {
                return _gobackward ??
           