﻿using System;
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

            Window mainwindow = Application.Current.MainWindow;
            mainwindow.Topmost = true; //else the application wouldnt get focused
            mainwindow.Activate();

            mainwindow.Focus();
            mainwindow.Topmost = false;
        }

        void Application_Deactivated(object sender, EventArgs e)
        {
            var first = DockManager.CurrentSide != DockingSide.None;
            var secound = _movedOutSide == Side.Left
                ? BaseWindow.Left >= WpfScreen.MostLeftX
                : BaseWindow.Left <= WpfScreen.MostRightX;
            if (first && secound) //(BaseWindow.ActualHeight == WpfScreen.GetScreenFrom(new Point(BaseWindow.Left, 0)).WorkingArea.Height && (BaseWindow.Left == 0 || BaseWindow.Left == maxwidth - 300) && BaseWindow.Top == 0)
            {
                //The window is at a good site
                MoveWindowOutOfScreen(BaseWindow.Left == WpfScreen.MostLeftX ? Side.Left : Side.Right);
            }
        }

        void activewindowhook_ActiveWindowChanged(object sender, IntPtr hwnd)
        {
            if (!MovedOut) return;
            if (WindowHelper.WindowIsFullscreen(hwnd))
            {
                if (!_strokeWindow.IsInvisible) _strokeWindow.MoveInvisible();
                Debug.Print("{0}: Its a fullscreen window",DateTime.Now.ToString());
            }
            else
            {
                if (_strokeWindow.IsInvisible) _strokeWindow.MoveVisible();
                Debug.Print("{0}: It isnt a fullscreen window", DateTime.Now.ToString());
            }
            if (!_strokeWindow.IsInvisible)
            {
                _strokeWindow.Topmost = false;
                _strokeWindow.Topmost = true;
            }
        }

        protected async void MoveWindowOutOfScreen(Side side)
        {
            BaseWindow.Topmost = true;
            if (MoveOut != null) MoveOut(this, EventArgs.Empty);
            if (side == Side.Left)
            {
                for (var i = 0; i > -32; i--)
                {
                    await Task.Delay(1);
                    BaseWindow.Left = i * 10 + WpfScreen.MostLeftX;
                }
            }
            else
            {
                for (int i = 0; i < 32; i++)
                {
                    await Task.Delay(1);
                    BaseWindow.Left = WpfScreen.MostRightX - BaseWindow.Width + i * 10;
                }
            }

            MovedOut = true;
            BaseWindow.ShowInTaskbar = false;
            _windowHider.HideWindowFromAltTab(BaseWindow);
            _movedOutSide = side;
            StartMagic();
        }

        protected void MoveWindowBackInScreen()
        {
            if (MoveIn != null) MoveIn(this, EventArgs.Empty);
            double newleft;
            if (_movedOutSide == Side.Left) { newleft = WpfScreen.MostLeftX; } else { newleft = WpfScreen.MostRightX - BaseWindow.Width; }
            BaseWindow.Left = _movedOutSide == Side.Left ? newleft + 10 : newleft - 10;

            Storyboard moveWindowBackInScreenStoryboard = new Storyboard();
            DoubleAnimation inanimation = new DoubleAnimation(BaseWindow.Left, newleft, TimeSpan.FromMilliseconds(150), FillBehavior.Stop);
            inanimation.Completed += (s, e) => { BaseWindow.Topmost = false; BaseWindow.Left = newleft; };
            moveWindowBackInScreenStoryboard.Children.Add(inanimation);
            Storyboard.SetTargetName(inanimation, BaseWindow.Name);
            Storyboard.SetTargetProperty(moveWindowBackInScreenStoryboard, new PropertyPath(Window.LeftProperty));

            moveWindowBackInScreenStoryboard.Begin(BaseWindow);

            MovedOut = false;
            BaseWindow.Topmost = true;
            _windowHider.ShowWindowInAltTab(BaseWindow);
            BaseWindow.Activate();
            BaseWindow.ShowInTaskbar = true;
            
            StopMagic();
        }
        protected void StopMagic()
        {
            if (_strokeWindow != null)
            {
                _strokeWindow.Close();
                _strokeWindow = null;
            }
            _activewindowhook.Unhook();
        }

        protected void StartMagic()
        {
            var screen = GetScreenFromSide(_movedOutSide);
            _strokeWindow = new StrokeWindow(screen.WorkingArea.Height, _movedOutSide == Side.Left ? WpfScreen.MostLeftX : WpfScreen.MostRightX, screen.WorkingArea.Top, _movedOutSide);
            _strokeWindow.Show();
            _strokeWindow.MouseMove += StrokeWindowMouseMove;
            _strokeWindow.MouseLeave += StrokeWindowMouseLeave;
            _strokeWindow.MouseDown += StrokeWindowMouseDown;
            _activewindowhook.Hook();
            _activewindowhook.RaiseOne(); //If the current window is fullscreen, the event wouldn't be raised (because nothing changed)
            _mouseWasOver = false;
        }

        void StrokeWindowMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MagicWindow != null)
            {
                var cursorposition = Cursor.Position;
                if (cursorposition.Y > MagicWindow.Top && cursorposition.Y < MagicWindow.Top + MagicWindow.Height && (_movedOutSide == Side.Left ? cursorposition.X < MagicWindow.Width + WpfScreen.MostLeftX : cursorposition.X > WpfScreen.MostRightX - MagicWindow.Width))
                {
                    MoveWindowBackInScreen();
                    _isInZone = false;
                    HideMagicArrow();
                }
            }
        }

        void StrokeWindowMouseMove(object sender, MouseEventArgs e)
        {
            Views.Test.TestWindow.AddMessage("Stroke: Mouse Move");
            if (!_magicArrowIsShown && !_isInZone && StrokeWindow.PositionIsOk(_movedOutSide, Cursor.Position.X, WpfScreen.MostLeftX - 2, W