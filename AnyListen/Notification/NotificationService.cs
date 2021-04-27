using System;
using System.Reflection;
using System.Windows;
using AnyListen.Music.AudioEngine;
using AnyListen.Music.CustomEventArgs;
using AnyListen.Music.Track;
using AnyListen.Notification.Views;
using AnyListen.Settings;
using AnyListen.Utilities;
using AnyListen.Utilities.Native;

namespace AnyListen.Notification
{
    public class NotificationService
    {
        public NotificationService(CSCoreEngine cscore)
        {
            cscore.TrackChanged += cscore_TrackChanged;
        }

        void cscore_TrackChanged(object sender, TrackChangedEventArgs e)
        {
            TrackChanged(e.NewTrack);
        }

        private Window _lastwindow;
        private PlayableBase _lasttrack;

        void TrackChanged(PlayableBase newtrack)
        {
            ConfigSettings config = AnyListenSettings.Instance.Config;
            if (config.DisableNotificationInGame && WindowHelper.WindowIsFullscreen(UnsafeNativeMethods.GetForegroundWindow())) return;
            ShowNotification(newtrack, config.Notification);

            _lasttrack = newtrack;
        }

        protected void ShowNotification(PlayableBase track, NotificationType type)
        {
            ConfigSettings config = AnyListenSettings.Instance.Config;
            if (config.Notification == NotificationType.None) return;
            if (_lastwindow !