using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Animation;
using AnyListen.Utilities;
using AnyListen.Utilities.Native;

namespace AnyListen.Views
{
    /// <summary>
    /// Interaction logic for EqualizerWindow.xaml
    /// </summary>
    public partial class EqualizerWindow
    {
        public event EventHandler BeginCloseAnimation;

        public EqualizerWindow(RECT rect, double width)
        {
            InitializeComponent();
            SetPosition(rect, width);
            Closing += EqualizerWindow_Closing;
        }

        bool _isClosing;
        bool _canClose;
        void EqualizerWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!_canClose)
            {
                e.Cancel = true;
                if (!_isClosing)
                {
                    _isClosing = true;
                    Storyboard story = new Storyboard();
     