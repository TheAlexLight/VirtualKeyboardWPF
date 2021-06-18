using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardPanelLibrary.Enums
{
    public enum ScanCode : ushort
    {
        LeftButton = 0,
        RightButton = 0,
        Cancel = 70,
        MiddleButton = 0,
        ExtraButton1 = 0,
        ExtraButton2 = 0,
        Back = 14,
        Tab = 15,
        Clear = 76,
        Return = 28,
        Shift = 42,
        Control = 29,
        /// <summary></summary>
        Menu = 56,
        /// <summary></summary>
        Pause = 0,
        /// <summary></summary>
        CapsLock = 58,
        /// <summary></summary>
        Kana = 0,
        /// <summary></summary>
        Hangeul = 0,
        /// <summary></summary>
        Hangul = 0,
        /// <summary></summary>
        Junja = 0,
        /// <summary></summary>
        Final = 0,
        /// <summary></summary>
        Hanja = 0,
        /// <summary></summary>
        Kanji = 0,
        /// <summary></summary>
        Escape = 1,
        /// <summary></summary>
        Convert = 0,
        /// <summary></summary>
        NonConvert = 0,
        /// <summary></summary>
        Accept = 0,
        /// <summary></summary>
        ModeChange = 0,
        /// <summary></summary>
        Space = 57,
        /// <summary></summary>
        Prior = 73,
        /// <summary></summary>
        Next = 81,
        /// <summary></summary>
        End = 79,
        /// <summary></summary>
        Home = 71,
        /// <summary></summary>
        Left = 75,
        /// <summary></summary>
        Up = 72,
        /// <summary></summary>
        Right = 77,
        /// <summary></summary>
        Down = 80,
        /// <summary></summary>
        Select = 0,
        /// <summary></summary>
        Print = 0,
        /// <summary></summary>
        Execute = 0,
        /// <summary></summary>
        Snapshot = 84,
        /// <summary></summary>
        Insert = 82,
        /// <summary></summary>
        Delete = 83,
        /// <summary></summary>
        Help = 99,
        /// <summary></summary>
        N0 = 11,
        /// <summary></summary>
        N1 = 2,
        /// <summary></summary>
        N2 = 3,
        /// <summary></summary>
        N3 = 4,
        /// <summary></summary>
        N4 = 5,
        /// <summary></summary>
        N5 = 6,
        /// <summary></summary>
        N6 = 7,
        /// <summary></summary>
        N7 = 8,
        /// <summary></summary>
        N8 = 9,
        /// <summary></summary>
        N9 = 10,
        /// <summary></summary>
        A = 30,
        /// <summary></summary>
        B = 48,
        /// <summary></summary>
        C = 46,
        /// <summary></summary>
        D = 32,
        /// <summary></summary>
        E = 18,
        /// <summary></summary>
        F = 33,
        /// <summary></summary>
        G = 34,
        /// <summary></summary>
        H = 35,
        /// <summary></summary>
        I = 23,
        /// <summary></summary>
        J = 36,
        /// <summary></summary>
        K = 37,
        /// <summary></summary>
        L = 38,
        /// <summary></summary>
        M = 50,
        /// <summary></summary>
        N = 49,
        /// <summary></summary>
        O = 24,
        /// <summary></summary>
        P = 25,
        /// <summary></summary>
        Q = 16,
        /// <summary></summary>
        R = 19,
        /// <summary></summary>
        S = 31,
        /// <summary></summary>
        T = 20,
        /// <summary></summary>
        U = 22,
        /// <summary></summary>
        V = 17,
        /// <summary></summary>
        W = 47,
        /// <summary></summary>
        X = 45,
        /// <summary></summary>
        Y = 21,
        /// <summary></summary>
        Z = 44,
        /// <summary></summary>
        LeftWindows = 91,
        /// <summary></summary>
        RightWindows = 92,
        /// <summary></summary>
        Application = 93,
        /// <summary></summary>
        Sleep = 95,
        /// <summary></summary>
        Numpad0 = 82,
        /// <summary></summary>
        Numpad1 = 79,
        /// <summary></summary>
        Numpad2 = 80,
        /// <summary></summary>
        Numpad3 = 81,
        /// <summary></summary>
        Numpad4 = 75,
        /// <summary></summary>
        Numpad5 = 76,
        /// <summary></summary>
        Numpad6 = 77,
        /// <summary></summary>
        Numpad7 = 71,
        /// <summary></summary>
        Numpad8 = 72,
        /// <summary></summary>
        Numpad9 = 73,
        /// <summary></summary>
        Multiply = 55,
        /// <summary></summary>
        Add = 78,
        /// <summary></summary>
        Separator = 0,
        /// <summary></summary>
        Subtract = 74,
        /// <summary></summary>
        Decimal = 83,
        /// <summary></summary>
        Divide = 53,
        /// <summary></summary>
        F1 = 59,
        /// <summary></summary>
        F2 = 60,
        /// <summary></summary>
        F3 = 61,
        /// <summary></summary>
        F4 = 62,
        /// <summary></summary>
        F5 = 63,
        /// <summary></summary>
        F6 = 64,
        /// <summary></summary>
        F7 = 65,
        /// <summary></summary>
        F8 = 66,
        /// <summary></summary>
        F9 = 67,
        /// <summary></summary>
        F10 = 68,
        /// <summary></summary>
        F11 = 87,
        /// <summary></summary>
        F12 = 88,
        /// <summary></summary>
        F13 = 100,
        /// <summary></summary>
        F14 = 101,
        /// <summary></summary>
        F15 = 102,
        /// <summary></summary>
        F16 = 103,
        /// <summary></summary>
        F17 = 104,
        /// <summary></summary>
        F18 = 105,
        /// <summary></summary>
        F19 = 106,
        /// <summary></summary>
        F20 = 107,
        /// <summary></summary>
        F21 = 108,
        /// <summary></summary>
        F22 = 109,
        /// <summary></summary>
        F23 = 110,
        /// <summary></summary>
        F24 = 118,
        /// <summary></summary>
        NumLock = 69,
        /// <summary></summary>
        ScrollLock = 70,
        /// <summary></summary>
        NEC_Equal = 0,
        /// <summary></summary>
        Fujitsu_Jisho = 0,
        /// <summary></summary>
        Fujitsu_Masshou = 0,
        /// <summary></summary>
        Fujitsu_Touroku = 0,
        /// <summary></summary>
        Fujitsu_Loya = 0,
        /// <summary></summary>
        Fujitsu_Roya = 0,
        /// <summary></summary>
        LeftShift = 42,
        /// <summary></summary>
        RightShift = 54,
        /// <summary></summary>
        LeftControl = 29,
        /// <summary></summary>
        RightControl = 29,
        /// <summary></summary>
        LeftMenu = 56,
        /// <summary></summary>
        RightMenu = 56,
        /// <summary></summary>
        BrowserBack = 106,
        /// <summary></summary>
        BrowserForward = 105,
        /// <summary></summary>
        BrowserRefresh = 103,
        /// <summary></summary>
        BrowserStop = 104,
        /// <summary></summary>
        BrowserSearch = 101,
        /// <summary></summary>
        BrowserFavorites = 102,
        /// <summary></summary>
        BrowserHome = 50,
        /// <summary></summary>
        VolumeMute = 32,
        /// <summary></summary>
        VolumeDown = 46,
        /// <summary></summary>
        VolumeUp = 48,
        /// <summary></summary>
        MediaNextTrack = 25,
        /// <summary></summary>
        MediaPrevTrack = 16,
        /// <summary></summary>
        MediaStop = 36,
        /// <summary></summary>
        MediaPlayPause = 34,
        /// <summary></summary>
        LaunchMail = 108,
        /// <summary></summary>
        LaunchMediaSelect = 109,
        /// <summary></summary>
        LaunchApplication1 = 107,
        /// <summary></summary>
        LaunchApplication2 = 33,
        /// <summary></summary>
        OEM1 = 39,
        /// <summary></summary>
        OEMPlus = 13,
        /// <summary></summary>
        OEMComma = 51,
        /// <summary></summary>
        OEMMinus = 12,
        /// <summary></summary>
        OEMPeriod = 52,
        /// <summary></summary>
        OEM2 = 53,
        /// <summary></summary>
        OEM3 = 41,
        /// <summary></summary>
        OEM4 = 26,
        /// <summary></summary>
        OEM5 = 43,
        /// <summary></summary>
        OEM6 = 27,
        /// <summary></summary>
        OEM7 = 40,
        /// <summary></summary>
        OEM8 = 0,
        /// <summary></summary>
        OEMAX = 0,
        /// <summary></summary>
        OEM102 = 86,
        /// <summary></summary>
        ICOHelp = 0,
        /// <summary></summary>
        ICO00 = 0,
        /// <summary></summary>
        ProcessKey = 0,
        /// <summary></summary>
        ICOClear = 0,
        /// <summary></summary>
        Packet = 0,
        /// <summary></summary>
        OEMReset = 0,
        /// <summary></summary>
        OEMJump = 0,
        /// <summary></summary>
        OEMPA1 = 0,
        /// <summary></summary>
        OEMPA2 = 0,
        /// <summary></summary>
        OEMPA3 = 0,
        /// <summary></summary>
        OEMWSCtrl = 0,
        /// <summary></summary>
        OEMCUSel = 0,
        /// <summary></summary>
        OEMATTN = 0,
        /// <summary></summary>
        OEMFinish = 0,
        /// <summary></summary>
        OEMCopy = 0,
        /// <summary></summary>
        OEMAuto = 0,
        /// <summary></summary>
        OEMENLW = 0,
        /// <summary></summary>
        OEMBackTab = 0,
        /// <summary></summary>
        ATTN = 0,
        /// <summary></summary>
        CRSel = 0,
        /// <summary></summary>
        EXSel = 0,
        /// <summary></summary>
        EREOF = 93,
        /// <summary></summary>
        Play = 0,
        /// <summary></summary>
        Zoom = 98,
        /// <summary></summary>
        Noname = 0,
        /// <summary></summary>
        PA1 = 0,
        /// <summary></summary>
        OEMClear = 0
    }
}
