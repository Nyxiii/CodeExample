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

        private Rela