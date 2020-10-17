using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace AnyListen.GUI.Behaviors
{
    static class FrameworkElementBehavior
    {
        public static readonly DependencyProperty AnimationTriggerProperty = DependencyProperty.RegisterAttached(
            "AnimationTrigger", typeof (object), typeof (FrameworkElementBehavior),
            new PropertyMetadata(default(object), PropertyChangedCallba