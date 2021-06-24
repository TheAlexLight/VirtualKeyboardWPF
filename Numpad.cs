using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using VirtualKeyboardWPF.Enums;

namespace KeyboardPanelLibrary
{
    public class Numpad : KeyboardBase
    {
        public Numpad()
        {
            KeyList = new();

            KeysInRow = new int[4];

            FillKeyList();
        }

        protected override void FillKeyList()
        {
            KeyList.Add(SetOneKey(new RepeatButton() { Background = Background, Foreground = Foreground, Margin = Margin }, VirtualKeyCode.Numpad7, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Background = Background, Foreground = Foreground, Margin = Margin }, VirtualKeyCode.Numpad8, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Background = Background, Foreground = Foreground, Margin = Margin }, VirtualKeyCode.Numpad9, 1));

            KeysInRow[0] = 3;

            KeyList.Add(SetOneKey(new RepeatButton() { Background = Background, Foreground = Foreground, Margin = Margin }, VirtualKeyCode.Numpad4, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Background = Background, Foreground = Foreground, Margin = Margin }, VirtualKeyCode.Numpad5, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Background = Background, Foreground = Foreground, Margin = Margin }, VirtualKeyCode.Numpad6, 1));

            KeysInRow[1] = 3;

            KeyList.Add(SetOneKey(new RepeatButton() { Background = Background, Foreground = Foreground, Margin = Margin }, VirtualKeyCode.Numpad1, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Background = Background, Foreground = Foreground, Margin = Margin }, VirtualKeyCode.Numpad2, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Background = Background, Foreground = Foreground, Margin = Margin }, VirtualKeyCode.Numpad3, 1));

            KeysInRow[2] = 3;

            KeyList.Add(SetOneKey(new RepeatButton() { Background = Background, Foreground = Foreground, Margin = Margin }, VirtualKeyCode.Numpad0, 2));
            KeyList.Add(SetOneKey(new RepeatButton() { Background = Background, Foreground = Foreground, Margin = Margin }, VirtualKeyCode.Decimal, 1));

            KeysInRow[3] = 2;

            foreach (var key in KeyList)
            {
                key.SetValue(HeightProperty, Height);
            }
        }
    }
}
