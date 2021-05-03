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
            Closing += NotificationTopWindow_Closing;
            Top = SystemParameters.WorkArea.Top;
            Thread t = new Thread(() =>
            {
                Thread.Sleep(timestayopened);
                if (!_isClosing) Application.Current.Dispatcher.Invoke(MoveOut);
            }) { IsBackground = true };
            t.Start();
        }

        void NotificationTopWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!_isClosing) MoveOut();
            e.Cancel = !_canClose;
        }

        private bool _isClosing;
        private bool _canClose;
        void NotificationTopWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isClosing) MoveOut();
        }

        void MoveOut()
        {
            _isClosing = true;
          