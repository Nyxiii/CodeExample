using System;
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
        /// </summary>
        /// <param name="obj">Dependency object</param>
        /// <param name="value">New value for AnimationType</param>
        public static void SetAnimationType(DependencyObject obj, AnimationType value)
        {
            obj.SetValue(AnimationTypeProperty, value);
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for AnimationType.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty AnimationTypeProperty = DependencyProperty.RegisterAttached(
            "AnimationType",
            typeof(AnimationType),
            typeof(VisibilityAnimation),
            new FrameworkPropertyMetadata(AnimationType.None, OnAnimationTypePropertyChanged));

        /// <summary>
        /// AnimationType property changed
        /// </summary>
        /// <param name="dependencyObject">Dependency object</param>
        /// <param name="e">e</param>
        private static void OnAnimationTypePropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement frameworkElement = dependencyObject as FrameworkElement;

            if (frameworkElement == null)
            {
                return;
            }

            // If AnimationType is set to True on this framework element, 
            if (GetAnimationType(frameworkElement) != AnimationType.None)
            {
                // Add this framework element to hooked list
                HookVisibilityChanges(frameworkElement);
            }
            else
            {
                // Otherwise, remove it from the hooked list
                UnHookVisibilityChanges(frameworkElement);
            }
        }

        /// <summary>
        /// Add framework element to list of hooked objects
        /// </summary>
        /// <param name="frameworkElement">Framework element</param>
        private static void HookVisibilityChanges(FrameworkElement frameworkElement)
        {
            _hookedElements.Add(frameworkElement, false);
        }

        /// <su