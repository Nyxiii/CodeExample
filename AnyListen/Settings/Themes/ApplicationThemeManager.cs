using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using AnyListen.Designer.Data;
using AnyListen.Settings.Themes.AudioVisualisation;
using AnyListen.Settings.Themes.Visual.AccentColors;
using AnyListen.Settings.Themes.Visual.AppThemes;
using MahApps.Metro;
using AppTheme = AnyListen.Settings.Themes.Visual.AppThemes.AppTheme;

namespace AnyListen.Settings.Themes
{
    public class ApplicationThemeManager
    {
        #region "Singleton & Constructor"

        private static ApplicationThemeManager _instance;
        public static ApplicationThemeManager Instance => _instance ?? (_instance = new ApplicationThemeManager());


        private ApplicationThemeManager()
        {
            _loadedResources = new Dictionary<string, ResourceDictionary>();
        }

        #endregion

        private ObservableCollection<AccentColorBase> _accentColors;
        public ObservableCollection<AccentColorBase> AccentColors => _accentColors;

        private ObservableCollection<AppThemeBase> _appThemes;
        public ObservableCollection<AppThemeBase> AppThemes => _appThemes;

        private ObservableCollection<ThemePack> _themePacks;
        public ObservableCollection<ThemePack> ThemePacks => _themePacks;

        private ObservableCollection<IAudioVisualisationContainer> _audioVisualisations;
        public ObservableCollection<IAudioVisualisationContainer> AudioVisualisations => _audioVisualisations;

        public void Refresh()
        {
            _accentColors = new ObservableCollection<AccentColorBase>();
            _appThemes = new ObservableCollection<AppThemeBase>();
            _themePacks = new ObservableCollection<ThemePack>();
            _audioVisualisations = new ObservableCollection<IAudioVisualisationContainer>();

            foreach (var t in ThemeManager.Accents.Select(a => new AccentColor { Name = a.Name }).OrderBy(x => x.TranslatedName))
            {
                _accentColors.Add(t);
            }

            foreach (var t in ThemeManager.AppThemes.Select(a => new AppTheme { Name = a.Name }).OrderBy(x => x.TranslatedName))
            {
                _appThemes.Add(t);
            }

            foreach (var defaultAudioVisuali