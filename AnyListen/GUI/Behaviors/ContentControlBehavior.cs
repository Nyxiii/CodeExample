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
            var values = GetFormatValues(contentControl);

            int counter = 0;
            foreach (var item in Regex.Split(dependencyPropertyChangedEventArgs.NewValue.ToString(), @"\{.\}"))
            {
                textBlock.Inlines.Add(new Run(item));

                if (values.Length - 1 < counter) continue;
                string[] sSplit = values[counter].Split('$');
                string text = sSplit[0];
                string url = sSplit[1];

                var hyperlink = new Hyperlink(new Run(text)) { NavigateUri = new Uri(url) };
                hyperlink.RequestNavigate += (s, e) => { Process.Start(e.Uri.AbsoluteUri); };
                textBlock.Inlines.Add(hyperlink);
                counter++;
            }
   