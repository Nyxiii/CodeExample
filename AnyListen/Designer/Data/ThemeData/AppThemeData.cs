using System.Collections.Generic;
using System.Linq;
using System.Windows;
using AnyListen.Settings;
using AnyListen.Settings.Themes;

namespace AnyListen.Designer.Data.ThemeData
{
    public class AppThemeData : DataThemeBase
    {
        public AppThemeData()
        {
            ThemeSettings = new List<IThemeSetting>
            {
                new ThemeColor
                {
                