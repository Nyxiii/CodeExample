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
                return false;
            }

            result = appTheme;
            return true;
        }

        [XmlIgnore]
        public override ResourceDictionary ResourceDictionary => new ResourceDictionary { Source = new Uri(Path.Combine(AnyListenSettings.Paths.AppThemesDirectory, Name + ".xaml"), UriKind.RelativeOrAbsolute) };

        public override string TranslatedName => Name;

        public override string Group => Application.Current.Resources["Custom"].ToString();

        public static bool IsAppThemeDictionary(ResourceDictionary resources)
        {
            if (resources == null) throw new ArgumentNullException("resources");

            // Note: add more checks if these ke