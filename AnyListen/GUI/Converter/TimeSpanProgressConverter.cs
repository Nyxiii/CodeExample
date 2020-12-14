using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AnyListen.GUI.Converter
{
    class TimeSpanProgressConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == DependencyProperty.UnsetValue || values[1] == DependencyProperty.UnsetValue