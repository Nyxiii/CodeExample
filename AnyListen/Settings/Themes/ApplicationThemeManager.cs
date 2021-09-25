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

        #endreg