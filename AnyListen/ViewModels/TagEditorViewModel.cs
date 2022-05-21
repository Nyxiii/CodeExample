using System;
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
            TagFile = File.