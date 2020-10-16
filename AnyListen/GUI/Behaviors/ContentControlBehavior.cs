using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace AnyListen.GUI.Behaviors
{
    static class ContentControlBehavior
    {
        public static readonly DependencyProperty FormattedContentProperty = DependencyProperty.RegisterAttached(
            "FormattedContent", typeof (string), typeof (ContentControlBehavior),
            new PropertyMetadata(default(string), PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(dependencyObject)) return;
            var contentControl = dependencyObject as ContentControl;
            if (contentControl == null) return;

            var textBlock = new TextBlock();
            var values = GetFormatVal