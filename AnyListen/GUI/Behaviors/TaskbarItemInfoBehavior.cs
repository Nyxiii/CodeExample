using System;
using System.Windows;
using System.Windows.Shell;

namespace AnyListen.GUI.Behaviors
{
    static class TaskbarItemInfoBehavior
    {
        public static readonly DependencyProperty MaximumProperty = DependencyProperty.RegisterAttached(
            "Maximum", typeof(double), typeof(TaskbarItemInfoBehavior), new PropertyMetadata(default(double), ProgressUpdate));

