using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using AnyListen.MagicArrow.DockManager;
using AnyListen.Settings;
using AnyListen.Utilities;
using Cursor = System.Windows.Forms.Cursor;

namespace AnyListen.MagicArrow
{
    /// <summary>
    /// A class for the arrow that is displayed when the cursor hits the right Windows border.
    /// </summary>
    public class MagicArrow : IDisposable
    {
        private readonly WindowHider _windowHider;
        private ActiveWindowHook _activewindowhook;
        private Side _movedOutSide;
        private StrokeWindow _strokeWindow;
        private bool _magicArrowIsShown;
        private bool _isInZone;
        private bool _mouseWasOver;

        public MagicArrow()
        {
            _windowHider = new WindowHider();
        }

        public void Dispose()
        {
            StopMagic();
            DockManager.Dispose();
            _activewindowhook.Dispose();
            Unregister();
        }

        public event EventHandler MoveOut; //When moving out
        public event EventHandler MoveIn;
        public event DragEventHandler FilesDropped;

        public Window BaseWindow { get; set; }
        public bool MovedOut { get; set; }
        public MagicArrowWindow MagicWindow { get; set; }
        public DockManager.DockManager DockManager { get; set; }

        public void Register(Window window)
        {
            if (BaseWindow != null) throw new InvalidOperationException("Only one window can be registered");
            BaseWindow = window;
            Application.Current.Deactivated += Application_Deactivated;
            DockManager = new DockManager.DockManager(BaseWindow);
            _activewindowhook = new ActiveWindowHook();
            _activewindowhook.ActiveWindowChanged += activewindowhook_ActiveWindowChanged;
        }

        public void Unregister()
        {
            Application.Current.Deactivated -= Application_Deactivated;
            BaseWindow = null;
        }

        public void BringToFront()
        {
            if (MovedOut) { MoveWindowBackInScreen(); }

            Window mai