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

        public static