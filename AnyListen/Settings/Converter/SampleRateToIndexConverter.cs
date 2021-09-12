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
                case 48000:
                    return 2; 
                case 88200:
                    return 3; 
                case 96000:
                    return 4; 
                case 176400:
                    return 5; 
                case 192000:
                    return 6; 
            }
            throw new ArgumentException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (int.Parse(value.ToString()))
            {
                case 0:
                    return -1; 
                case 1:
                    return 44100; 
                case 2