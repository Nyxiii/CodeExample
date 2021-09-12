using System;
using System.IO;
using System.Xml.Serialization;
using AnyListen.MagicArrow.DockManager;
using AnyListen.Music;
using AnyListen.Music.MusicEqualizer;

namespace AnyListen.Settings
{
    [Serializable]
    public class CurrentState : SettingsBase
    {
        private const