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
            var co