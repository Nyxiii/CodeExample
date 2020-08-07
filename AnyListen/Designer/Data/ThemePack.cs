using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Xml.Serialization;
using AnyListen.PluginAPI.AudioVisualisation;
using AnyListen.Settings.Themes;
using AnyListen.Settings.Themes.AudioVisualisation;
using AnyListen.Settings.Themes.Background;
using AnyListen.Settings.Themes.Visual;
using AnyListen.Utilities;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using Newtonsoft.Json;

namespace AnyListen.Designer.Data
{
    public class ThemePack : IApplicationBackground, IAppTheme, IAccentColor, IAudioVisualisationContainer
    {
        [XmlIgnore]
        public string Creator { get; set; }

        [XmlIgnore]
        public string Name { get; set; }

        public string FileName { get; set; }

        #region ContainInfo

        [XmlIgnore]
        public bool ContainsAppTheme { get; set; }

        [XmlIgnore]
        public bool ContainsAccentColor { get; set; }

        [XmlIgnore]
        public bool ContainsAudioVisualisation { get; set; }

        [XmlIgnore]
        public bool ContainsBackground { get; set; }

        #endregion

        [XmlIgnore]
        public string BackgroundName { get; set; }


        public static bool FromFile(string fileNam