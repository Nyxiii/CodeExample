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
        private bool _mouseWas