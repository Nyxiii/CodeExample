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
                    RegexPattern = "x:Key=\"Gray2\">(?<content>(.*?))<",
                    ID = "Gray2",
                    DisplayName ="Gray #2"
                },
                new ThemeColor
                {
                    RegexPattern = "x:Key=\"Gray7\">(?<content>(.*?))<",
                    ID = "Gray7",
                    DisplayName ="Gray #7"
                },
                new ThemeColor
                {
                    RegexPattern = "x:Key=\"Gray8\">(?<content>(.*?))<",
                    ID = "Gray8",
                    DisplayName ="Gray #8"
                },
                new ThemeColor
                {
                    RegexPattern = "x:Key=\"Gray10\" Color=\"(?<content>(.*?))\"",
                    ID = "Gray10",
                    DisplayName ="Gray #10"
                },
                new ThemeColor
                {
                    RegexPattern = "x:Key=\"GrayNormal\">(?<content>(.*?))<",
                    ID = "GrayNormal",
                    DisplayName ="Gray normal"
                },
                new ThemeColor
                {
                    RegexPattern = "x:Key=\"GrayHover\">(?<content>(.*?))<",
                    ID = "GrayHover",
                    DisplayName ="Gray hover"
                },
                new ThemeColor
                {
                    RegexPattern = "x:Key=\"FlyoutColor\">(?<content>(.*?))<",
                    ID = "FlyoutColor",
                    DisplayNam