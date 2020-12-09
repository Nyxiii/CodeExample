using System.ComponentModel;
using System.Windows;

namespace AnyListen.GUI.Controls
{
    /// <summary>
    /// Interaction logic for VolumeIcon.xaml
    /// </summary>
    public partial class VolumeIcon : INotifyPropertyChanged
    {
        public VolumeIcon()
        {
            InitializeComponent();
            RefreshCurrentState();
        }

        public static readonly DependencyProperty CurrentVolumeProperty = DependencyProperty.Register(
            "CurrentVolume", typeof (float), typeof (VolumeIcon), new PropertyMetadata(1.0f, PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((VolumeIcon) dependencyObject).RefreshCurrentState();
        }

        void RefreshCurrentState()
        {
            if (CurrentVolume == 0)
            {
                CurrentDisplayState = DisplayState.Mute;
            }
            else if (CurrentVolume <= 0.5)
            {
                CurrentDisplayState = DisplayState.Medium;
            }
            else if (CurrentVolume > 0.5)
          