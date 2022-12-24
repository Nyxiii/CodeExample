using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace AnyListen.Views.UserControls
{
    /// <summary>
    /// Interaction logic for EqualizerView.xaml
    /// </summary>
    public partial class EqualizerView : INotifyPropertyChanged
    {
        public event EventHandler WantClose;
        public Thickness SliderThickness { get; set; }
       