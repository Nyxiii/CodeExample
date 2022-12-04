
﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Navigation;

namespace AnyListen.Views.UserControls
{
    /// <summary>
    /// Interaction logic for AboutView.xaml
    /// </summary>
    public partial class AboutView
    {
        public AboutView()
        {
            Components = new List<Component>
            {
                new Component
                {
                    Name = "CSCore – .NET Sound Library",
                    Url = "https://cscore.codeplex.com/",
                    LicenceUrl = "https://cscore.codeplex.com/license",
                    Description = "CSCore is a free .NET audio library which is completely written in C#."
                },
                new Component
                {
                    Name = "Extended WPF Toolkit™",
                    Url = "https://wpftoolkit.codeplex.com/",
                    LicenceUrl = "https://wpftoolkit.codeplex.com/license",
                    Description =
                        "Extended WPF Toolkit™ is the number one collection of WPF controls, components and utilities for creating next generation Windows applications."
                },
                new Component
                {
                    Name = "GongSolutions.WPF.DragDrop",
                    Url = "https://punker76.github.io/gong-wpf-dragdrop/",
                    LicenceUrl = "https://github.com/punker76/gong-wpf-dragdrop/blob/master/LICENSE",
                    Description =
                        "The GongSolutions.WPF.DragDrop library is a drag'n'drop framework for WPF"
                },