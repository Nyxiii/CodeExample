using System.Windows.Media;

namespace AnyListen.PluginAPI.AudioVisualisation
{
    /// <summary>
    /// The audio visualisation plugin interface
    /// </summary>
    public interface IAudioVisualisationPlugin
    {
        /// <summary>
        /// This function should return the audio visualisation for the advanced window
        /// </summary>
        /// <returns>The audio visualisation</returns>
        IAudioVisualisation AdvancedWindowVisualisation { get; }

        /// <summary>
        /// This property should return the audio visualisation for the smart window
        /// </summary>
        /// <returns>The audio visualisa