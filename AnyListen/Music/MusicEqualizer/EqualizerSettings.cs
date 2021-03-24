using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AnyListen.Music.CustomEventArgs;
using AnyListen.ViewModelBase;

namespace AnyListen.Music.MusicEqualizer
{
    [Serializable]
    public class EqualizerSettings : PropertyChangedBase
    {
        public event EventHandler<EqualizerChangedEventArgs> EqualizerChanged;

        protected List<string> Bandlabels = new List<string>(new [] { "31", "62", "125", "250", "500", "1K", "2K", "4K", "8K", "16K" });

        public void CreateNew()
        {
            if (Bands != null) { Bands.Clear(); } else { Bands = new ObservableCollection<EqualizerBand>(); }

            for (int i = 0; i < 10; i++)
            {
                Bands.Add(new EqualizerBand(Bandlabels[i]));
            }
            Loaded();
        }

        public void Loaded()
        {
            foreach (EqualizerBand b in Bands)
            {
                b.EqualizerChanged += (s, e) =>
                {
                    if (EqualizerChanged != null)
                        EqualizerChanged(this, new EqualizerChangedEventArgs(Bands.IndexOf(b), b.Value));
                };
                b.Label = Bandlabels[Bands.IndexOf(b)];
            }
        }

        private RelayCommand _resetequalizer;
        public RelayCommand ResetEqualizer
        {
            get
            {
                return _re