using System.Windows.Media;

namespace AnyListen.PluginAPI.AudioVisualisation
{
    /// <summary>
    /// Some color information from the applied color theme
    /// </summary>
    public class ColorInformation
    {
        private Brush _accentBrush;
        private Brush _whiteBrush;
        private Brush _blackBrush;
        private Brush _grayBrush;

        /// <summary>
        /// The main color
        /// </summary>
        public Color AccentColor { get; set; }

        /// <summary>
        /// The white (= foreground) color
        /// </summary>
        public Color WhiteColor { get; set; }

       