using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MahApps.Metro.Controls;

namespace AnyListen.GUI.Behaviors
{
    class TransitioningContentControlBehavior
    {
        public static readonly DependencyProperty DisplayTextProperty = DependencyProperty.RegisterAttached(
            "DisplayText", typeof (object), typeof (TransitioningContentControlBehavior),
            new PropertyMetadata(default(object), DisplayTextPropertyChangedCallback));

        private static void DisplayTextPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var control = dependencyObject as TransitioningContentControl;
            if (control == null) throw new ArgumentException();
            control.Content = dependencyPropertyChangedEventArgs.NewValue == null
                ? null
                : new TextBlock
                {
                    Text = dependencyPropertyChangedEventArgs.NewValue.ToString(),
                    SnapsToDevicePixels = true,
                    TextTrimming = TextTrimming.CharacterEllipsis,
                    Margin = new Thickness(1),
                    VerticalAlignment = VerticalAlignment.Center,
                    Foreground = (Brush)Application.Current.Resources["BlackBrush"]
                };