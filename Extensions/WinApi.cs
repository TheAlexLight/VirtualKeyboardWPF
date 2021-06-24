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
        /// <summary>
        /// Declaration of external SendInput method
        /// </summary>
        [DllImport("user32.dll")]
        internal static extern uint SendInput(
            uint nInputs,
           /* [MarshalAs(UnmanagedType.LPArray), In]*/ INPUT[] pInputs,
            int cbSize);

        //[DllImport("user32.dll")]
        //static internal extern int MapVirtualKey(uint uCode, uint uMapType);

        [DllImport("user32.dll")]
        static internal extern int ToUnicode(uint virtualKeyCode, uint scanCode, // Convert virtual key to current language
           byte[] keyboardState,
           [Out, MarshalAs(UnmanagedType.LPWStr, SizeConst = 64)]
            StringBuilder receivingBuffer,
           int bufferSize, uint flags);

        //[DllImport("user32.dll")]
        //static internal extern IntPtr GetKeyboardLayout(uint idThread); // Get current language

        [DllImport("user32.dll")]
        static internal extern uint GetKeyboardLayoutList(int nBuff, [Out] IntPtr[] lpList);

        // [DllImport("user32.dll", CharSet = CharSet.Auto)]
        // internal static extern int GetKeyNameText(
        //int lParam,
        //[MarshalAs(UnmanagedType.LPWStr), Out] System.Text.StringBuilder str,
        //int size);

        //[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        //static internal extern int GetLocaleInfoEx(String lpLocaleName, LCTYPE LCType, StringBuilder lpLCData, int cchData);

        [DllImport("user32.dll")]
         static internal extern UInt32 ActivateKeyboardLayout(IntPtr hkl, UInt32 flags);

    }
}
