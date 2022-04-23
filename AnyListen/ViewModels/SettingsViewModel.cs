using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using AnyListen.Music;
using AnyListen.Music.Data;
using AnyListen.Settings;
using AnyListen.Settings.RegistryManager;
using AnyListen.Settings.Themes;
using AnyListen.Settings.Themes.Background;
using AnyListen.Settings.Themes.Visual;
using AnyListen.ViewModelBase;
using CSCore.SoundOut;
using Microsoft.Win32;

// ReSharper disable ExplicitCallerInfoArgument

namespace AnyListen.ViewModels
{
    class