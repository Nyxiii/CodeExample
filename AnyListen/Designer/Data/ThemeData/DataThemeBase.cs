using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Markup;

namespace AnyListen.Designer.Data.ThemeData
{
    public abstract class DataThemeBase : ISaveable
    {
        public List<IThemeSetting> ThemeSettings { get; set; }
        public string Name { get; set; }
        public abstract string Source { get; }
        public abstract string Filter { get; }
        public abstract string BaseDirectory { get; }

        public void LoadFromFile(string filePath)
        {
            LoadFromResourceDictionary(new ResourceDictionary {Source = new Uri(filePath)});
        }

        public void LoadFromResourceDictionary(ResourceDictionary dictionary)
        {
            foreach (var setting in ThemeSettings)
            {
                setting.SetValue(GetValueFromDictionary(setting.ID, dictionary).ToString());
            }
        }

        private object GetValueFromDictionary(string key, Resourc