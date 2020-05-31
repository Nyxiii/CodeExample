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
                        designer.ShowDialog();
                        return;
                    case "/registry":
                        var manager = new RegistryManager();
                        var item = manager.ContextMenuItems.First(x => x.Extension == Environment.GetCommandLineArgs()[2]);
                        try
                        {
                            if (item != null) item.ToggleRegister(!item.IsRegistered, false);
                        }
                        catch (SecurityException)
                        {
                            MessageBox.Show("Something went extremly wrong. This application didn't got administrator rights so it can't register anything.");
                        }

                        Current.Shutdown();
                        return;
                    case "/screeninfo":
                        var message = new StringBuilder();
                        var screens = WpfScreen.AllScreens().ToList();
                        message.AppendLine("AnyListen - Detected Screens");
                        message.AppendLine("-----------------------------------------------------------------------------------");
                        message.AppendFormat("Found screens: {0}", screens.Count);
                        message.AppendLine();
                        foreach (var wpfScreen in screens)
                        {
                            message.AppendFormat("Screen #{0} ({1})", screens.IndexOf(wpfScreen), wpfScreen.DeviceName);
                            message.AppendFormat("Size: Width {0}, Height {1}", wpfScreen.WorkingArea.Width, wpfScreen.WorkingArea.Height);
                            message.AppendFormat("Position: X {0}, Y {1}", wpfScreen.WorkingArea.X, wpfScreen.WorkingArea.Y);
                            message.AppendFormat("IsPrimary: {0}", wpfScreen.IsPrimary);
                            message.AppendLine();
                        }
                        message.AppendLine("-----------------------------------------------------------------------------------");
                        message.AppendLine();
                        message.AppendFormat("Most left x: {0}", WpfScre