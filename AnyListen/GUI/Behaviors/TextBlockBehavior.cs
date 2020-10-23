using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace AnyListen.GUI.Behaviors
{
    static class TextBlockBehavior
    {
        public static string GetUpperText(DependencyObject obj) { return (string)obj.GetValue(UpperTextProperty); }
        public static void SetUpperText(DependencyObject obj, string value) { obj.SetValue(UpperTextProperty, value); }

        public static readonly DependencyProperty UpperTextProperty = DependencyProperty.RegisterAttached("UpperText", typeof(string), typeof(TextBlockBehavior), new UIPropertyMetadata(string.Empty, OnUpperTextChanged));

        private static void OnUpperTextChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var element = obj as TextBlock;
            if (element == null) throw new ArgumentException();
            element.Text = e.NewValue.ToString().ToUpper();
        }

        public static readonly DependencyProperty PlaceHolderTextProperty = DependencyProperty.RegisterAttached(
            "PlaceHolderText", typeof(string), typeof(TextBlockBehavior), new PropertyMetadata(default(string)));

        public static void SetPlaceHolderText(DependencyObject element, string value)
        {
            element.SetValue(PlaceHolderTextProperty, value);
            var txt = element as TextBlock;
            if (txt == null) throw new ArgumentException();
            DependencyPropertyDescriptor.FromProperty(TextBlock.TextProperty, typeof(TextBlock)).RemoveValueChanged(txt, TextChangedHandler);
            DependencyPropertyDescriptor.FromPr