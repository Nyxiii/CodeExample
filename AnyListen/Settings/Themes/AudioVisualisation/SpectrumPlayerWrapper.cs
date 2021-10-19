using System;
using System.ComponentModel;
using AnyListen.PluginAPI.AudioVisualisation;
using WPFSoundVisualizationLib;

namespace AnyListen.Settings.Themes.AudioVisualisation
{
    public class SpectrumPlayerWrapper : ISpectrumPlayer
    {
        private readonly ISpectrumProvider _spectrumProvider;
        public SpectrumPlayerWrapper(ISpectrumProvider spectrumProvider)
        {
            _spectrumProvider = spectrumProvider;
            _spectrumProvider.PlayStateChanged += _spectrumProvider_PlayStateChanged;
        }

        void _spectrumProvider_PlayStateChanged(object sender, EventArgs e)
        {
            if (PropertyChanged != null) PropertyChanged(this, new Pr