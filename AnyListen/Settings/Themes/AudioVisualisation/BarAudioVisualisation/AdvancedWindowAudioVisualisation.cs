using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using AnyListen.PluginAPI.AudioVisualisation;
using WPFSoundVisualizationLib;

namespace AnyListen.Settings.Themes.AudioVisualisation.BarAudioVisualisation
{
    public class AdvancedWindowAudioVisualisation : IAudioVisualisation
    {
        private SpectrumAnalyzer _spectrumAnalyzer;

        public void Enable()
        {
     