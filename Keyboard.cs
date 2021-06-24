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
        }

        protected override void FillKeyList()
        {
            Style keyStyle = Application.Current.FindResource("keyStyle") as Style;

            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.Q, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.W, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.E, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.R, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.T, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.Y, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.U, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.I, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.O, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.P, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.OEM4, 1)); //[
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.OEM6, 1)); //]
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.OEM5, 1)); //\
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.Back, 2)); //Backspace 

            KeysInRow[0] = 14;

            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.Tab, 2));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.A, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.S, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.D, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.F, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.G, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.H, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.J, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.K, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.L, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.OEM1, 1)); //;
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.OEM7, 1)); //'
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.Return, 2)); //Enter


            KeysInRow[1] = 13;

            KeyList.Add(SetOneKey(new ToggleButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.Shift, 3));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.Z, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.X, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.C, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.V, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.B, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.N, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.M, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.OEMComma, 1)); //,
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.OEMPeriod, 1)); //.
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.OEM2, 1)); //?

            KeyList.Add(SetOneKey(new ComboBox() { Background = KeyBackground, Foreground = Foreground, Margin = KeyMargin, FontSize = FontSize}, 0, 2)); //Change language
            var comboBoxStyle = Application.Current.FindResource("comboBoxStyle") as Style;
            KeyList.Last().SetValue(StyleProperty, comboBoxStyle);
            ((ComboBox)KeyList.Last()).ItemsSource = GetLocalLanguages();
            //((ComboBox)KeyList.Last()).SelectedIndex = 0;

            KeysInRow[2] = 12;



            KeyList.Add(SetOneKey(new Button() {Style = keyStyle, Margin = KeyMargin, Content = "&123" }, 0, 2)); //Change to numbers
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.Space, 11));
            KeyList.Add(SetOneKey(new RepeatButton() { Style = keyStyle, Margin = KeyMargin }, VirtualKeyCode.Left, 1)); //Left arrow
            KeyList.Add(SetOneKey(new Button() { Style = keyStyle }, VirtualKeyCode.Right, 1)); //Right arrow
    
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
