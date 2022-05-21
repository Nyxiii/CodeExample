﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AnyListen.Music.Track;
using AnyListen.ViewModelBase;
using AnyListen.Views;
using TagLib;
using Microsoft.Win32;

namespace AnyListen.ViewModels
{
    public class TagEditorViewModel : PropertyChangedBase
    {
        public File TagFile { get; set; }
        public LocalTrack Track { get; set; }

        private readonly Window _baseWindow;

        public TagEditorViewModel(LocalTrack track, Window baseWindow)
        {
            TagFile = File.Create(track.Path);
            Track = track;
            baseWindow.Closed += (s, e) => TagFile.Dispose();
            _baseWindow = baseWindow;

            AllGenres = Genres.Audio.ToList();
            AllGenres.AddRange(Enum.GetValues(typeof(Genre)).Cast<Genre>().Select(PlayableBase.GenreToString).Where(x => !AllGenres.Contains(x)));
            AllGenres.Sort();

            SelectedGenres = track.Genres.Select(PlayableBase.GenreToString).ToList();
        }

        public TagEditorViewModel()
        {
            
        }

        private RelayCommand _closeCommand;
        public RelayCommand CloseCommand
        {
            get { return _closeCommand ?? (_closeCommand = new RelayCommand(parameter => _baseWindow.Cl