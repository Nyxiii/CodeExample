using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using AnyListen.ViewModelBase;
using Microsoft.Win32;

namespace AnyListen.Views.Tools
{
    /// <summary>
    /// Interaction logic for QueueManagerWindow.xaml
    /// </summary>
    public partial class LanguageCreatorWindow : INotifyPropertyChanged
    {
        public LanguageCreatorWindow()
        {
            InitializeComponent();
        }

        #region Properties

        private LanguageDocument _currentLanguageDocument;
        public LanguageDocument CurrentLanguageDocument
        {
            get { return _currentLanguageDocument; }
            set { _currentLanguageDocument = value; OnPropertyChanged("CurrentLanguageDocument"); }
        }

        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; OnPropertyChanged("FilePath"); }
        }

        #endregion

        #region Commands
        private RelayCommand _openDocument;
        public RelayCommand OpenDocument
        {
            get
            {
                return _openDocument ?? (_openDocument = new RelayCommand(parameter =>
                {
                    var ofd = new OpenFileDialog() { Filter = "XAML files|*.xaml|All files|*.*" };
                    if (ofd.ShowDialog() != true) return;
                    ofd.Multiselect = false;
                    ofd.CheckFileExists = true;
                    try
                    {
                        Curre