using System;
using System.Windows;
using System.Xml.Serialization;
using AnyListen.Settings.Converter;

namespace AnyListen.Settings.Themes.Visual.AppThemes
{
    [Serializable, XmlInclude(typeof(AppTheme)), XmlInclude(typeof(CustomAppTheme))]
    public abstract class AppThemeBase : IAppTheme, IGroupable
    {
        public string Name { set; get; }
        public abstract string TranslatedName { get