using System.Windows;
using System.Windows.Media;
using AnyListen.PluginAPI.AudioVisualisation;

namespace AnyListen.Settings.Themes.AudioVisualisation.SquareAudioVisualisation
{
    public class SquareAudioVisualisation : AudioVisualisationBase
    {
        private IAudioVisualisationPlugin _loadedPlugin;
        public override IAudioVisualisationPlugin Visualisation => _loadedPlugin ?? (_loadedPlugin = new AudioVisualisationPlugin());

        public override string Name => Application.Current.Resources["Squares"].ToString();

        public class AudioVisualisationPlugin : IAudioVisualisationPlugin
        {
            private IAudioVisualisation _advancedAudioVisualisation;
            public IAudioVisualisation AdvancedWindowVisualisation =>