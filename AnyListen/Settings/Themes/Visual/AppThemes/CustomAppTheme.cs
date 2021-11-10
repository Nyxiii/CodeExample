using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using System.Xml.Serialization;

namespace AnyListen.Settings.Themes.Visual.AppThemes
{
    [Serializable]
    public class CustomAppTheme : AppThemeBase
    {
        public static bool FromFile(string filename, out CustomAppTheme result)
        {
            var appTheme = new CustomAppTheme { Name = Path.GetFileNameWithoutExtension(filename) };

            try
            {
                if (!IsAppThemeDictionary(appTheme.ResourceDictionary))
                {
                    result = null;
                    return false;
                }
            }
            catch (XamlParseException)
            {
                result = null;
                return fals