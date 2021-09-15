
﻿using System;
using System.IO;
using System.Threading;
using AnyListen.Settings.Themes;

namespace AnyListen.Settings
{
    public class AnyListenSettings
    {
        private bool _isSaving;
        private static PathProvider _paths;
        private static AnyListenSettings _instance;

        public class PathProvider
        {
            public readonly string BaseDirectory;
            public readonly string CoverDirectory;

            public readonly string AccentColorsDirectory;
            public readonly string AppThemesDirectory;
            public readonly string ThemePacksDirectory;
            public readonly string AudioVisualisationsDirectory;
            // ReSharper disable once UnassignedReadonlyField