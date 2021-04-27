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
      