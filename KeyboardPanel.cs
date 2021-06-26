using KeyboardPanelLibrary.Enums;
using KeyboardPanelLibrary.Extensions;
using KeyboardPanelLibrary.Extensions.Structs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using VirtualKeyboardWPF.Enums;
using static KeyboardPanelLibrary.Extensions.WinApi;

namespace KeyboardPanelLibrary
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:KeyboardPanelLibrary"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:KeyboardPanelLibrary;assembly=KeyboardPanelLibrary"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class KeyboardPanel : Panel
    {
        static KeyboardPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(KeyboardPanel), new FrameworkPropertyMetadata(typeof(KeyboardPanel)));
        }

        public KeyboardPanel()
        {
            //MainKeyboard = new();
            //Numpad = new();

            //allKeyboards = new KeyboardBase[2];

            //allKeyboards[0] = MainKeyboard;
            //allKeyboards[1] = Numpad;

        }

        //private const int MAPVK_VK_TO_VSC = 0;
        private const UInt32 KLF_SETFORPROCESS = 0x00000100;
        private const double SPACE_BETWEEN_KEYBOARDS = 20;
        private const int ROWS_COUNT = 4;
        private double oneKeyWidth = 0;

        //readonly KeyboardBase[] allKeyboards;
        //public Keyboard MainKeyboard;
        //public Numpad Numpad;
        readonly KeyboardHelper helper = new();
        List<int> rowsWithKeys;

        protected override Size MeasureOverride(Size availableSize)
        {
            rowsWithKeys = GetKeyList();

            Size availSize = availableSize;

            double maxMarginInAllLines = FindFullMargin(rowsWithKeys) + SPACE_BETWEEN_KEYBOARDS * (rowsWithKeys.Count / ROWS_COUNT - 1);

            oneKeyWidth = (availSize.Width - maxMarginInAllLines) / helper.CountMaxKeysInAllRows(rowsWithKeys, ROWS_COUNT, InternalChildren);

            //////Application.Current.MainWindow.MinHeight = (mainKeyboard.Height + mainKeyboard.Margin.Top + mainKeyboard.Margin.Bottom) * 4  + SystemParameters.WindowCaptionHeight;

            SetMinWidth(maxMarginInAllLines, availSize);
            SetNewWidth();
            SetNewHeight(availSize);

            foreach (UIElement child in InternalChildren)
            {
                SetButtonsContent(child);
                child.Measure(availSize);
            }

            return base.MeasureOverride(availSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            ArrangeAllKeys();

            return base.ArrangeOverride(finalSize);
        }

        private List<int> GetKeyList()
        {
            List<int> rowsWithKeys = new();

            //List<List<UIElement>> rowsWithKeys = new();
            //rowsWithKeys.Add(/*List<UIElement>())*/;

            int currentKeyboardLine = 1;
            int keysInOneRow = 0;

            foreach (UIElement child in InternalChildren)
            {
                var keyRowMetadata = KeyboardBase.GetAdditionalMetadataProperty(child).RowLocation;

                if (keyRowMetadata == currentKeyboardLine + 1)
                {
                    rowsWithKeys.Add(keysInOneRow);
                    keysInOneRow = 0;
                    currentKeyboardLine++;
                }

                keysInOneRow++;
            }

            if (keysInOneRow != 0)
            {
                rowsWithKeys.Add(keysInOneRow);
            }

            return rowsWithKeys;
        }

        private double FindFullMargin(List<int> rowsWithKeys)
        {
            double allKeyboardsMargin = 0;
            double oneKeyboardMaxMargin = 0;

            int currentKey = 0;

            for (int i = 0; i < rowsWithKeys.Count; i++)
            {
                    double maxMarginInOneLine = helper.CalculateAllMarginInOneRow(rowsWithKeys[i], 
                            (Thickness)InternalChildren[currentKey].GetValue(MarginProperty), currentKey, InternalChildren );

                    if (maxMarginInOneLine > oneKeyboardMaxMargin)
                    {
                        oneKeyboardMaxMargin = maxMarginInOneLine;
                    }

                    if ((i + 1) % ROWS_COUNT == 0 && i!= 0)
                    {
                        allKeyboardsMargin += maxMarginInOneLine;
                        oneKeyboardMaxMargin = 0;
                    }

                currentKey += rowsWithKeys[i];
            }

            return allKeyboardsMargin;
        }

        private Size SetMinWidth( double maxMarginInAllLines, Size availableSize)
        {
            if (Application.Current.MainWindow.Width < 30 * helper.CountMaxKeysInAllRows(rowsWithKeys, ROWS_COUNT, InternalChildren) + maxMarginInAllLines)
            {
                oneKeyWidth = 30;
                double minAvailSize = 30 * helper.CountMaxKeysInAllRows(rowsWithKeys, ROWS_COUNT, InternalChildren) + maxMarginInAllLines;
                availableSize.Width = minAvailSize;
                Application.Current.MainWindow.MinWidth = minAvailSize;
            }

            return availableSize;
        }

        private void SetNewWidth()
        {
            foreach (UIElement child in InternalChildren)
            {
                var widthCoefficient = KeyboardBase.GetAdditionalMetadataProperty(child).WidthCoefficient;

                child.SetValue(WidthProperty, oneKeyWidth * widthCoefficient
                        + (((Thickness)child.GetValue(MarginProperty)).Right + ((Thickness)child.GetValue(MarginProperty)).Left) * (widthCoefficient - 1));
            }
        }

        private void SetNewHeight(Size availableSize)
        {
            foreach (UIElement child in InternalChildren)
            {
                child.SetValue(HeightProperty, (availableSize.Height - (((Thickness)child.GetValue(MarginProperty)).Top
                            + ((Thickness)child.GetValue(MarginProperty)).Bottom) * ROWS_COUNT) / ROWS_COUNT);
            }
        }

        private void SetButtonsContent(UIElement child)
        {
            if (child is ButtonBase)
            {
                var button = child as ButtonBase;
                ushort virtualKey = KeyboardBase.GetAdditionalMetadataProperty(button).VirtualCode;

                string content = "";

                content = virtualKey switch
                {
                    0x00 => button.Content.ToString(),
                    (ushort)VirtualKeyCode.Tab => "Tab",
                    (ushort)VirtualKeyCode.Shift => "Shift",
                    (ushort)VirtualKeyCode.Back => "Backspace",
                    (ushort)VirtualKeyCode.Return => "Enter",
                    (ushort)VirtualKeyCode.Space => "Space",
                    (ushort)VirtualKeyCode.Left => "<",
                    (ushort)VirtualKeyCode.Right => ">",
                    _ => GetCharsFromKeys((VirtualKeyCode)virtualKey, false, false),
                };

                button.Content = content;
            }
        }

        private string GetCharsFromKeys(VirtualKeyCode keys, bool shift, bool altGr)
        {
            var buf = new StringBuilder(256);
            var keyboardState = new byte[256];
            if (shift)
                keyboardState[(int)VirtualKeyCode.Shift] = 0xff;
            if (altGr)
            {
                keyboardState[(int)VirtualKeyCode.Control] = 0xff;
                keyboardState[(int)VirtualKeyCode.Menu] = 0xff;
            }
            WinApi.ToUnicode((uint)keys, 0, keyboardState, buf, 256, 0);
            return buf.ToString();
        }

        private void ArrangeAllKeys()
        {
            double currentHeightShift = 0.0;
            double currentWidthShift = 0.0;

            double previousKeyboardMaxWidth = 0;

            int currentKeyboard = 0;
            int currentKey = 0;
            int currentKeyInLine = 0;

            for (int k = 0; k < rowsWithKeys.Count; k++)
            {
                for (int i = 0; i < rowsWithKeys[k]; i++)
                {
                    double shiftFromMaxWidth = 0;

                    if (helper.CountMaxKeysInKeyboard(rowsWithKeys, currentKeyboard, ROWS_COUNT, InternalChildren) 
                            - helper.CountMaxKeysInOneRow(rowsWithKeys[k], currentKeyInLine, InternalChildren) != 0)
                    {
                        Thickness margin = (Thickness)InternalChildren[currentKey].GetValue(MarginProperty);
                        shiftFromMaxWidth = (oneKeyWidth + margin.Left+ margin.Right)
                           * (helper.CountMaxKeysInKeyboard(rowsWithKeys, currentKeyboard, ROWS_COUNT, InternalChildren) 
                            - helper.CountMaxKeysInOneRow(rowsWithKeys[k], currentKeyInLine, InternalChildren)) / 2;
                    }

                    InternalChildren[currentKey].Arrange(new Rect(new Point(currentWidthShift + shiftFromMaxWidth, currentHeightShift), InternalChildren[currentKey].DesiredSize));

                    currentWidthShift += InternalChildren[currentKey].DesiredSize.Width;

                    if (k != rowsWithKeys.Count - 1 || i != rowsWithKeys[k] - 1)
                    {
                        currentKey++;
                    }
                }

                currentWidthShift = previousKeyboardMaxWidth;
                currentHeightShift += InternalChildren[currentKey].DesiredSize.Height;

                if ((k + 1) % ROWS_COUNT == 0 && (k + 1) != 0)
                {
                    double currentKeyboardMaxWidth = oneKeyWidth * helper.CountMaxKeysInKeyboard(rowsWithKeys, currentKeyboard, ROWS_COUNT, InternalChildren)
                    + helper.CalculateAllMarginInKeyboard(rowsWithKeys, currentKeyboard, ROWS_COUNT, InternalChildren, (Thickness)InternalChildren[currentKey].GetValue(MarginProperty));

                    previousKeyboardMaxWidth += currentKeyboardMaxWidth + SPACE_BETWEEN_KEYBOARDS;
                    currentHeightShift = 0;

                    currentWidthShift = previousKeyboardMaxWidth;
                    currentKeyboard++;
                }

                currentKeyInLine += rowsWithKeys[k];
            }
        }
    }

    //protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
    //{
    //    if (visualAdded != null)
    //    {
    //        //visualAdded.SetValue(MarginProperty, KeyMargin);

    //        if (visualAdded is ButtonBase)
    //        {
    //            var button = visualAdded as ButtonBase;
    //            button.Click += VirtualKeyPress;
    //        }
    //        else if(visualAdded is ComboBox)
    //        {
    //            var comboBox = visualAdded as ComboBox;
    //            comboBox.SelectionChanged += ChangeLanguage;
    //        }
    //    }

    //    base.OnVisualChildrenChanged(visualAdded, visualRemoved);
    //}

    //public void Send(VirtualKeyCode virtualKey)
    //{
    //    INPUT[] Inputs = new INPUT[1];
    //    INPUT Input = new INPUT();
    //    Input.type = 1; // 1 = Keyboard Input
    //    Input.inputUinion.ki.wVk = virtualKey;
    //    Input.inputUinion.ki.dwFlags = KEYEVENTF.KEYDOWN;
    //    Inputs[0] = Input;
    //    SendInput(1, Inputs, INPUT.Size);
    //}

    //private void VirtualKeyPress(object sender, RoutedEventArgs e)
    //{
    //    var button = (ButtonBase)sender;
    //    ushort virtualKey = KeyboardBase.GetAdditionalMetadataProperty(button).VirtualCode;

    //   // int scanCode =  WinApi.MapVirtualKey(virtualKey, MAPVK_VK_TO_VSC);
    //    Send((VirtualKeyCode)virtualKey/*(ushort)scanCode*/);
    //}

    //private void ChangeLanguage(object sender, RoutedEventArgs e)
    //{
    //    var comboBox = (ComboBox)sender;
    //    CultureInfo languageInfo = new CultureInfo((UInt16)((ComboBoxItem)comboBox.SelectedItem).Tag, false);

    //    WinApi.ActivateKeyboardLayout((IntPtr)languageInfo.KeyboardLayoutId, KLF_SETFORPROCESS);

    //    foreach (UIElement child in InternalChildren)
    //    {
    //        SetButtonsContent(child);
    //    }
    //}

    //}
}
