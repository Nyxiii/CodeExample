
﻿using System;
using System.Windows;
using System.Windows.Controls;
using AnyListen.Designer.Data;
using AnyListen.Designer.Data.ThemeData;
using AnyListen.Designer.Pages;
using AnyListen.Settings.Themes;
using AnyListen.ViewModelBase;
using Microsoft.Win32;

namespace AnyListen.Designer
{
    public class DesignerViewModel : PropertyChangedBase
    {
        #region "Singleton & Constructor"

        private static DesignerViewModel _instance;
        public static DesignerViewModel Instance => _instance ?? (_instance = new DesignerViewModel());


        private DesignerViewModel()
        {
            CurrentTitle = "AnyListen Designer";
            ApplicationThemeManager.Instance.Refresh();
        }

        #endregion

        private ISaveable _currentElement;
        public ISaveable CurrentElement
        {
            get { return _currentElement; }
            set
            {
                SetProperty(value, ref _currentElement);
            }
        }
        
        private UserControl _currentView;
        public UserControl CurrentView
        {
            get { return _currentView; }
            set
            {
                SetProperty(value, ref _currentView);   
            }
        }

        private UserControl _previewControl;
        public UserControl PreviewControl
        {
            get { return _previewControl ?? (_previewControl = new HomePage()); }
            set
            {
                SetProperty(value, ref _previewControl);
            }
        }

        private IPreviewable _previewData;
        public IPreviewable PreviewData
        {
            get { return _previewData; }
            set
            {
                SetProperty(value, ref _previewData);
            }
        }

        private RelayCommand _createNewThemePack;
        public RelayCommand CreateNewThemePack
        {
            get
            {
                return _createNewThemePack ?? (_createNewThemePack = new RelayCommand(parameter =>
                {
                    ThemePackViewModel.Instance.ThemePack = new ThemePack();
                    CurrentView = new ThemePackPage();