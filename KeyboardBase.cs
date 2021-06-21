﻿using System;
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
    public abstract class KeyboardBase: Control
    {
        static KeyboardBase()
        {
            MarginProperty.OverrideMetadata(typeof(KeyboardBase), new FrameworkPropertyMetadata(new Thickness(2)));
            HeightProperty.OverrideMetadata(typeof(KeyboardBase), new FrameworkPropertyMetadata(60.0));
            WidthProperty.OverrideMetadata(typeof(KeyboardBase), new FrameworkPropertyMetadata(60.0));
        }

        public virtual List<UIElement> KeyList { get; set; }
        public virtual int[] KeysInRow { get; set; }
        public virtual int MaxAmountOfKeys { get => KeysInRow.Max(); }

        public static readonly DependencyProperty AdditionalMetadataProperty
        = DependencyProperty.RegisterAttached("SetAdditionalMetadata", typeof(KeyboardAdditionalMetadata), typeof(KeyboardBase), new PropertyMetadata());

        public static void SetAdditionalMetadataProperty(DependencyObject obj, KeyboardAdditionalMetadata value)
        {
            obj.SetValue(AdditionalMetadataProperty, value);
        }

        public static KeyboardAdditionalMetadata GetAdditionalMetadataProperty(DependencyObject obj)
        {
            return (KeyboardAdditionalMetadata)obj.GetValue(AdditionalMetadataProperty);
        }

        protected abstract void FillKeyList();

        protected virtual UIElement SetOneKey(ButtonBase buttonType, double keyWidth, Thickness margin, VirtualKeyCode virtualKey, double widthCoefficient)
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

        public virtual double CalculateAllMargin(int row)
        {
            double allMargin = 0;

            int currentKey = FindSearchedLine(row);
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
        public virtual double CalculateAllMarginInKeyboard()
        {
            double maxAmount = 0;
            int currentKey = 0;

            for (int i = 0; i < KeysInRow.Length; i++)
            {
                double oneLineMaxCount = 0;
                for (int j = 0; j < KeysInRow[i]; j++)
                {
                    Thickness baseMargin = (Thickness)this.GetValue(MarginProperty);
                    Thickness currentKeyMargin = (Thickness)KeyList[currentKey].GetValue(MarginProperty);
                    double widthCoefficient = GetAdditionalMetadataProperty(KeyList[currentKey]).WidthCoefficient;

                    oneLineMaxCount += (baseMargin.Left + baseMargin.Right) * (widthCoefficient - 1) + currentKeyMargin.Left + currentKeyMargin.Right;
                    currentKey++;
                }

                if (oneLineMaxCount > maxAmount)
                {
                    maxAmount = oneLineMaxCount;
                }
            }

            return maxAmount;
        }

        private int FindSearchedLine(int row)
        {
            int startRow = 0;
            int currentKey = 0;

            while (startRow != row)
            {
                currentKey += KeysInRow[startRow];
                startRow++;
            }

            return currentKey;
        }

        public virtual double CountMaxAmountOfKeys()
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
                    maxAmount = oneLineMaxCount;
                }
            }

            return maxAmount;
        }

        public virtual double CountAmountOfKeysInOneRow(int row)
        {
            double amountOfKeys = 0;

            int currentKey = FindSearchedLine(row);
            int loopStart = currentKey;

            for (int j = loopStart; j < loopStart + KeysInRow[row]; j++)
            {
                amountOfKeys += GetAdditionalMetadataProperty(KeyList[currentKey]).WidthCoefficient;
                currentKey++;
            }

            return amountOfKeys;
        }
    }
}
