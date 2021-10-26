﻿using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using AnyListen.PluginAPI.AudioVisualisation;
using WPFSoundVisualizationLib;

namespace AnyListen.Settings.Themes.AudioVisualisation.SquareAudioVisualisation
{
    class SmartWindowAudioVisualisation : IAudioVisualisation
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
            _spectrumAnalyzer = null;
        }

        private ISpectrumProvider _spectrumProvider;
        public ISpectrumProvider SpectrumProvider
        {
            set { _spectrumProvider = value; }
        }

        private ColorInformation _colorInformation;
        public ColorInformation ColorInformation
        {
            set { _colorInformation = value; }
        }

        public UIElement VisualElement
        {
            get
            {
                if (_spectrumAnalyzer == null)
                {

                    var style = new Style(typeof(SpectrumAnalyzer));
                    var barStyle = new Style(typeof(Rectangle));
                    barStyle.Setters.Add(new Setter(UIElement.RenderTransformOriginProperty, new Point(.5, .5)));
                    barStyle.Setters.Add(new Setter(UIElement.RenderTrans