using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static KeyboardPanelLibrary.Extensions.WinApi;

namespace KeyboardPanelLibrary.Extensions.Structs
{
    // Declare the INPUT struct
    [StructLayout(LayoutKind.Sequential)]
    public struct INPUT
    {
        internal uint type;
        internal InputUnion inputUinion;
        internal static int Size
        {
            get { return Marshal.SizeOf(typeof(INPUT)); }
        }
    }
}
