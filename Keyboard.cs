using KeyboardPanelLibrary.Enums;
using KeyboardPanelLibrary.Extensions;
using KeyboardPanelLibrary.Helper;
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
            //KeyList = new();

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
            ItemsControl keyItemsControl = PropertyNameSearcher.FindChild<ItemsControl>(this, "keyboardItemsControl");

            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Q, 1, 1));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.W, 1, 1));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.E, 1, 1));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.R, 1, 1));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.T, 1, 1));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Y, 2, 1));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.U, 1, 1));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.I, 1, 1));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.O, 1, 1));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.P, 1, 1));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEM4, 1, 1)); //[
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEM6, 1, 1)); //]
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEM5, 1, 1)); //\
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Back, 2, 1)); //Backspace 

            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Tab, 2, 2));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.A, 1, 2));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.S, 1, 2));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.D, 1, 2));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.F, 1, 2));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.G, 1, 2));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.H, 1, 2));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.J, 1, 2));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.K, 1, 2));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.L, 1, 2));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEM1, 1, 2)); //;
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEM7, 1, 2)); //'
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Return, 2, 2)); //Enter

            keyItemsControl.Items.Add(SetOneKey(new ToggleButton(), VirtualKeyCode.Shift, 3, 3));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Z, 1, 3));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.X, 1, 3));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.C, 1, 3));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.V, 2, 3));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.B, 1, 3));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.N, 1, 3));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.M, 1, 3));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEMComma, 1, 3)); //,
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEMPeriod, 1, 3)); //.
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEM2, 1, 3)); //?



            ComboBox combo = new ComboBox(){ Background = KeyBackground, Foreground = Foreground, FontSize = FontSize };
            combo = (ComboBox)SetOneKey(combo, 0, 2, 3);
            var comboBoxStyle = Application.Current.FindResource("comboBoxStyle") as Style;
            combo.SetValue(StyleProperty, comboBoxStyle);
            combo.ItemsSource = GetLocalLanguages();
            keyItemsControl.Items.Add(combo);


            //keyItemsControl.Items.Add(SetOneKey(new ComboBox() { Background = KeyBackground, Foreground = Foreground, FontSize = FontSize }, 0, 2)); //Change language
            //KeyList.Items.Last().SetValue(StyleProperty, comboBoxStyle);
            //((ComboBox)KeyList.Last()).ItemsSource = GetLocalLanguages();
            //((ComboBox)KeyList.Last()).SelectedIndex = 0;

            keyItemsControl.Items.Add(SetOneKey(new Button() { Content = "&123" }, 0, 2, 4)); //Change to numbers
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Space, 11, 4));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Left, 1, 4)); //Left arrow
            keyItemsControl.Items.Add(SetOneKey(new Button(), VirtualKeyCode.Right, 1, 4)); //Right arrow

            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad7, 1, 5));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad8, 1, 5));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad9, 1, 5));

            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad4, 1, 6));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad5, 1, 6));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad6, 1, 6));

            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad1, 1, 7));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad2, 1, 7));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad3, 1, 7));

            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad0, 2, 8));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Decimal, 1, 8));

            Style keyStyle = Application.Current.FindResource("keyStyle") as Style;

            foreach (UIElement key in keyItemsControl.Items)
            {
                key.SetValue(MarginProperty, KeyMargin);

                if (key is ButtonBase)
                {
                    key.SetValue(StyleProperty, keyStyle);
                };
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
