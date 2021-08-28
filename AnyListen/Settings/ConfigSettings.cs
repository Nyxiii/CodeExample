using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Xml.Serialization;
using CSCore.SoundOut;
using AnyListen.Music.AudioEngine;
using AnyListen.Music.Download;
using AnyListen.Notification;
using AnyListen.Settings.Themes;
using MahApps.Metro.Controls;

// ReSharper disable InconsistentNaming
namespace AnyListen.Settings
{
    [Serializable, XmlType(TypeName = "Settings")]
    public class ConfigSettings : SettingsBase
    {
        public const string Filename = "config.xml";

        //CSCore
        public string SoundOutDeviceID { get; set; }
        public SoundOutMode SoundOutMode { get; set; }
        public int Latency { get; set; }
        public bool IsCrossfadeEnabled { get; set; }
        public int CrossfadeDuration { get; set; }

        //Playback
        public int WaveSourceBits { get; set; }
        public int SampleRate { get; set; }

        //Magic Arrow
        public bool ShowMagicArrowBelowCursor { get; set; }


        //Search
        public string PersonalCode { get; set; }
        public int DownloadBitrate { get; set; }
        public int LosslessPrefer { get; set; }
        public int FileNameFormat { get; set; }
        public int FileFloderFormat { get; set; }

        