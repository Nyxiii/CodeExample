using System;
using System.Globalization;
using System.Windows.Data;

namespace AnyListen.GUI.Converter
{
    class StringArrayToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString())) return string.Empty;

            return string.Join(", ", (string[])value);
        }

        public obje