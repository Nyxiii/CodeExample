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
        public const string Filename = "c