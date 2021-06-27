using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KeyboardPanelLibrary
{
    class KeyboardHelper
    {
        public double CalculateAllMarginInOneRow(int oneRowKeys, Thickness keysMargin, int startKey, UIElementCollection internalChildren)
        {
            double allMargin = 0;

            for (int j = 0; j < oneRowKeys; j++)
            {
                double widthCoefficient = Keyboard.GetAdditionalMetadataProperty(internalChildren[startKey]).WidthCoefficient;

                allMargin += (keysMargin.Left + keysMargin.Right) * (widthCoefficient);

                startKey++;
            }

            return allMargin;
        }


        public double CalculateAllMarginInKeyboard(List<int> rowsWithKeys, int keyboardNumber, int rowsCount, UIElementCollection internalChildren, Thickness margin)
        {
            double maxMarginInKeyboard = 0;
            int currentKey = 0;

            for (int i = 0; i < keyboardNumber * rowsCount; i++)
            {
                currentKey += rowsWithKeys[i];
            }

            for (int i = keyboardNumber * rowsCount; i < keyboardNumber * rowsCount + rowsCount; i++)
            {
               double maxMarginInOneRow = CalculateAllMarginInOneRow(rowsWithKeys[i], margin, currentKey, internalChildren);

                if (maxMarginInOneRow > maxMarginInKeyboard)
                {
                    maxMarginInKeyboard = maxMarginInOneRow;
                }


                currentKey += rowsWithKeys[i];
            }

            return maxMarginInKeyboard;
        }

        public double CountMaxKeysInAllRows(List<int> rowsWithKeys, int rowsCount, UIElementCollection internalChildren)
        {
            double maxKeysInOneKeyboard = 0;
            double maxKeysInAllKeyboards = 0;

            int currentKey = 0;

            for (int i = 0; i < rowsWithKeys.Count; i++)
            {
                double maxKeysInOneRow = CountMaxKeysInOneRow(rowsWithKeys[i], currentKey, internalChildren);

                if (maxKeysInOneRow > maxKeysInOneKeyboard)
                {
                    maxKeysInOneKeyboard = maxKeysInOneRow;
                }

                if ((i + 1) % rowsCount == 0 && i != 0)
                {
                    maxKeysInAllKeyboards += maxKeysInOneKeyboard;
                    maxKeysInOneKeyboard = 0;
                }

                currentKey +=  rowsWithKeys[i];
            }

            return maxKeysInAllKeyboards;
        }

        public double CountMaxKeysInOneRow(int oneRowKeys, int startKey, UIElementCollection internalChildren)
        {
            double keyCount = 0;

            int currentKey = startKey;

            for (int i = 0; i < oneRowKeys; i++)
            {
                keyCount += Keyboard.GetAdditionalMetadataProperty(internalChildren[currentKey]).WidthCoefficient;
                currentKey++;
            }

            return keyCount;
        }

        public double CountMaxKeysInKeyboard(List<int> rowsWithKeys, int keyboardNumber, int rowsCount, UIElementCollection internalChildren)
        {
            double maxKeysInKeyboard = 0;
            int currentKey = 0;

            for (int i = 0; i < keyboardNumber * rowsCount; i++)
            {
                currentKey += rowsWithKeys[i];
            }

            for (int i = keyboardNumber * rowsCount; i < keyboardNumber * rowsCount + rowsCount; i++)
            {
                double maxKeysInOneRow = CountMaxKeysInOneRow(rowsWithKeys[i], currentKey, internalChildren);

                if (maxKeysInOneRow > maxKeysInKeyboard)
                {
                    maxKeysInKeyboard = maxKeysInOneRow;
                }

                currentKey += rowsWithKeys[i];
            }

            return maxKeysInKeyboard;
        }
    }
}
