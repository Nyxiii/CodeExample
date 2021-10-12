﻿using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using AnyListen.PluginAPI.AudioVisualisation;
using WPFSoundVisualizationLib;

namespace AnyListen.Settings.Themes.AudioVisualisation.RectangleVisualisation
{
    public class AdvancedWindowAudioVisualisation : IAudioVisualisation
    {
        private SpectrumAnalyzer _spectrumAnalyzer;

        public void Enable()
        {
            if (_spectrumAnalyzer != null) _spectrumAnalyzer.RefreshInterval = 20;
        }

        public void Disable()
        {
            if (_spectrumAnalyzer != null) _spectrumAnalyzer.RefreshInterval = int.MaxValue;
        }

        public void Dispose()
        {
            _spectrumA