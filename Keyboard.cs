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

            Width = (keyWidth + margin.Left + margin.Right) * KeysInRow;

            FillKeyList(keyWidth,keyHeight, margin);
        }

        public override double Width { get; set; }
        public override List<UIElement> KeyList { get; set; }
        public override int KeysInRow { get; set; }

        private void FillKeyList(double keyWidth, double keyHeight, Thickness margin)
        {
            KeyList.Add(new RepeatButton() {Tag = (ushort)VirtualKeyCode.Tab, Width = (keyWidth + margin.Left + margin.Right) * 2 - margin.Left - margin.Right
                    , Height = keyHeight, Focusable = false});
            KeyList.Add(new RepeatButton() {Tag = (ushort)VirtualKeyCode.Q, Width = keyWidth, Height = keyHeight, Focusable = false });
            KeyList.Add(new RepeatButton() {Tag = (ushort)VirtualKeyCode.W, Width = keyWidth, Height = keyHeight, Focusable = false });
            KeyList.Add(new RepeatButton() {Tag = (ushort)VirtualKeyCode.E, Width = keyWidth, Height = keyHeight, Focusable = false });
            KeyList.Add(new RepeatButton() {Tag = (ushort)VirtualKeyCode.R, Width = keyWidth, Height = keyHeight, Focusable = false });
            KeyList.Add(new RepeatButton() {Tag = (ushort)VirtualKeyCode.T, Width = keyWidth, Height = keyHeight, Focusable = false });
            KeyList.Add(new RepeatButton() {Tag = (ushort)VirtualKeyCode.Y, Width = keyWidth, Height = keyHeight, Focusable = false });
            KeyList.Add(new RepeatButton() {Tag = (ushort)VirtualKeyCode.U, Width = keyWidth, Height = keyHeight, Focusable = false });
            KeyList.Add(new RepeatButton() {Tag = (ushort)VirtualKeyCode.I, Width = keyWidth, Height = keyHeight, Focusable = false });
            KeyList.Add(new RepeatButton() {Tag = (ushort)VirtualKeyCode.O, Width = keyWidth, Height = keyHeight, Focusable = false });
            KeyList.Add(new RepeatButton() {Tag = (ushort)VirtualKeyCode.P, Width = keyWidth, Height = keyHeight, Focusable = false });
            KeyList.Add(new RepeatButton() {Tag = (ushort)VirtualKeyCode.OEM4, Width = keyWidth, Height = keyHeight, Focusable = false }); //[
            KeyList.Add(new RepeatButton() {Tag = (ushort)VirtualKeyCode.OEM6, Width = keyWidth, Height = keyHeight, Focusable = false }); //]
            KeyList.Add(new RepeatButton() {Tag = (ushort)VirtualKeyCode.Back, Width = (keyWidth + margin.Left + margin.Right) * 2 - margin.Left - margin.Right
                    , Height = keyHeight, Focusable = false }); //Backspace

            KeyList.Add(new ToggleButton() { Tag = (ushort)VirtualKeyCode.CapsLock, Width = (keyWidth + margin.Left + margin.Right) * 2.5 - margin.Left - margin.Right
                    , Height = keyHeight, Focusable = false });
            KeyList.Add(new RepeatButton() { Tag = (ushort)VirtualKeyCode.A, Width = keyWidth, Height = keyHeight, Focusable = false });
            KeyList.Add(new RepeatButton() { Tag = (ushort)VirtualKeyCode.S, Width = keyWidth, Height = keyHeight, Focusable = false });
            KeyList.Add(new RepeatButton() { Tag = (ushort)VirtualKeyCode.D, Width = keyWidth, Height = keyHeight, Focusable = false });
            KeyList.Add(new RepeatButton() { Tag = (ushort)VirtualKeyCode.F, Width = keyWidth, Height = keyHeight, Focusable = false });
            KeyList.Add(new RepeatButton() { Tag = (ushort)VirtualKeyCode.G, Width = keyWidth, Height = keyHeight, Focusable = false });
            KeyList.Add(new RepeatButton() { Tag = (ushort)VirtualKeyCode.H, Width = keyWidth, Height = keyHeight, Focusable = false });
            KeyList.Add(new RepeatButton() { Tag = (ushort)VirtualKeyCode.J, Width = keyWidth, Height = keyHeight, Focusable = false });
            KeyList.Add(new RepeatButton() { Tag = (ushort)VirtualKeyCode.K, Width = keyWidth, Height = keyHeight, Focusable = false });
            KeyList.Add(new RepeatButton() { Tag = (ushort)VirtualKeyCode.L, Width = keyWidth, Height = keyHeight, Focusable = false });
            KeyList.Add(new RepeatButton() { Tag = (ushort)VirtualKeyCode.OEM1, Width = keyWidth, Height = keyHeight, Focusable = false }); //;
            KeyList.Add(new RepeatButton() { Tag = (ushort)VirtualKeyCode.OEM7, Width = keyWidth, Height = keyHeight, Focusable = false }); //'
            KeyList.Add(new RepeatButton() { Tag = (ushort)VirtualKeyCode.Return, Width = (keyWidth + margin.Left + margin.Right) * 2.5 - margin.Left - margin.Right
                    , Height = keyHeight, Focusable = false }); //Enter

            KeyList.Add(new ToggleButton() { Tag = (ushort)VirtualKeyCode.Shift, Width = (keyWidth + margin.Left + margin.Right) * 3 - margin.Left - margin.Right
                    , Height = keyHeight, Focusable = false });
            KeyList.Add(new RepeatButton() { Tag = (ushort)VirtualKeyCode.Z, Width = keyWidth, Height = keyHeight, Focusable = false });
            KeyList.Add(new RepeatButton() { Tag = (ushort)VirtualKeyCode.X, Width = keyWidth, Height = keyHeight, Focusable = false });
            KeyList.Add(new RepeatButton() { Tag = (ushort)VirtualKeyCode.C, Width = keyWidth, Height = keyHeight, Focusable = false });
            KeyList.Add(new RepeatButton() { Tag = (ushort)VirtualKeyCode.V, Width = keyWidth, Height = keyHeight, Focusable = false });
            KeyList.Add(new RepeatButton() { Tag = (ushort)VirtualKeyCode.B, Width = keyWidth, Height = keyHeight, Focusable = false });
            KeyList.Add(new RepeatButton() { Tag = (ushort)VirtualKeyCode.N, Width = keyWidth, Height = keyHeight, Focusable = false });
            KeyList.Add(new RepeatButton() { Tag = (ushort)VirtualKeyCode.M, Width = keyWidth, Height = keyHeight, Focusable = false });
            KeyList.Add(new RepeatButton() { Tag = (ushort)VirtualKeyCode.OEMComma, Width = keyWidth, Height = keyHeight, Focusable = false }); //,
            KeyList.Add(new RepeatButton() { Tag = (ushort)VirtualKeyCode.OEMPeriod, Width = keyWidth, Height = keyHeight, Focusable = false }); //.
            KeyList.Add(new RepeatButton() { Tag = (ushort)VirtualKeyCode.OEM2, Width = keyWidth, Height = keyHeight, Focusable = false }); //?
            KeyList.Add(new RepeatButton() { Tag = (ushort)VirtualKeyCode.OEM5, Width = keyWidth, Height = keyHeight, Focusable = false }); //\
            KeyList.Add(new Button() { Tag = (ushort)VirtualKeyCode.A, Content = "Lang", Width = (keyWidth + margin.Left + margin.Right) * 2 - margin.Left - margin.Right
                    , Height = keyHeight, Focusable = false });  //Change language

            KeyList.Add(new Button() { Tag = (ushort)VirtualKeyCode.A, Content = "Symb", Width = (keyWidth + margin.Left + margin.Right) * 2 - margin.Left - margin.Right
                    , Height = keyHeight, Focusable = false });  //Change to numbers
            KeyList.Add(new RepeatButton() { Tag = (ushort)VirtualKeyCode.Space, Width = (keyWidth + margin.Left + margin.Right) * 12 - margin.Left - margin.Right
                    , Height = keyHeight, Focusable = false }); //.
            KeyList.Add(new RepeatButton() { Tag = (ushort)VirtualKeyCode.Left, Width = keyWidth, Height = keyHeight, Focusable = false }); //Left arrow
            KeyList.Add(new RepeatButton() { Tag = (ushort)VirtualKeyCode.Right, Width = keyWidth, Height = keyHeight, Focusable = false }); //Right arrow
        }
    }
}
