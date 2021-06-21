using KeyboardPanelLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using VirtualKeyboardWPF.Enums;

namespace KeyboardPanelLibrary
{
    public class Keyboard : KeyboardBase
    {
        public Keyboard()
        {
            KeyList = new();

            KeysInRow = new int[4];

            FillKeyList();
        }

        protected override void FillKeyList()
        {
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.Q, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.W, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.E, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.R, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.T, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.Y, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.U, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.I, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.O, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.P, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.OEM4, 1)); //[
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.OEM6, 1)); //]
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.Back, 3)); //Backspace 

            KeysInRow[0] = 13;

            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.Tab, 2));
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.A, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.S, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.D, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.F, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.G, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.H, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.J, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.K, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.L, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.OEM1, 1)); //;
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.OEM7, 1)); //'
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.Return, 2)); //Enter


            KeysInRow[1] = 13;

            KeyList.Add(SetOneKey(new ToggleButton(), Width, Margin, VirtualKeyCode.Shift, 3));
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.Z, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.X, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.C, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.V, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.B, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.N, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.M, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.OEMComma, 1)); //,
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.OEMPeriod, 1)); //.
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.OEM2, 1)); //?
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.OEM5, 1)); //\
            KeyList.Add(SetOneKey(new Button() { Content = "Lang" }, Width, Margin, 0, 1)); //Change language

            KeysInRow[2] = 13;

            KeyList.Add(SetOneKey(new Button() { Content = "Symb" }, Width, Margin, 0, 2)); //Change to numbers
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.Space, 11));
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.Left, 1)); //Left arrow
            KeyList.Add(SetOneKey(new RepeatButton(), Width, Margin, VirtualKeyCode.Right, 1)); //Right arrow

            KeysInRow[3] = 4;

            foreach (var key in KeyList)
            {
                key.SetValue(HeightProperty, Height);
            }
        }

        //private UIElement SetOneKey(ButtonBase buttonType, double keyWidth, Thickness margin, VirtualKeyCode virtualKey, double widthCoefficient)
        //{

        //    buttonType.Margin = margin;
        //    buttonType.Focusable = false;

        //    KeyboardAdditionalMetadata additionalMetadata = new();
        //    additionalMetadata.VirtualCode = (ushort)virtualKey;
        //    additionalMetadata.WidthCoefficient = widthCoefficient;

        //    SetAdditionalMetadataProperty(buttonType, additionalMetadata);

        //    buttonType.SetValue(WidthProperty, keyWidth * GetAdditionalMetadataProperty(buttonType).WidthCoefficient);

        //    return buttonType;
        //}

    }
}
