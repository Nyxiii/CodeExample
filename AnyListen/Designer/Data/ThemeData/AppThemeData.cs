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
                    RegexPattern = "x:Key=\"BlackColor\">(?<content>(.*?))<",
                    ID = "BlackColor",
                    DisplayName ="Black color"
                },
                new ThemeColor
                {
                    RegexPattern = "x:Key=\"WhiteColor\">(?<content>(.*?))<",
                    ID = "WhiteColor",
                    DisplayName ="White color"
                },
                new ThemeColor
                {
                    RegexPattern = "x:Key=\"Gray1\">(?<content>(.*?))<",
                    ID = "Gray1",
                    DisplayName ="Gray #1"
                },
                new ThemeColor
                {
                    RegexPattern = "x:Key=\"Gray2\">(?<cont