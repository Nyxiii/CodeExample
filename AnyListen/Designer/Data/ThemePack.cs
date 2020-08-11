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


        public static bool FromFile(string fileName, out ThemePack result)
        {
            var fiSource = new FileInfo(fileName);

            using (var fs = new FileStream(fiSource.FullName, FileMode.Open, FileAccess.Read))
            using (var zf = new ZipFile(fs))
            {
                var ze = zf.GetEntry("info.json");
                if (ze == null)
                {
                    result = null;
                    return false;
                }

                using (var s = zf.GetInputStream(ze))
                using (var reader = new StreamReader(s))
                {
                    var themePack = JsonConvert.DeserializeObject<ThemePack>(reader.ReadToEnd());
                    themePack.FileName = fiSource.Name;

                    result = themePack;
                    return true;
                }
            }
        }

        public async Task Load(string filePath)
        {
            var fiSource = new FileInfo(filePath);

            using (var fs = new FileStream(fiSource.FullName, FileMode.Open, FileAccess.Read))
            using (var zf = new ZipFile(fs))
            {
                if (ContainsAudioVisualisation)
                {
                    using (var stream = zf.GetInputStream(zf.GetEntry(ThemePackConsts.AudioVisualisationName)))
                    {
                        _audioVisualisationPlugin = await Task.Run(() => AudioVisualisationPluginHelper.FromStream(stream));
                    }
                }

                if (ContainsBackground)
                {
                    var path = "AnyListenBackground" + BackgroundName;
                    var backgroundZipEntry = zf.GetEntry(BackgroundName);
                    using (var zipStream = zf.GetInputStream(backgroundZipEntry))
                    {
                        var buffer = new byte[4096];
                        var file = new FileInfo(path);
                        if (file.Exists) file.Delete();
                        using (var streamWriter = File.Create(file.FullName))
                        {
                            StreamUtils.Copy(zipStream, streamWriter, buffer);
   