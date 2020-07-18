using System;
using System.Windows.Media;

namespace AnyListen.Designer.Data
{
    public class ThemeColor : IThemeSetting
    {
        private Color _color;
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                if (ValueChanged != null) ValueChanged(this, Ev