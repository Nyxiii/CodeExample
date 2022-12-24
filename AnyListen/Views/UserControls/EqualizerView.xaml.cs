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
        public bool ShowLabelBelowSlider { get; set; }

        private bool _showSeparator;
        public bool ShowSeparator
        {
            get { return _showSeparator; }
            set
            {
                _showSeparator = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ShowSeparator"));
            }
        }
        

        private int _itemSpace;
        public int ItemSpace
        {
            get
            {
                return _itemSpace;
            }
            set
            {
                _itemSpace = value;
                SliderThickness = new Thickness(value, 0, value, 0);
                if (PropertyChanged != null) PropertyChanged(t