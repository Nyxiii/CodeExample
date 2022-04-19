
﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AnyListen.DragDrop;
using AnyListen.Music;
using AnyListen.Music.CustomEventArgs;
using AnyListen.Music.Playlist;
using AnyListen.Music.Track;
using AnyListen.Settings;
using AnyListen.Utilities;
using AnyListen.ViewModelBase;
using AnyListen.Views;

namespace AnyListen.ViewModels
{
    partial class MainViewModel : PropertyChangedBase
    {
        private static MainViewModel _instance;
        public static MainViewModel Instance => _instance ?? (_instance = new MainViewModel());

        private MainViewModel()
        {
        }

        private MainWindow _baseWindow;

        private AnyListenSettings _mySettings;
        public AnyListenSettings MySettings
        {
            get { return _mySettings; }