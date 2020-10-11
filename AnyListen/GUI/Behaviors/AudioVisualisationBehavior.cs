using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using AnyListen.PluginAPI.AudioVisualisation;
using AnyListen.ViewModels;

namespace AnyListen.GUI.Behaviors
{
    static class AudioVisualisationBehavior
    {
        private static void AudioVisualisationForAdvancedWindowChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            AudioVisualisationChanged(dependencyObject, dependencyPropertyChangedEventArgs, true);
        }

        public static readonly DependencyProperty AudioVisualisationForAdvancedWindowProperty = DependencyProperty.RegisterAttached(
            "AudioVisualisationForAdvancedWindow", typeof(IAudioVisualisationPlugin), typeof(AudioVisualisationBehavior), new PropertyMetadata(default(IAudioVisualisationPlugin), AudioVisualisationForAdvancedWindowChanged));

        public static void SetAudioVisualisationForAdvancedWindow(DependencyObject element, IAudioVisualisationPlugin value)
        {
            element.SetValue(AudioVisualisationForAdvancedWindowProperty, value);
        }

        public static IAudioVisualisationPlugin GetAudioVisualisationForAdvancedWindow(DependencyObject element)
        {
            return (IAudioVisualisationPlugin)element.GetValue(AudioVisualisationForAdvancedWindowProperty);
        }


        public static readonly DependencyProperty AudioVisualisationForSmartWindowProperty = DependencyProperty.RegisterAttached(
            "AudioVisualisationForSmartWindow", typeof(IAudioVisualisationPlugin), typeof(AudioVisualisationBehavior), new PropertyMetadata(default(IAudioVisualisationPlugin), AudioVisualisationForSmartWindowChanged));

        private static void AudioVisualisationForSmartWindowChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            AudioVisualisationChanged(dependencyObject, dependencyPropertyChangedEven