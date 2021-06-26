using KeyboardPanelLibrary.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using VirtualKeyboardWPF.Enums;

namespace KeyboardPanelLibrary
{
    public class Numpad : KeyboardBase
    {
        public Numpad()
        {
            //KeyList = new();

            KeysInRow = new int[4];

            FillKeyList();
        }

        protected override void FillKeyList()
        {
            ItemsControl keyItemsControl = PropertyNameSearcher.FindChild<ItemsControl>(this, "keyboardItemsControl");

            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad7, 1, 5));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad8, 1, 5));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad9, 1, 5));

            KeysInRow[0] = 3;

            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad4, 1, 6));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad5, 1, 6));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad6, 1, 6));

            KeysInRow[1] = 3;

            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad1, 1, 7));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad2, 1, 7));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad3, 1, 7));

            KeysInRow[2] = 3;

            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad0, 2, 8));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Decimal, 1, 8));

            KeysInRow[3] = 2;

            Style keyStyle = Application.Current.FindResource("keyStyle") as Style;

            foreach (UIElement key in keyItemsControl.Items)
            {
                 key.SetValue(StyleProperty, keyStyle);
            }
        }
    }
}
