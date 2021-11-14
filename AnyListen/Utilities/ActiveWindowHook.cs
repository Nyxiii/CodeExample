using System;
using AnyListen.Utilities.Native;
// ReSharper disable InconsistentNaming

namespace AnyListen.Utilities
{
    class ActiveWindowHook : IDisposable
    {
        public delegate void ActiveWindowChangedHandler(object sender, IntPtr hwnd);
        public event ActiveWindowChangedHandler ActiveWindowChanged;

        const uint WINEVENT_OUTOFCONTEXT = 0;
        const uint EVENT_SYSTEM_FOREGROUND = 3;

        IntPtr m_hhook;
        private readonly WinEventD