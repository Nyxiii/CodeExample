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
                        CurrentLanguageDocument = LanguageDocument.FromFile(ofd.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Application.Current.Resources["Error"].ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    FilePath = ofd.FileName;
                }));
            }
        }

        private RelayCommand _newDocument;
        public RelayCommand NewDocument
        {
            get
            {
                return _newDocument ?? (_newDocument = new RelayCommand(parameter =>
                {
                    CurrentLanguageDocument = new LanguageDocument();
                    FilePath = null;
                }));
            }
        }

        private RelayCommand _saveDocument;
        public RelayCommand SaveDocument
        {
            get
            {
                return _saveDocument ?? (_saveDocument = new RelayCommand(parameter =>
                {
                    if (CurrentLanguageDocument == null) return;
                    if (string.IsNullOrEmpty(FilePath)) { SaveAs(); } else { CurrentLanguageDocument.SaveDocument(FilePath); }
                }));
            }
        }

        private RelayCommand _saveDocumentAs;
        public RelayCommand SaveDocumentAs
        {
            get
            {
                return _saveDocumentAs ?? (_saveDocumentAs = new RelayCommand(parameter =>
                {
                    if (CurrentLanguageDocument == null) return;
                    SaveAs();
                }));
            }
        }

        private void SaveAs()
        {
            var sfd = new SaveFileDialog() { Filter = "XAML files|*.xaml|All files|*.*" };
            if (sfd.ShowDialog(this) == true)
            {
                CurrentLanguageDocument.SaveDocument(sfd.FileName);
                FilePath = sfd.FileName;
            }
        }

        private RelayCommand _closeCommand;
        public RelayCommand CloseCommand
        {
            get { return _closeCommand ?? (_closeCommand = new RelayCommand(parameter => { Close(); })); }
        }

        private RelayCommand _resetValues;
        public RelayCommand ResetValues
        {
            get
            {
                return _resetValues ?? (_resetValues = new RelayCommand(parameter =>
                {
                    if (CurrentLanguageDocument != null)
                        CurrentLanguageDocument.LanguageEntries.ForEach(x => x.Value = string.Empty);
                }));
            }
        }
        #endregion

        #region INotifyPropertyChanged
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }

    public class LanguageDocument
    {
        public List<LanguageEntry> LanguageEntries { get; private set; }

        public LanguageDocument()
        {
            LanguageEntries = GetEmptyList();
            var germanDictionary = new ResourceDictionary
            {
                Source = new Uri("/Resources/Languages/AnyListen.de-de.xaml", UriKind.Relative)
            };
            var englishDictionary = new ResourceDictionary
            {
                Source = new Uri("/Resources/Languages/AnyListen.en-us.xaml", UriKind.Relative)
    