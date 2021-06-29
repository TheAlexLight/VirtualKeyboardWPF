using KeyboardPanelLibrary.AdditionalMetadata;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
            //BackgroundProperty.OverrideMetadata(typeof(KeyboardPanel), new FrameworkPropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#bbbbbb"))));


DefaultStyleKeyProperty.OverrideMetadata(typeof(KeyboardPanel), new FrameworkPropertyMetadata(typeof(KeyboardPanel)));
        }

        public KeyboardPanel()
        {
            this.Focusable = false;
        }

        private const UInt32 KLF_SETFORPROCESS = 0x00000100;
        private const double SPACE_BETWEEN_KEYBOARDS = 20;
        private const int ROWS_COUNT = 4;
        private double oneKeyWidth = 0;

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

            int currentKeyboardLine = 1;
            int keysInOneRow = 0;

            foreach (UIElement child in InternalChildren)
            {
                var keyRowMetadata = Keyboard.GetAdditionalMetadataProperty(child).RowLocation;

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
                        (Thickness)InternalChildren[currentKey].GetValue(MarginProperty), currentKey, InternalChildren);

                if (maxMarginInOneLine > oneKeyboardMaxMargin)
                {
                    oneKeyboardMaxMargin = maxMarginInOneLine;
                }

                if ((i + 1) % ROWS_COUNT == 0 && i != 0)
                {
                    allKeyboardsMargin += oneKeyboardMaxMargin;
                    oneKeyboardMaxMargin = 0;
                }

                currentKey += rowsWithKeys[i];
            }

            return allKeyboardsMargin;
        }

        private Size SetMinWidth(double maxMarginInAllLines, Size availableSize)
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
                var widthCoefficient = Keyboard.GetAdditionalMetadataProperty(child).WidthCoefficient;

                child.SetValue(WidthProperty, oneKeyWidth * widthCoefficient
                        + (((Thickness)child.GetValue(MarginProperty)).Right + ((Thickness)child.GetValue(MarginProperty)).Left) * (widthCoefficient - 1));
            }
        }

        private void SetNewHeight(Size availableSize)
        {
            foreach (UIElement child in InternalChildren)
            {
                if (!double.IsInfinity(availableSize.Height) && availableSize.Height > 61.5 * ROWS_COUNT)
                {
                    child.SetValue(HeightProperty, (availableSize.Height - (((Thickness)child.GetValue(MarginProperty)).Top
                           + ((Thickness)child.GetValue(MarginProperty)).Bottom) * ROWS_COUNT) / ROWS_COUNT);
                }
                else
                {
                    double heightValue = (250 - (((Thickness)child.GetValue(MarginProperty)).Top
                         + ((Thickness)child.GetValue(MarginProperty)).Bottom) * ROWS_COUNT) / ROWS_COUNT;

                    child.SetValue(HeightProperty, heightValue);
                    Application.Current.MainWindow.MinHeight = SystemParameters.WindowCaptionHeight + heightValue;
                }
               
            }
        }

        private void SetButtonsContent(UIElement child)
        {
            if (child is ButtonBase)
            {
                var button = child as ButtonBase;
                ushort virtualKey = Keyboard.GetAdditionalMetadataProperty(button).VirtualCode;

                string content = "";

                //bool shiftIsActive = false;

                //if (virtualKey == (ushort)VirtualKeyCode.Shift)
                //{
                //    shiftIsActive = (Keyboard.GetAdditionalMetadataProperty(child) as ShiftAdditionalMetadata).IsActive;
                //}

                Image img = new Image();

                img.Width = 40;
                img.Height = 40;

                img.Source = virtualKey switch
                {
                    (ushort)VirtualKeyCode.Tab => Application.Current.FindResource("TabDrawingImage") as DrawingImage,
                    (ushort)VirtualKeyCode.Shift => Application.Current.FindResource("ShiftDrawingImage") as DrawingImage,
                    (ushort)VirtualKeyCode.Back => Application.Current.FindResource("BackspaceDrawingImage") as DrawingImage,
                    (ushort)VirtualKeyCode.Return => Application.Current.FindResource("EnterDrawingImage") as DrawingImage,
                    (ushort)VirtualKeyCode.Left => Application.Current.FindResource("LeftArrowDrawingImage") as DrawingImage,
                    (ushort)VirtualKeyCode.Right => Application.Current.FindResource("RightArrowDrawingImage") as DrawingImage,
                    _ => null,
                };

                if (img.Source == null )
                {
                    if (virtualKey == 0x00)
                    {
                        button.Content = button.Content.ToString();
                    }
                    else
                    {
                        button.Content = GetCharsFromKeys((VirtualKeyCode)virtualKey, Keyboard.ShiftIsActive, false);
                    }
                }
                else
                {
                    if (virtualKey == (ushort)VirtualKeyCode.Shift && Keyboard.ShiftIsActive)
                    {
                        img.Source = Application.Current.FindResource("ShiftDownDrawingImage") as DrawingImage;
                    }

                    button.Content = img;
                }
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
                        shiftFromMaxWidth = (oneKeyWidth + margin.Left + margin.Right)
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

        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            if (visualAdded != null)
            {
                if (visualAdded is ButtonBase)
                {
                    var button = visualAdded as ButtonBase;

                    ushort virtualKeycode = Keyboard.GetAdditionalMetadataProperty(button).VirtualCode;

                    if (virtualKeycode == (ushort)VirtualKeyCode.Shift)
                    {
                        button.Click += ShiftKeyPress;
                    }
                    else
                    {
                        button.Click += VirtualKeyPress;
                    }
                }
                else if (visualAdded is ComboBox)
                {
                    var comboBox = visualAdded as ComboBox;
                    comboBox.SelectionChanged += ChangeLanguage;
                }
            }

            base.OnVisualChildrenChanged(visualAdded, visualRemoved);
        }

        public void Send(VirtualKeyCode virtualKey, KEYEVENTF keyFlag)
        {
            INPUT[] Inputs = new INPUT[1];
            INPUT Input = new INPUT();
            Input.type = 1; // 1 = Keyboard Input
            Input.inputUinion.ki.wVk = virtualKey;
            Input.inputUinion.ki.dwFlags = keyFlag;
            Inputs[0] = Input;
            SendInput(1, Inputs, INPUT.Size);
        }

        private void VirtualKeyPress(object sender, RoutedEventArgs e)
        {
            var button = (ButtonBase)sender;
            ushort virtualKey = Keyboard.GetAdditionalMetadataProperty(button).VirtualCode;

            // int scanCode =  WinApi.MapVirtualKey(virtualKey, MAPVK_VK_TO_VSC);
            Send((VirtualKeyCode)virtualKey, KEYEVENTF.KEYDOWN/*(ushort)scanCode*/);
        }

        private void ShiftKeyPress(object sender, RoutedEventArgs e)
        {
            var button = (ButtonBase)sender;
            ushort virtualKey = Keyboard.GetAdditionalMetadataProperty(button).VirtualCode;

            //var shiftAdditionalMatadata = Keyboard.GetAdditionalMetadataProperty(button) as ShiftAdditionalMetadata;
            //var isActive = shiftAdditionalMatadata.IsActive;

            KEYEVENTF keyFlag = KEYEVENTF.KEYDOWN;

            if (Keyboard.ShiftIsActive)
            {
                keyFlag = KEYEVENTF.KEYUP;
                Keyboard.ShiftIsActive = false;
            }
            else
            {
                Keyboard.ShiftIsActive = true;
            }

            Send((VirtualKeyCode)virtualKey, keyFlag);

            int currentKey = 0;

            for (int i = 0; i < ROWS_COUNT; i++)
            {
                for (int j = 0; j < rowsWithKeys[i]; j++)
                {
                    SetButtonsContent(InternalChildren[currentKey]);
                    currentKey++;
                }
            }

                
        }

        private void ChangeLanguage(object sender, RoutedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            CultureInfo languageInfo = new CultureInfo((UInt16)((ComboBoxItem)comboBox.SelectedItem).Tag, false);

            WinApi.ActivateKeyboardLayout((IntPtr)languageInfo.KeyboardLayoutId, KLF_SETFORPROCESS);

            foreach (UIElement child in InternalChildren)
            {
                SetButtonsContent(child);
            }
        }
    }
}
