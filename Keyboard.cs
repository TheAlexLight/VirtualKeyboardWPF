using KeyboardPanelLibrary.Extensions;
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
    public class Keyboard : KeyboardBase
    {
        public Keyboard(double keyWidth, double keyHeight, Thickness margin)
        {
            KeyList = new();

            KeysInRow = 15;

            FullWidth = (keyWidth + margin.Left + margin.Right) * KeysInRow;

            FillKeyList(keyWidth,keyHeight, margin);
        }

        public override double FullWidth { get; set; }
        public override List<UIElement> KeyList { get; set; }
        public override int KeysInRow { get; set; }

        private void FillKeyList(double keyWidth, double keyHeight, Thickness margin)
        {
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.Tab, 2));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.Q, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.W, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.E, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.R, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.T, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.Y, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.U, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.I, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.O, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.P, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.OEM4, 1)); //[
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.OEM6, 1)); //]
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.Back, 2)); //Backspace 

            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.A, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.S, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.D, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.F, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.G, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.H, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.J, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.K, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.L, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.OEM1, 1)); //;
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.OEM7, 1)); //'
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.Return, 5)); //Enter

            KeyList.Add(SetOneKey(new ToggleButton(), keyWidth, keyHeight, VirtualKeyCode.Shift, 3));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.Z, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.X, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.C, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.V, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.B, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.N, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.M, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.OEMComma, 1)); //,
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.OEMPeriod, 1)); //.
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.OEM2, 1)); //?
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.OEM5, 1)); //\
            KeyList.Add(SetOneKey(new Button() {Content = "Lang"}, keyWidth, keyHeight, 0, 1)); //Change language
           
            KeyList.Add(SetOneKey(new Button() {Content = "Symb"}, keyWidth, keyHeight, 0, 2)); //Change to numbers
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.Space, 12));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.Left, 1)); //Left arrow
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, keyHeight, VirtualKeyCode.Right, 1)); //Right arrow
        }

        private UIElement SetOneKey(ButtonBase buttonType, double keyWidth, double keyHeight, VirtualKeyCode virtualKey, double widthCoefficient)
        {

            buttonType.Height = keyHeight;
            buttonType.Focusable = false;

            AttachedProperty.SetVirtualKeyCodeProperty(buttonType, (ushort)virtualKey);
            AttachedProperty.SetWidthCoefficientProperty(buttonType, widthCoefficient);
            buttonType.SetValue(WidthProperty, keyWidth * AttachedProperty.GetWidthCoefficientProperty(buttonType));

            return buttonType; 
        }
    }
}
