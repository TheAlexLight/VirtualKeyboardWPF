using KeyboardPanelLibrary.Enums;
using KeyboardPanelLibrary.Extensions;
using KeyboardPanelLibrary.Extensions.Structs;
using System;
using System.Collections.Generic;
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
            MainKeyboard = new();
            Numpad = new();

            allKeyboards = new KeyboardBase[2];

            allKeyboards[0] = MainKeyboard;
            allKeyboards[1] = Numpad;
        }

        private const int MAPVK_VK_TO_VSC = 0;
        private double oneKeyWidth = 0;

        readonly KeyboardBase[] allKeyboards;
        public Keyboard MainKeyboard;
        public Numpad Numpad;

        protected override Size MeasureOverride(Size availableSize)
        {
            Size availSize = availableSize;

            double maxMarginInAllLines = FindFullMargin(allKeyboards) + 20;

            oneKeyWidth = ( availableSize.Width - maxMarginInAllLines) / CountMaxAmountOfAllKeys();

            //Application.Current.MainWindow.MinHeight = (mainKeyboard.Height + mainKeyboard.Margin.Top + mainKeyboard.Margin.Bottom) * 4  + SystemParameters.WindowCaptionHeight;

            SetMinWidth(maxMarginInAllLines, availSize);
            SetNewWidth();

            foreach (UIElement child in InternalChildren)
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

                child.Measure(availableSize);
            }

            return base.MeasureOverride(availableSize);
        }
        protected override Size ArrangeOverride(Size finalSize)
        {
            ArrangeAllKeys();

            return base.ArrangeOverride(finalSize);
        }

        private double FindFullMargin(KeyboardBase[] allKeyboards)
        {
            double allKeyboardsMaxMargin = 0;

            for (int i = 0; i < allKeyboards.Length; i++)
            {
                double oneKeyboardMaxMargin = 0;

                for (int j = 0; j < allKeyboards[i].KeysInRow.Length; j++)
                {
                    double maxMarginInOneLine = allKeyboards[i].CalculateAllMargin(j);

                    if (maxMarginInOneLine > oneKeyboardMaxMargin)
                    {
                        oneKeyboardMaxMargin = maxMarginInOneLine;
                    }
                }

                allKeyboardsMaxMargin += oneKeyboardMaxMargin;
            }

            return allKeyboardsMaxMargin;
        }

        private int FindMaxKeysNumberInAllKeyboards(KeyboardBase[] allKeyboards)
        {
            int maxAmountOfAllKeys = 0;

            for (int i = 0; i < allKeyboards.Length; i++)
            {
                maxAmountOfAllKeys += allKeyboards[i].MaxAmountOfKeys;
            }

            return maxAmountOfAllKeys;
        }

        private Size SetMinWidth(double maxMarginInAllLines, Size availableSize)
        {
            if (Application.Current.MainWindow.Width < 30 * FindMaxKeysNumberInAllKeyboards(allKeyboards) + maxMarginInAllLines)
            {
                oneKeyWidth = 30;
                double minAvailSize = 30 * FindMaxKeysNumberInAllKeyboards(allKeyboards) + maxMarginInAllLines;
                availableSize.Width = minAvailSize;
                Application.Current.MainWindow.MinWidth = minAvailSize;
            }

            return availableSize;
        }

        private void SetNewWidth()
        {
            int currentKey = 0;

            for (int i = 0; i < allKeyboards.Length; i++)
            {
                for (int k = 0; k < allKeyboards[i].KeysInRow.Length; k++)
                {
                    for (int j = 0; j < allKeyboards[i].KeysInRow[k]; j++)
                    {
                        var widthCoefficient = KeyboardBase.GetAdditionalMetadataProperty(InternalChildren[currentKey]).WidthCoefficient;
                        InternalChildren[currentKey].SetValue(WidthProperty, oneKeyWidth * widthCoefficient + 
                                (allKeyboards[i].Margin.Left + allKeyboards[i].Margin.Right) * (widthCoefficient - 1));
                        currentKey++;
                    }
                }
            }
        }

        private double CountMaxAmountOfAllKeys()
        {
            double maxAmountOfAllKeys = 0;

            for (int i = 0; i < allKeyboards.Length; i++)
            {
                maxAmountOfAllKeys += allKeyboards[i].CountMaxAmountOfKeys();
            }

            return maxAmountOfAllKeys;
        }

        private void ArrangeAllKeys()
        {
            double currentHeightShift = 0.0;
            double currentWidthShift = 0.0;

            double previousKeyboardMaxWidth = 0;

            int currentKey = 0;

            for (int k = 0; k < allKeyboards.Length; k++)
            {
                for (int i = 0; i < allKeyboards[k].KeysInRow.Length; i++)
                {
                    for (int j = 0; j < allKeyboards[k].KeysInRow[i]; j++)
                    {
                        InternalChildren[currentKey].Arrange(new Rect(new Point(currentWidthShift, currentHeightShift), InternalChildren[currentKey].DesiredSize));

                        currentWidthShift += InternalChildren[currentKey].DesiredSize.Width;
                        if ( k != allKeyboards.Length - 1 || i != allKeyboards[k].KeysInRow.Length - 1 || j != allKeyboards[k].KeysInRow[i] - 1)
                        {
                            currentKey++;
                        }
                    }

                    if (i == allKeyboards[k].KeysInRow.Length - 1)
                    {
                        double currentKeyboardMaxWidth = oneKeyWidth * allKeyboards[k].CountMaxAmountOfKeys()
                        + allKeyboards[k].CalculateAllMarginInKeyboard();

                        previousKeyboardMaxWidth += currentKeyboardMaxWidth + 20;
                        
                    }

                    currentWidthShift = previousKeyboardMaxWidth;

                    currentHeightShift += InternalChildren[currentKey].DesiredSize.Height;
                }

                currentHeightShift = 0;
            }
        }

        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            if (visualAdded != null)
            {
                //visualAdded.SetValue(MarginProperty, KeyMargin);

                if (visualAdded is ButtonBase)
                {
                    var button = visualAdded as ButtonBase;
                    button.Click += VirtualKeyPress;
                }
            }

            base.OnVisualChildrenChanged(visualAdded, visualRemoved);
        }

        public void Send(VirtualKeyCode virtualKey)
        {
            INPUT[] Inputs = new INPUT[1];
            INPUT Input = new INPUT();
            Input.type = 1; // 1 = Keyboard Input
            Input.inputUinion.ki.wVk = virtualKey;
            Input.inputUinion.ki.dwFlags = KEYEVENTF.KEYDOWN;
            Inputs[0] = Input;
            SendInput(1, Inputs, INPUT.Size);
        }
        
        private void VirtualKeyPress(object sender, RoutedEventArgs e)
        {
            var button = (ButtonBase)sender;
            ushort virtualKey = KeyboardBase.GetAdditionalMetadataProperty(button).VirtualCode;

           // int scanCode =  WinApi.MapVirtualKey(virtualKey, MAPVK_VK_TO_VSC);
            Send((VirtualKeyCode)virtualKey/*(ushort)scanCode*/);
        }

        static string GetCharsFromKeys(VirtualKeyCode keys, bool shift, bool altGr)
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
    }
}
