
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
                new Component
                {
                    Name = "Hardcodet.Wpf.TaskbarNotification",
                    Url = "http://www.hardcodet.net/wpf-notifyicon",
                    LicenceUrl = "http://www.codeproject.com/info/cpol10.aspx",
                    Description =
                        "It's an implementation of a NotifyIcon (aka system tray icon or taskbar icon) for the WPF platform."
                },
                new Component
                {
                    Name = "Json.NET",
                    Url = "http://james.newtonking.com/json",
                    LicenceUrl = "https://github.com/JamesNK/Newtonsoft.Json/blob/master/LICENSE.md",
                    Description =
                        "Popular high-performance JSON framework for .NET"
                },
                new Component
                {
                    Name = "MahApps.Metro",
                    Url = "http://mahapps.com/",
                    LicenceUrl = "https://github.com/MahApps/MahApps.Metro/blob/master/LICENSE",
                    Description = "A toolkit for creating metro-style WPF applications."
                },
                new Component
                {
                    Name = "SharpZipLib",
                    Url = "https://icsharpcode.github.io/SharpZipLib/",
                    LicenceUrl = "https://icsharpcode.github.io/SharpZipLib/",
                    Description =
                        "#ziplib is a Zip, GZip, Tar and BZip2 library written entirely in C# for the .NET platform."
                },
                new Component
                {
                    Name = "TagLib#",
                    Url = "https://github.com/mono/taglib-sharp",
                    LicenceUrl = "http://opensource.org/licenses/lgpl-2.1.php",
                    Description =
                        "TagLib# (aka taglib-sharp) is a library for reading and writing metadata in media files, including video, audio, and photo formats."
                },
                new Component
                {
                    Name = "WPF Native Folder Browser",
                    Url = "https://wpffolderbrowser.codeplex.com/",
                    LicenceUrl = "https://wpffolderbrowser.codeplex.com/license",
                    Description =
                        "Use the Windows Vista / Windows 7 Folder Browser Dialog from your WPF projects, without any additional dependencies."
                },
                new Component
                {
                    Name = "WPF Sound Visualization Library",
                    Url = "http://wpfsvl.codeplex.com/",
                    LicenceUrl = "https://wpfsvl.codeplex.com/license",
                    Description =
                        "The WPF Sound Visualization Library is a collection of WPF Controls for graphically displaying data related to sound processing."
                }
            };

            ImageCreators = new List<ImageCreator>
            {
                new ImageCreator
                {
                    Name = "Visual Pharm",
                    Licence = "http://opensource.org/licenses/MIT",
                    Website = "http://ionicons.com/"
                },
                new ImageCreator
                {
                    Name = "Icons4Android",
                    Licence = "https://creativecommons.org/licenses/by/3.0/",
                    Website = "http://www.icons4android.com/"
                },
                new ImageCreator
                {
                    Name = "Linh Pham Thi Dieu",
                    Licence = "https://www.iconfinder.com/licenses/basic",
                    Website = "http://linhpham.me/#works"
                },
                new ImageCreator
                {
                    Name = "Ionicons",
                    Licence = "http://opensource.org/licenses/MIT",
                    Website = "http://ionicons.com/"
                },
                new ImageCreator
                {
                    Name = "Icons8",
                    Licence = "http://icons8.com/license/",
                    Website = "http://www.icons8.com/"
                },
                new ImageCreator
                {
                    Name = "freepik",
                    Licence = "https://creativecommons.org/licenses/by/3.0/",
                    Website = "http://www.flaticon.com/authors/freepik"
                },
                new ImageCreator
                {
                    Name = "Elegant Themes",
                    Licence = "https://creativecommons.org/licenses/by/3.0/",
                    Website = "http://www.elegantthemes.com"
                }
            };

            var version = Assembly.GetExecutingAssembly().GetName().Version;
            CurrentVersion = $"{version.Major}.{version.Minor}.{version.Build} (Build {version.Revision})";
            InitializeComponent();
        }

        public List<Component> Components { get; set; }
        public List<ImageCreator> ImageCreators { get; set; }
        public string CurrentVersion { get; set; }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(e.Uri.AbsoluteUri);
            e.Handled = true;
        }

        private void ButtonGitHub_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/AnyListen/AMP");
        }

        private void ButtonVBP_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.vb-paradise.de/index.php/Thread/108601/");
        }
    }

    public class Component
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string LicenceUrl { get; set; }
    }

    public class ImageCreator
    {
        public string Name { get; set; }
        public string Website { get; set; }
        public string Licence { get; set; }
    }
}