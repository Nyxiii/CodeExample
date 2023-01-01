using System;

namespace AnyListen.PluginAPI.AudioVisualisation
{
    /// <summary>
    /// Provides access to audio engine functionality needed to render the audio visualisation
    /// </summary>
    public interface ISpectrumProvider
    {
        /// <summary>
        /// Assigns current FFT data to a buffer.
        /// </summary>
        /// <param name="fftDataBuffer">The buffer to copy FFT d