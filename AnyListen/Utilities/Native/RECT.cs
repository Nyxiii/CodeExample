using System;
using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace AnyListen.Utilities.Native
{
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    public struct RECT
    {
        public int left;
        public int top;
 