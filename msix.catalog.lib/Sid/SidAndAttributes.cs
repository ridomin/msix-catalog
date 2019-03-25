using System;
using System.Runtime.InteropServices;

namespace msix.catalog.lib
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct SID_AND_ATTRIBUTES
    {
        public IntPtr Sid;
        public int Attributes;
    }
}