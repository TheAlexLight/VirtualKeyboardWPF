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
            KeyList.Add(SetOneKey(new RepeatButton() { Background = Background, Foreground = Foreground, Margin = Margin }, Width, VirtualKeyCode.Numpad7, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Background = Background, Foreground = Foreground, Margin = Margin }, Width, VirtualKeyCode.Numpad8, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Background = Background, Foreground = Foreground, Margin = Margin }, Width, VirtualKeyCode.Numpad9, 1));

            KeysInRow[0] = 3;

            KeyList.Add(SetOneKey(new RepeatButton() { Background = Background, Foreground = Foreground, Margin = Margin }, Width, VirtualKeyCode.Numpad4, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Background = Background, Foreground = Foreground, Margin = Margin }, Width, VirtualKeyCode.Numpad5, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Background = Background, Foreground = Foreground, Margin = Margin }, Width, VirtualKeyCode.Numpad6, 1));

            KeysInRow[1] = 3;

            KeyList.Add(SetOneKey(new RepeatButton() { Background = Background, Foreground = Foreground, Margin = Margin }, Width, VirtualKeyCode.Numpad1, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Background = Background, Foreground = Foreground, Margin = Margin }, Width, VirtualKeyCode.Numpad2, 1));
            KeyList.Add(SetOneKey(new RepeatButton() { Background = Background, Foreground = Foreground, Margin = Margin }, Width, VirtualKeyCode.Numpad3, 1));

            KeysInRow[2] = 3;

            KeyList.Add(SetOneKey(new RepeatButton() { Background = Background, Foreground = Foreground, Margin = Margin }, Width, VirtualKeyCode.Numpad0, 2));
            KeyList.Add(SetOneKey(new RepeatButton() { Background = Background, Foreground = Foreground, Margin = Margin }, Width, VirtualKeyCode.OEMPeriod, 1));

            KeysInRow[3] = 2;

            foreach (var key in KeyList)
            {
                key.SetValue(HeightProperty, Height);
            }
        }
    }
}
