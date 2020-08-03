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
        public List<IThemeSetting> ThemeSettings { get;