using KeyboardPanelLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VirtualKeyboardWPF.Enums;
using static KeyboardPanelLibrary.Extensions.WinApi;

namespace KeyboardPanelLibrary.Extensions.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct KEYBDINPUT
    {
        internal VirtualKeyCode wVk;
        internal ushort wScan;
        internal KEYEVENTF dwFlags;
        internal uint time;
        internal UIntPtr dwExtraInfo;
    }
}
