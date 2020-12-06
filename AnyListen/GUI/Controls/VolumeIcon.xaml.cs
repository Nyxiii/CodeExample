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

        public static readonly DependencyProperty CurrentVolumeProperty = DependencyProper