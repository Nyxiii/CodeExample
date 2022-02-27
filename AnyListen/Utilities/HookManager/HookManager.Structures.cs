using System.Runtime.InteropServices;
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable MemberCanBePrivate.Local
// ReSharper disable once InconsistentNaming

namespace AnyListen.Utilities.HookManager
{
    public static partial class HookManager
    {
        /// <summary>
        /// The Point structure defines the X- and Y- coordinates of a point. 
        /// </summary>
        /// <remarks>
        /// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/gdi/rectangl_0tiq.asp
        /// </remarks>
        [StructLayout(LayoutKind.Sequential)]
        private struct Point
        {
            /// <summary>
            /// Specifies the X-coordinate of the point. 
            /// </summary>
            public int X;
            /// <summary>
            /// Specifies the Y-coordinate of the point. 
            /// </summary>
            public int Y;
        }

        /// <summary>
        /// The MSLLHOOKSTRUCT structure contains information about a low-level keyboard input event. 
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct MouseLLHookStruct
        {
            /// <summary>
            /// Specifies a Point structure that contains the X- and Y-coordinates of the cursor, in screen coordinates. 
            /// </summary>
            public Point Point;
            /// <summary>
            /// If the message is WM_MOUSEWHEEL, the high-order word of this member is the wheel delta. 
            /// The l