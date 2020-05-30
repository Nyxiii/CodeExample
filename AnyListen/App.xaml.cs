using System;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using AnyListen.Notification.WindowMessages;
using AnyListen.Settings;
using AnyListen.Settings.RegistryManager;
using AnyListen.Settings.Themes;
using AnyListen.Utilities;
using AnyListen.Utilities.Native;
using AnyListen.ViewModels;
using AnyListen.Views;
using AnyListen.Views.Test;
using AnyListen.Views.Tools;

namespace AnyListen
{
    /// <summary>
    /// Interaction logic for "App.xaml"
    /// </summary>
    public partial class App
    {
        Mutex _myMutex;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //if (e.Args.Length > 0 && e.Args[0] == "/update")
            //    UpdateService.UpdateSettings(AnyListenSettings.Paths.BaseDirectory);
            AnyListenSettings.Instance.Load();

            var openfile = false;
            if (e.Args.Length > 0)
            {
                switch (e.Args[0])
                {
                    case "/test":
                        var view = new TestWindow();
                        view.Show();
                        break;
                    case "/language_creator":
                        var languageCreator = new LanguageCreatorWindow();
                        languageCreator.ShowDialog();
                        return;
                    case "/designer":
                        var resource = new ResourceDictionary { Source = new Uri("/Resources/Themes/Cyan.xaml", UriKind.Relative) };
                        ApplicationThemeManager.Instance.LoadResource("accentcolor", resource);
                        var designer = new Designer.DesignerWindow();
         