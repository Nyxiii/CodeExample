using System;
using System.Globalization;
using System.Windows.Data;

namespace AnyListen.Settings.Converter
{
    class SampleRateToIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (int.Parse(value.ToString()))
            {
                case -1:
                    return 0; 
                case 44100:
                    return 1; 
           