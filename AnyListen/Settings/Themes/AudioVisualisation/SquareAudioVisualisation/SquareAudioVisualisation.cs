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
            public IAudioVisualisation AdvancedWindowVisualisation => _advancedAudioVisualisation ??
                                                                      (_advancedAudioVisualisation = new AdvancedWindowAudioVisualisation());

            private IAudioVisualisation _smartWindowAudioVisualisation;
            public IAudioVisualisation SmartWindowVisualisation => _smartWindowAudioVisualisation ??
                                                                   (_smartWindowAudioVisualisation = new SmartWindowAudioVisualisation());

            public string Creator => "Akaline";

            private GeometryGroup _thumbnail;
            public GeometryGroup Thumbnail
            {
                get
                {
                    if (_thumbnail == null)
                    {
                        var values = new[]
                        {
                            "F1 M 0.000,337.505 L 39.447,337.505 L 39.447,376.954 L 0.000,376.954 L 0.000,337.505 Z",
                            "F1 M 0.000,289.300 L 39.447,289.300 L 39.447,328.733 L 0.000,328.733 L 0.000,289.300 Z",
                            "F1 M 0.000,241.071 L 39.447,241.071 L 39.447,280.512 L 0.000,280.512 L 0.000,241.071 Z",
                            "F1 M 0.000,192.857 L 39.447,192.857 L 39.447,232.306 L 0.000,232.306 L 0.000,192.857 Z",
                            "F1 M 0.000,144.634 L 39.447,144.634 L 39.447,184.081 L 0.000,184.081 L 0.000,144.634 Z",
                            "F1 M 0.000,96.425 L 39.447,96.425 L 39.447,135.868 L