using KeyboardPanelLibrary.Enums;
using KeyboardPanelLibrary.Extensions.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VirtualKeyboardWPF.Enums;

namespace KeyboardPanelLibrary.Extensions
{
    public static class WinApi
    {
        [DllImport("user32.dll")]
        internal static extern uint SendInput(
            uint nInputs,
           INPUT[] pInputs,
            int cbSize);

        [DllImport("user32.dll")]
        static internal extern int ToUnicode(uint virtualKeyCode, uint scanCode, // Convert virtual key to current language
           byte[] keyboardState,
           [Out, MarshalAs(UnmanagedType.LPWStr, SizeConst = 64)]
            StringBuilder receivingBuffer,
           int bufferSize, uint flags);

        [DllImport("user32.dll")]
        static internal extern IntPtr GetKeyboardLayout(uint idThread); // Get current language

        [DllImport("user32.dll")]
        static internal extern uint GetKeyboardLayoutList(int nBuff, [Out] IntPtr[] lpList);

        [DllImport("user32.dll")]
         static internal extern UInt32 ActivateKeyboardLayout(IntPtr hkl, UInt32 flags);
    }
}
