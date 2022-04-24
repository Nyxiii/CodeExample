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

        public MusicManager MusicManager => Mai