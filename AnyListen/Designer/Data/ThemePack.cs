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
    public class ThemePack : IApplicatio