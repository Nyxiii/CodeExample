
﻿using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shell;
using CSCore.SoundOut;
using Hardcodet.Wpf.TaskbarNotification;
using AnyListen.AppMainWindow.Messages;
using AnyListen.AppMainWindow.WindowSkins;
using AnyListen.MagicArrow.DockManager;
using AnyListen.Music.CustomEventArgs;
using AnyListen.Settings;
using AnyListen.Utilities;
using AnyListen.Utilities.Native;
using AnyListen.ViewModels;
using AnyListen.Views;
using MahApps.Metro.Controls;
using Microsoft.Win32;

// ReSharper disable once CheckNamespace
namespace AnyListen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MagicArrow.MagicArrow MagicArrow { get; set; }
        public bool IsInSmartMode { get; set; }
        public IWindowSkin HostedWindow { get; set; }

        private IWindowSkin _smartWindowSkin;
        public IWindowSkin SmartWindowSkin => _smartWindowSkin ?? (_smartWindowSkin = new WindowSmartView());

        private IWindowSkin _advancedWindowSkin;
        public IWindowSkin AdvancedWindowSkin => _advancedWindowSkin ?? (_advancedWindowSkin = new WindowAdvancedView());

        #region Constructor & Load

        public MainWindow()
        {
            InitializeComponent();
            HostedWindow = null;
            MagicArrow = new MagicArrow.MagicArrow();
            MagicArrow.MoveOut += (s, e) =>
            {
                HideEqualizer();
                HostedWindow.DisableWindow();
            };
            MagicArrow.MoveIn += (s, e) => { HostedWindow.EnableWindow(); };
            MagicArrow.FilesDropped +=
                (s, e) => { MainViewModel.Instance.DragDropFiles((string[]) e.Data.GetData(DataFormats.FileDrop)); };
            MagicArrow.Register(this);

            Closing += MainWindow_Closing;
            Loaded += MainWindow_Loaded;
            StateChanged += MainWindow_StateChanged;

            MagicArrow.DockManager.Docked += (s, e) => { ApplyHostWindow(SmartWindowSkin); };
            MagicArrow.DockManager.Undocked += (s, e) => { ApplyHostWindow(AdvancedWindowSkin); };
            MagicArrow.DockManager.DragStopped += DockManagerOnDragStopped;

            var appsettings = AnyListenSettings.Instance.CurrentState;
            if (appsettings.ApplicationState == null)
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen;
                appsettings.ApplicationState = new DockingApplicationState
                {
                    CurrentSide = DockingSide.None,
                    Height = 600,
                    Width = 1000,
                    Left = Left,
                    Top = Top
                };
            }

            if (appsettings.ApplicationState.CurrentSide == DockingSide.None)
            {
                if (appsettings.ApplicationState.Left < WpfScreen.MostRightX)
                    //To prevent that the window is out of view when the user unplugs a monitor
                {
                    Height = appsettings.ApplicationState.Height;
                    Width = appsettings.ApplicationState.Width;
                    Left = appsettings.ApplicationState.Left;
                    Top = appsettings.ApplicationState.Top;
                    WindowState = appsettings.ApplicationState.WindowState;
                }
                else
                {
                    WindowStartupLocation = WindowStartupLocation.CenterScreen;
                }
            }

            MagicArrow.DockManager.CurrentSide = appsettings.ApplicationState.CurrentSide;
            WindowDialogService = new WindowDialogService(this);
            SystemEvents.PowerModeChanged += SystemEventsOnPowerModeChanged;
        }

        public void CenterWindowOnScreen()
        {
            var screen = WpfScreen.GetScreenFrom(this).WorkingArea;
            Left = (screen.Width / 2) - (Width / 2);
            Top = (screen.Height / 2) - (Height / 2);
        }

        public void RefreshHostWindow(bool saveInformation)
        {
            if (MagicArrow.DockManager.CurrentSide == DockingSide.None)
            {
                ApplyHostWindow(AdvancedWindowSkin, saveInformation);
            }
            else
            {
                ApplyHostWindow(SmartWindowSkin, saveInformation);
                Height = WpfScreen.GetScreenFrom(new Point(Left, 0)).WorkingArea.Height;
            }
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                MagicArrow.DockManager.ApplyCurrentSide();

                var viewmodel = MainViewModel.Instance;
                viewmodel.Loaded(this);

                RefreshHostWindow(false);

                viewmodel.MusicManager.CSCoreEngine.PlaybackStateChanged += CSCoreEngine_PlaybackStateChanged;

                ResetFlyout();
            }
            catch (Exception ex)
            {
#if (!DEBUG)
                var reportExceptionWindow = new ReportExceptionWindow(ex) { Owner = this };
                reportExceptionWindow.ShowDialog();
#else
                MessageBox.Show(ex.ToString());
#endif
            }
        }

        #endregion

        #region ApplyHostWindow

        protected void ApplyHostWindow(IWindowSkin skin, bool saveinformation = true)
        {
            if (skin == HostedWindow) return;
            if (HostedWindow != null)
            {
                HostedWindow.DragMoveStart -= skin_DragMoveStart;
                HostedWindow.DragMoveStop -= skin_DragMoveStop;
                HostedWindow.ToggleWindowState -= skin_ToggleWindowState;
                HostedWindow.TitleBarMouseMove -= skin_TitleBarMouseMove;
                HostedWindow.DisableWindow();

                var element = (FrameworkElement) HostedWindow;
                ContentGrid.Children.Remove(element);
            }

            skin.CloseRequest += (s, e) => Close();
            skin.DragMoveStart += skin_DragMoveStart;
            skin.DragMoveStop += skin_DragMoveStop;
            skin.ToggleWindowState += skin_ToggleWindowState;
            skin.TitleBarMouseMove += skin_TitleBarMouseMove;

            var appstate = AnyListenSettings.Instance.CurrentState.ApplicationState;
            if (skin != AdvancedWindowSkin && saveinformation)
            {
                appstate.Height = Height;
                appstate.Width = Width;
            }

            HideEqualizer();

            MaxHeight = skin.Configuration.MaxHeight;
            MinHeight = skin.Configuration.MinHeight;
            MaxWidth = skin.Configuration.MaxWidth;
            MinWidth = skin.Configuration.MinWidth;
            ShowTitleBar = skin.Configuration.ShowTitleBar;
            ShowSystemMenuOnRightClick = skin.Configuration.ShowSystemMenuOnRightClick;

            if (!_isDragging)
            {
                if (skin.Configuration.IsResizable)
                {
                    WindowHelper.ShowMinimizeAndMaximizeButtons(this);
                    ResizeMode = ResizeMode.CanResize;
                }
                else
                {
                    WindowHelper.HideMinimizeAndMaximizeButtons(this);
                    ResizeMode = ResizeMode.NoResize;
                }
            }

            if (skin == AdvancedWindowSkin && saveinformation)
            {
                Width = appstate.Width;
                Height = appstate.Height;
            }

            if (skin.Configuration.SupportsCustomBackground)