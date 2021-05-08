using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using AnyListen.Utilities.Native;

namespace AnyListen.Notification.WindowMessages
{
    class WindowMessanger
    {
        // ReSharper disable once InconsistentNaming
        public const int WM_OPENMUSICFILE = 0x4A;
        // ReSharper disable once InconsistentNaming
        public const int WM_BRINGTOFRONT = 3532;

        public event EventHandler BringWindowToFront;
        public event EventHandler<PlayTrackEventArgs> PlayMusicFile;

        public WindowMessanger(Window baseWindow)
        {
            baseWindow.SourceInitialized += (s,e) =>
            {
                var source = PresentationSource.FromVisual(baseWindow) as HwndSource;
                if (source == null) return;
                source.AddHook(WndProc);
            };
        }
        
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_BRINGTOFRONT:
                    if (BringWindowToFront != null) BringWindowToFront(this, EventArgs.Empty);
                    break;
                case WM_OPENMUSICFILE:
                    if (PlayMusicFile != null)
                    {
                        CopyDataStruct st = (CopyDataStruct)Marshal.PtrToStructure(lParam, typeof(CopyDataStruct));
                        string strData = Marshal.PtrToStringUni(st.lpData);
                        PlayMusicFile(this, new PlayTrackEventArgs(strData));
                    }
                    brea