using System.Windows.Media;
using AnyListen.Utilities;

namespace AnyListen.MagicArrow
{
    /// <summary>
    /// Interaction logic for StrokeWindow.xaml
    /// </summary>
    public partial class StrokeWindow
    {

        private double _currentleft;

        public StrokeWindow(double height, double left, double top, Side side)
        {
            InitializeComponent();
            