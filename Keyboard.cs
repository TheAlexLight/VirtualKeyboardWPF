using KeyboardPanelLibrary.Enums;
using KeyboardPanelLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using VirtualKeyboardWPF.Enums;

namespace KeyboardPanelLibrary
{
    public class Keyboard : KeyboardBase
    {
        public Keyboard()
        {
            KeyList = new();

            KeysInRow = new int[4];


        }

        public ComboBoxItem LanguageList { get; set; }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            FillKeyList();

            var b = KeyBackground;
            var a = KeyWidth;
        }

        protected override void FillKeyList()
        {
            Style keyStyle = Application.Current.FindResource("keyStyle") as Style;

            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.Q, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.W, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.E, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.R, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.T, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.Y, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.U, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.I, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.O, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.P, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.OEM4, 1)); //[
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.OEM6, 1)); //]
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.OEM5, 1)); //\
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.Back, 2)); //Backspace 

            KeysInRow[0] = 14;

            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.Tab, 2));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.A, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.S, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.D, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.F, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.G, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.H, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.J, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.K, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.L, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.OEM1, 1)); //;
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.OEM7, 1)); //'
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.Return, 2)); //Enter


            KeysInRow[1] = 13;

            KeyList.Add(SetOneKey(new ToggleButton() { Style = keyStyle }, Width, VirtualKeyCode.Shift, 3));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.Z, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.X, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.C, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.V, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.B, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.N, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.M, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.OEMComma, 1)); //,
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.OEMPeriod, 1)); //.
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, Width, VirtualKeyCode.OEM2, 1)); //?

            KeyList.Add(SetOneKey(new ComboBox() { Background = KeyBackground, Foreground = Foreground, Margin = KeyMargin, FontSize = FontSize}, Width, 0, 2)); //Change language
            var comboBoxStyle = Application.Current.FindResource("comboBoxStyle") as Style;
            KeyList.Last().SetValue(StyleProperty, comboBoxStyle);
            ((ComboBox)KeyList.Last()).ItemsSource = GetLocalLanguages();
            //((ComboBox)KeyList.Last()).SelectedIndex = 0;

            KeysInRow[2] = 12;



            KeyList.Add(SetOneKey(new Button() {Style = keyStyle, Content = "&123" }, KeyWidth, 0, 2)); //Change to numbers
            KeyList.Last().SetValue(StyleProperty, keyStyle);
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, KeyWidth, VirtualKeyCode.Space, 11));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle }, KeyWidth, VirtualKeyCode.Left, 1)); //Left arrow
            KeyList.Add(SetOneKey(new Button() { Style = keyStyle }, KeyWidth, VirtualKeyCode.Right, 1)); //Right arrow
    
            KeysInRow[3] = 4;

            foreach (var key in KeyList)
            {
                key.SetValue(HeightProperty, Height);
            }
        }

        private List<ComboBoxItem> GetLocalLanguages()
        {
            var keyboardLanguages = new List<ComboBoxItem>();

            uint nElements = WinApi.GetKeyboardLayoutList(0, null);
            IntPtr[] keyboardsIds = new IntPtr[nElements];
            WinApi.GetKeyboardLayoutList(keyboardsIds.Length, keyboardsIds);

            foreach (var keyboardId in keyboardsIds)
            {
                var languageId = (UInt16)((UInt32)keyboardId & 0xFFFF);

                CultureInfo languageInfo = new CultureInfo(languageId, false);

                ComboBoxItem comboBoxItem = new();
                comboBoxItem.Content = languageInfo.ThreeLetterWindowsLanguageName;
                comboBoxItem.Tag = languageId;

                keyboardLanguages.Add(comboBoxItem);
            }

            return keyboardLanguages;
        }


    }
}
