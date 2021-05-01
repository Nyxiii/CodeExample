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
            MouseMove += NotificationRightBottomWindow_MouseMove;
            Thread t = new Thread(() =>
            {
                Thread.Sleep(timestayopened);
                if (!_isClosing) Application.Current.Dispatcher.Invoke(MoveOut);
            }) { IsBackground = true };
            t.Start();
        }

        private bool _isClosing;
        private bool _canClose;
        void NotificationRightBottomWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isClosing) MoveOut();
        }

        void NotificationRightBottomWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!_isClosing) MoveOut();
            e.Cancel = !_ca