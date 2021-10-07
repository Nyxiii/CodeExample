using System.Windows;
using System.Windows.Media;
using AnyListen.PluginAPI.AudioVisualisation;

namespace AnyListen.Settings.Themes.AudioVisualisation.BarAudioVisualisation
{
    public class BarAudioVisualisation : AudioVisualisationBase
    {
        private IAudioVisualisationPlugin _loadedPlugin;
        public override IAudioVisualisationPlugin Visualisation => _loadedPlugin ?? (_loadedPlugin = new AudioVisualisationPlugin());

        public override string Name => Application.Current.Resources["Bars"].ToString();

        public class AudioVisualisationPlugin : IAudioVisualisationPlugin
        {
            private IAudioVisualisation _advancedAudioVisualisation;
            public IAudioVisualisation AdvancedWindowVisualisation => _advancedAudioVisualisation ??
                                                                      (_advancedAudioVisualisation = new AdvancedWindowAudioVisualisation());

            private IAudioVisualisation _smartWindowAudioVisualisation;
            public IAudioVisualisation SmartWindowVisualisation =>