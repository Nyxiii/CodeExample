using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using AnyListen.Music;
using AnyListen.Music.Data;
using AnyListen.Settings;
using AnyListen.Settings.RegistryManager;
using AnyListen.Settings.Themes;
using AnyListen.Settings.Themes.Background;
using AnyListen.Settings.Themes.Visual;
using AnyListen.ViewModelBase;
using CSCore.SoundOut;
using Microsoft.Win32;

// ReSharper disable ExplicitCallerInfoArgument

namespace AnyListen.ViewModels
{
    class SettingsViewModel : PropertyChangedBase
    {
        #region "Singleton & Constructor"
        private static SettingsViewModel _instance;
        public static SettingsViewModel Instance => _instance ?? (_instance = new SettingsViewModel());

        private SettingsViewModel()
        {
            RegistryManager = new RegistryManager(); //important for shortcut
        }

        public void Load()
        {
            SoundOutList = MusicManager.CSCoreEngine.SoundOutManager.SoundOutList;
            SelectedSoundOut = SoundOutList.First(x => x.SoundOutMode == Config.SoundOutMode);
            CurrentLanguage = Config.Languages.First(x => x.Code == Config.Language);
        }

        public MusicManager MusicManager => MainViewModel.Instance.MusicManager;
        public ApplicationThemeManager ApplicationThemeManager => ApplicationThemeManager.Instance;
        public RegistryManager RegistryManager { get; set; }

        public ConfigSettings Config => AnyListenSettings.Instance.Config;

        public MainWindow BaseWindow => (MainWindow)Application.Current.MainWindow;

        #endregion

        private int _selectedtab;
        public int SelectedTab
        {
            get { return _selectedtab; }
            set
            {
                SetProperty(value, ref _selectedtab);
                switch (value)
                {
                    case 2:
                        OnPropertyChanged("MusicManager");
                        break;
                }
            }
        }

        #region Playback

        private ObservableCollection<SoundOutRepresenter> _soundOutList;
        public ObservableCollection<SoundOutRepresenter> SoundOutList
        {
            get { return _soundOutList; }
            set
            {
                SetProperty(value, ref _soundOutList);
            }
        }

        
        private int _selectedAudioDeviceIndex;
        public int SelectedAudioDeviceIndex
        {
            get { return _selectedAudioDeviceIndex; }
            set
            {
                SetProperty(value, ref _selectedAudioDeviceIndex);
            }
        }

        private AudioDevice _selectedaudiodevice;
        public AudioDevice SelectedAudioDevice
        {
            get { return _selectedaudiodevice; }
            set
            {
                if (SetProperty(value, ref _selectedaudiodevice)) OnPropertyChanged("CanApplySoundOut");
                if (value == null && SelectedSoundOut.AudioDevices.Count > 0)
                    SelectedAudioDevice = SelectedSoundOut.AudioDevices.First();
            }
        }

        private SoundOutRepresenter _selectedSoundOut;
        public SoundOutRepresenter SelectedSoundOut
        {
            get { return _selectedSoundOut; }
            set
            {
                if (SetProperty(value, ref _selectedSoundOut) && value != null)
                {
                    OnPropertyChanged("CanApplySoundOut");

                    var device = value.AudioDevices.FirstOrDefault(x => x.ID == Config.SoundOutDeviceID) ??
                                 value.AudioDevices.FirstOrDefault(x => x.IsDefault);
                    SelectedAudioDeviceIndex = device == null ? 0 : value.AudioDevices.IndexOf(device);
                }
            }
        }

        private RelayCommand _applySoundOut;
        public RelayCommand ApplySoundOut
        {
            get
            {
                return _applySoundOut ?? (_applySoundOut = new RelayCommand(parameter =>
                {
                    if (!CanApplySoundOut) return;
                    Config.SoundOutMode = SelectedSoundOut.SoundOutMode;
                    Config.SoundOutDeviceID = SelectedAudioDevice.ID;
                    MusicManager.CSCoreEngine.Refresh();
                    OnPropertyChanged("CanApplySoundOut");
                }));
            }
        }

        public bool CanApplySoundOut
        {
            get
            {
                if (SelectedAudioDevice == null || SelectedSoundOut == null) return false;
                return Config.SoundOutDeviceID != SelectedAudioDevice.ID || Config.SoundOutMode != SelectedSoundOut.SoundOutMode;
            }
        }

        #endregion

        #region Apperance

        private RelayCommand _openDesigner;
        public RelayCommand OpenDesigner
        {
          