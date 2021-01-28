
﻿using System;
using System.Windows;
using System.Windows.Input;
using AnyListen.Settings;
using AnyListen.Utilities;
using AnyListen.Utilities.HookManager;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;

namespace AnyListen.MagicArrow.DockManager
{
    public class DockManager : IDisposable
    {
        #region Events

        public event EventHandler Undocked;
        public event EventHandler Docked;
        public event EventHandler DragStopped;

        protected void OnUndocked()
        {
            if (Undocked != null) Undocked(this, EventArgs.Empty);
        }

        protected void OnDocked()
        {
            if (Docked != null) Docked(this, EventArgs.Empty);
        }

        protected void OnDragStopped()
        {
            if (DragStopped != null) DragStopped(this, EventArgs.Empty);
        }

        #endregion

        #region Constructor

        private readonly Window _basewindow;
        public DockManager(Window window)
        {
            _basewindow = window;
            if (AnyListenSettings.Instance.CurrentState.ApplicationState != null) CurrentSide = AnyListenSettings.Instance.CurrentState.ApplicationState.CurrentSide;
        }

        #endregion

        #region Properties

        public bool IsEnabled { get; set; }

        #endregion

        public double WindowHeight { get; set; }

        private WindowPositionSide? _newSide;
        public WindowPositionSide? NewSide
        {
            get { return _newSide; }
            set { _newSide = value; }
        }

        public DockingSide CurrentSide { get; set; } //the applied side

        public void DragStart()
        {
            if (IsEnabled) return;
            IsEnabled = true;
            NewSide = null;
            _firedUndocked = false;
            HookManager.MouseMove += HookManager_MouseMove;
        }

        protected bool MouseIsLeftRightOrTop(int mouseX, int mouseY, out WindowPositionSide? side)
        {
            if (mouseX < WpfScreen.MostLeftX + 5)
            {
                side = WindowPositionSide.Left;
                return true;
            }
            if (mouseX >= WpfScreen.MostRightX - 5)
            {
                side = WindowPositionSide.Right;
                return true;
            }
            if (mouseY < 5)
            {
                side = WindowPositionSide.Top;
                return true;
            }
            side = WindowPositionSide.None;
            return false;
        }

        protected bool WindowIsLeftOrRight()
        {
            return _basewindow.Left == WpfScreen.MostLeftX || (_basewindow.Left == WpfScreen.MostRightX - _basewindow.Width);
        }

        private bool _firedUndocked;
        void HookManager_MouseMove(object sender, MouseEventArgs e)
        {
            
            //Side dock > top dock
            if (!IsEnabled) return;
            if (Mouse.LeftButton == MouseButtonState.Released) //If the user doubleclicks the window, relocates the window and releases the mouse, it doesn't get stopped
            {
                DragStop();
                return;
            }

            if (MouseIsLeftRightOrTop(e.X, e.Y, out _newSide))
            {
                var screen = WpfScreen.GetScreenFrom(new Point(e.X, e.Y));
                if (NewSide == WindowPositionSide.Left || NewSide == WindowPositionSide.Right)
                {
                    if (!IsAtRightOrLeftBorder)
                    {
                        IsAtRightOrLeftBorder = true;
                        OpenWindow(NewSide.Value, screen);
                        if (IsAtTop) IsAtTop = false;
                    }
                    return;
                }
                if (IsAtRightOrLeftBorder)
                {
                    IsAtRightOrLeftBorder = false;
                    CloseWindowIfExists();
                }
                if (NewSide == WindowPositionSide.Top)
                {
                    if (!IsAtTop || screen.DeviceName != DisplayingScreen)
                    {
                        IsAtTop = true;
                        OpenWindow(NewSide.Value, screen);
                        DisplayingScreen = screen.DeviceName;
                    }
                }
            }
            else
            {
                if (!_firedUndocked)