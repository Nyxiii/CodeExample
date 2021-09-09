using System;
using System.Globalization;
using System.Windows.Data;

namespace AnyListen.Settings.Converter
{
    class BitrateToIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return BitrateToIndex(int.Parse(value.ToString()));
        }

        public static int BitrateToIndex(int waveSourceBits)
        {
            switch (waveSourceBits)
            {
                case 8:
                    return 0;
                case 16:
                    return 1;
                case 24:
                    return 2;
                case 32:
         