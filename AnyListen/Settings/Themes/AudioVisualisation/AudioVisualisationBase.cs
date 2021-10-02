using System.Xml.Serialization;
using AnyListen.PluginAPI.AudioVisualisation;

namespace AnyListen.Settings.Themes.AudioVisualisation
{
    [XmlInclude(typeof(BarAudioVisualisation.BarAudioVisualisation)),
    XmlInclude(typeof(SquareAudioVisualisation.SquareAudioVisualisation)),
    XmlInclude(typeof(Rec