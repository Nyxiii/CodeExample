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
            ///