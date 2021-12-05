using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AnyListen.Utilities.HookManager
{
    public static partial class HookManager
    {
        /// <summary>
        /// The CallWndProc hook procedure is an application-defined or library-defined callback 
        /// function used with the SetWindowsHookEx function. The HOOKPROC type defines a pointer 
        /// to this callback function. CallWndProc is a placeholder for the application-defined 
        /// or library-defined function name.
        /// </summary>
        /// <param name="nCode">
        /// [in] Specifies whether the hook procedure must process the message. 
        /// If nCode is HC_ACTION, the hook procedure must process the message. 
        /// If nCode is less than zero, the hook procedure must pass the message to the 
        /// CallNextHookEx function without further processing and must return the 
        /// value returned by CallNextHookEx.
        /// </param>
        /// <param name="wParam">
        /// [in] Specifies whether the message was sent by the current thread. 
        /// If the message was sent by the current thread, it is nonzero; otherwise, it is zero. 
        /// </param>
        /// <param name="lParam">
        /// [in] Pointer to a CWPSTRUCT structure that contains details about the message. 
        /// </param>
        //