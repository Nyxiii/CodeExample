using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
// ReSharper disable InconsistentNaming

namespace AnyListen.Utilities.Native
{
    internal static class UnsafeNativeMethods
    {
        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        internal static extern bool UnhookWinEvent(IntPt