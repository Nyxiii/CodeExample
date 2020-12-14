using System;
using System.Globalization;
using System.Windows.Data;

namespace AnyListen.GUI.Converter
{
    class TrueFalseValueConverter : IValueConverter
    {
        public object TrueValue { get; set; }
        public object FalseValue { get; set; }

        public object Convert(object value, Type targetType, obje