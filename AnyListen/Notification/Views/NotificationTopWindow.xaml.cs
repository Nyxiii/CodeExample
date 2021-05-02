using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using AnyListen.Music.Track;

namespace AnyListen.Notification.Views
{
    /// <summary>
    /// Interaction logic for NotificationTopWindow.xaml
    /// </summary>
    public partial class NotificationTopWindow
    {
        public NotificationTopWindow(PlayableBase track, TimeSpan timestayopened)
        {
            CurrentTrack = track;
            InitializeComponent();
            Width = SystemParameters.WorkArea.Width;
            MouseMove += NotificationTopWindow_MouseMove;
            Closing += NotificationTopWindow_Closin