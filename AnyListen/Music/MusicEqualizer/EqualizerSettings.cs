﻿using System;
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
                return _resetequalizer ?? (_resetequalizer = new RelayCommand(parameter =>
                {
                    foreach (EqualizerBand band in Bands)
                    {
                        band.Value = 0;
                    }
                }));
            }
        }

        private ObservableCollection<EqualizerBand> _bands;
        public ObservableCollection<EqualizerBand> Bands
        {
            get { return _bands; }
            set
            {
                SetProperty(value, ref _bands);
            }
        }

        private RelayCommand _loadpresetbass;
        public RelayCommand LoadPresetBass
        {
            get { return _loadpresetbass ?? (_loadpresetbass = new RelayCommand(parameter => { LoadPreset(50, 35, 20, 5, -10, -10, 0, -2, 0, 2); })); }
        }

        private RelayCommand _loadpresetbassexteme;
        public RelayCommand LoadPresetBassExteme
        {
            get { return _loadpresetbassexteme ?? (_loadpresetbassexteme = new RelayCommand(parameter => { LoadPreset(90, 85, 65, 30, 0, -5, -5, 0, 2, 0); })); }
        }

        private RelayCommand _loadpresetbassandheights;
        public RelayCommand LoadPresetBassAndHeights
        {
            get { return _loadpresetbassandheights ?? (_loadpresetbassandheights = new RelayCommand(parameter => { LoadPreset(20, 20, 10, 0, -10, -10, 0, 5, 20, 20); })); }
        }

        private RelayCommand _loadpresetheights;
        public RelayCommand LoadPresetHeights
        {
            get { return _loadpresetheights ?? (_loadpresetheights = new RelayCommand(parameter => { LoadPreset(-10, -10, -10, -10, -5, -5, 0, 25, 50, 75); })); }
        }

        private RelayCommand _loadpresetclassic;
        public RelayCommand LoadPresetClassic
        {
            get { return _loadpresetclassic ?? (_loadpresetclassic = new RelayCommand(parameter => { LoadPreset(0, 0, 0, 0, 0, 0, -10, -10, -10, -20); })); }
        }

        private RelayCommand _loadpresetdance;
        public RelayCommand LoadPresetDance
        {
            get { return _loadpresetdance ?? (_loadpresetdance = new RelayCommand(parameter => { LoadPreset(30, 15, 10, 0, 0, -10, -5, -5, 0, 0); })); }
        }

        private RelayCommand _loadpresetrock;
        public RelayCommand LoadPresetRock
        {
            get { return _loadpresetrock ?? (_loadpresetrock = new RelayCommand(parameter => { LoadPreset(25, 10, 5, -10, -5, 5, 10, 20, 20, 20); })); }
        }

        private RelayCommand _loadpresettechno;
        public RelayCommand LoadPresetTechno
        {
            get { return _loadpresettechno ?? (_loadpresettechno = new RelayCommand(parameter => { LoadPreset(20, 20, 0, -10, -5, 0, 10, 40, 40, 40); })); }
        }

        private RelayCommand _loadpresetpop;
        public RelayCommand LoadPresetPop
        {
            get { return _loadpresetpop ?? (_loadpresetpop = new RelayCommand(parameter => 