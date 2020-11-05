﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Animation;

namespace AnyListen.GUI.Behaviors
{
    /// <summary>
    /// Supplies attached properties that provides visibility of animations
    /// </summary>
    public class VisibilityAnimation : DependencyObject
    {
        public enum AnimationType
        {
            /// <summary>
            /// No animation
            /// </summary>
            None,

            /// <summary>
            /// Fade in / Fade out
            /// </summary>
            Fade
        }

        /// <summary>
        /// Animation duration
        /// </summary>
        private const int AnimationDuration = 300;

        /// <summary>
        /// List of hooked objects
        /// </summary>
        private static readonly Dictionary<FrameworkElement, bool> _hookedElements = new Dictionary<FrameworkElement, bool>();

        /// <summary>
        /// Get AnimationType attached property
        /// </summary>
        /// <param name="obj">Dependency object</param>
        /// <returns>AnimationType value</returns>
        public static AnimationType GetAnimationType(DependencyObject obj)
        {
            return (AnimationType)obj.GetValue(AnimationTypeProperty);
        }

        /// <summary>
        /// Set AnimationType attached property