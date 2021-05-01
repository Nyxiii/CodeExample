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
    /// Interaction logic for NotificationRightBottomWindow.xaml
    /// </summary>
    public partial class NotificationRightBottomWindow
    {
        public NotificationRightBottomWindow(PlayableBase track, TimeSpan timestayopened)
        {
            CurrentTrack = track;
            InitializeComponent();
            Top = SystemParameters.WorkArea.Height - Height;
            Width = SystemParameters.WorkArea.Width / 4;
            Left = SystemParameters.WorkArea.Width - Width;
            Closing += NotificationRightBottomWindow_Closing;
     