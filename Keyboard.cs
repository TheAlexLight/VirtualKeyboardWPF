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
        static Keyboard()
        {
            MarginProperty.OverrideMetadata(typeof(Keyboard), new FrameworkPropertyMetadata(new Thickness(2)));
            HeightProperty.OverrideMetadata(typeof(Keyboard), new FrameworkPropertyMetadata(60.0));
        }

        public Keyboard()
        {

        }
        public Keyboard(double keyWidth, double keyHeight)
        {
            KeyList = new();

            KeysInRow = new int[4];

            FillKeyList(keyWidth, keyHeight);
        }


        public override double FullWidth { get => CalculateKeyboardWidth(); }

        public override List<UIElement> KeyList { get; set; }
        public override int[] KeysInRow { get; set; }
        public int MaxAmountOfKeys { get => KeysInRow.Max(); }

        private void FillKeyList(double keyWidth, double keyHeight)
        {

            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.Tab, 2));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.Q, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.W, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.E, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.R, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.T, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.Y, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.U, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.I, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.O, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.P, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.OEM4, 1)); //[
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.OEM6, 1)); //]
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.Back, 2)); //Backspace 

            KeysInRow[0] = 14;

            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.A, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.S, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.D, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.F, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.G, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.H, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.J, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.K, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.L, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.OEM1, 1)); //;
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.OEM7, 1)); //'
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.Return, 5)); //Enter


            KeysInRow[1] = 12;

            KeyList.Add(SetOneKey(new ToggleButton(), keyWidth, Margin, VirtualKeyCode.Shift, 3));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.Z, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.X, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.C, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.V, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.B, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.N, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.M, 1));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.OEMComma, 1)); //,
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.OEMPeriod, 1)); //.
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.OEM2, 1)); //?
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.OEM5, 1)); //\
            KeyList.Add(SetOneKey(new Button() { Content = "Lang" }, keyWidth, Margin, 0, 1)); //Change language

            KeysInRow[2] = 13;

            KeyList.Add(SetOneKey(new Button() { Content = "Symb" }, keyWidth, Margin, 0, 2)); //Change to numbers
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.Space, 12));
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.Left, 1)); //Left arrow
            KeyList.Add(SetOneKey(new RepeatButton(), keyWidth, Margin, VirtualKeyCode.Right, 1)); //Right arrow

            KeysInRow[3] = 4;

            foreach (var key in KeyList)
            {
                key.SetValue(HeightProperty, keyHeight);
            }
        }

        private UIElement SetOneKey(ButtonBase buttonType, double keyWidth, Thickness margin, VirtualKeyCode virtualKey, double widthCoefficient)
        {

            buttonType.Margin = margin;
            buttonType.Focusable = false;

            KeyboardAdditionalMetadata additionalMetadata = new();
            additionalMetadata.VirtualCode = (ushort)virtualKey;
            additionalMetadata.WidthCoefficient = widthCoefficient;

            SetAdditionalMetadataProperty(buttonType, additionalMetadata);

            buttonType.SetValue(WidthProperty, keyWidth * GetAdditionalMetadataProperty(buttonType).WidthCoefficient);

            return buttonType;
        }

        private double CalculateKeyboardWidth()
        {
            double maxWidth = 0;
            int currentKey = 0;

            for (int i = 0; i < KeysInRow.Length; i++)
            {
                double oneLineMaxWidth = 0;
                for (int j = 0; j < KeysInRow[i]; j++)
                {
                    oneLineMaxWidth += (double)KeyList[currentKey].GetValue(WidthProperty);
                    currentKey++;
                }

                if (oneLineMaxWidth > maxWidth)
                {
                    maxWidth = oneLineMaxWidth;
                }
            }

            return maxWidth;
        }

        public double CalculateAllMargin(int row)
        {
            double allMargin = 0;
            int currentKey = 0;

            int startRow = 0;

            while (startRow != row)
            {
                currentKey += KeysInRow[startRow];
                startRow++;
            }

            int loopStart = currentKey;

            for (int j = loopStart; j < loopStart + KeysInRow[row]; j++)
            {
                Thickness baseMargin = (Thickness)this.GetValue(MarginProperty);
                Thickness currentKeyMargin = (Thickness)KeyList[currentKey].GetValue(MarginProperty);
                double widthCoefficient = GetAdditionalMetadataProperty(KeyList[currentKey]).WidthCoefficient;

                allMargin += (baseMargin.Left + baseMargin.Right) * (widthCoefficient - 1) + currentKeyMargin.Left + currentKeyMargin.Right;
                currentKey++;
            }

            return allMargin;
        }
        
        public double CountMaxAmountOfKeys()
        {
            double maxAmount = 0;
            int currentKey = 0;

            for (int i = 0; i < KeysInRow.Length; i++)
            {
                double oneLineMaxCount = 0;
                for (int j = 0; j < KeysInRow[i]; j++)
                {
                    oneLineMaxCount += GetAdditionalMetadataProperty(KeyList[currentKey]).WidthCoefficient;
                    currentKey++;
                }

                if (oneLineMaxCount > maxAmount)
                {
                    maxAmount = (int)oneLineMaxCount;
                }
            }

            return maxAmount;
        }

        public double CountAmountOfKeysInOneRow(int row)
        {
            double amountOfKeys = 0;
            int currentKey = 0;
            int startRow = 0;

            while (startRow != row)
            {
                currentKey += KeysInRow[startRow];
                startRow++;
            }

            int loopStart = currentKey;

            for (int j = loopStart; j < loopStart + KeysInRow[row]; j++)
            {
                amountOfKeys += GetAdditionalMetadataProperty(KeyList[currentKey]).WidthCoefficient;
                currentKey++;
            }

            return amountOfKeys;
        }

        //public override void BeginInit()
        //{
        //    base.BeginInit();

        //    KeyList = new();
        //    KeysInRow = new int[4];
        //    FillKeyList(60,60,new Thickness(2));
        //}
    }
}
