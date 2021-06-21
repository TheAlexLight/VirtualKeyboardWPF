using KeyboardPanelLibrary.Extensions;
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
            mainKeyboard = new();
        }

        private const int MAPVK_VK_TO_VSC = 0;
        private double oneKeyWidth = 0;

        public Keyboard mainKeyboard;

        protected override Size MeasureOverride(Size availableSize)
        {
            Size availSize = availableSize;

            double maxMarginInAllLines = 0;

            for (int i = 0; i < 4; i++)
            {
                double maxMarginInOneLine = mainKeyboard.CalculateAllMargin(i);

                if (maxMarginInOneLine > maxMarginInAllLines)
                {
                    maxMarginInAllLines = maxMarginInOneLine;
                }  
            }

            

            oneKeyWidth = ( availableSize.Width - maxMarginInAllLines) / mainKeyboard.CountMaxAmountOfKeys() /*mainKeyboard.MaxAmountOfKeys*/;
            // oneKeyWidth = (mainKeyboard.FullWidth + maxMarginInAllLines) / mainKeyboard.MaxAmountOfKeys;

            //Application.Current.MainWindow.MinHeight = (mainKeyboard.Height + mainKeyboard.Margin.Top + mainKeyboard.Margin.Bottom) * 4  + SystemParameters.WindowCaptionHeight;

            if (Application.Current.MainWindow.Width < 30 * mainKeyboard.MaxAmountOfKeys + maxMarginInAllLines)
            {
                oneKeyWidth = 30;
                double minAvailSize = 30 * mainKeyboard.MaxAmountOfKeys + maxMarginInAllLines;
                availSize.Width = minAvailSize;
                Application.Current.MainWindow.MinWidth = minAvailSize;
            }

            foreach (UIElement child in InternalChildren)
            {
                var widthCoefficient = KeyboardBase.GetAdditionalMetadataProperty(child).WidthCoefficient;
                child.SetValue(WidthProperty, oneKeyWidth * widthCoefficient + (mainKeyboard.Margin.Left + mainKeyboard.Margin.Right) * (widthCoefficient - 1));
            }

            foreach (UIElement child in InternalChildren)
            {
                if (child is ButtonBase)
                {
                    var button = child as ButtonBase;
                    ushort virtualKey = KeyboardBase.GetAdditionalMetadataProperty(button).VirtualCode;
                    if (virtualKey != 0)
                    {
                        button.Content = GetCharsFromKeys((VirtualKeyCode)virtualKey, false, false);
                    }
                }

                child.Measure(availableSize);
            }

            return base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double currentHeightShift = 0.0;
            double currentWidthShift = 0.0;
            // double maxWidth = (KeyWidth + KeyMargin.Left + KeyMargin.Right) * 16;

            int currentRow = 0;

            foreach (UIElement child in InternalChildren)
            {
                if ((int)(currentWidthShift + child.DesiredSize.Width) > oneKeyWidth * mainKeyboard.CountAmountOfKeysInOneRow(currentRow) /*mainKeyboard.CountMaxAmountOfKeys()*/ + mainKeyboard.CalculateAllMargin(currentRow))
                {
                    currentWidthShift = 0;
                    currentHeightShift += child.DesiredSize.Height;
                    currentRow++;
                }

                child.Arrange(new Rect(new Point(currentWidthShift, currentHeightShift), child.DesiredSize));

                currentWidthShift += child.DesiredSize.Width;
            }

            return base.ArrangeOverride(finalSize);
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

        public void Send(ushort scan)
        {
            INPUT[] Inputs = new INPUT[1];
            INPUT Input = new INPUT();
            Input.type = 1; // 1 = Keyboard Input
            Input.U.ki.wScan = scan;
            Input.U.ki.dwFlags = KEYEVENTF.SCANCODE;
            Inputs[0] = Input;
            SendInput(1, Inputs, INPUT.Size);
        }
        
        private void VirtualKeyPress(object sender, RoutedEventArgs e)
        {
            var button = (ButtonBase)sender;
            ushort virtualKey = KeyboardBase.GetAdditionalMetadataProperty(button).VirtualCode;

            int scanCode =  WinApi.MapVirtualKey(virtualKey, MAPVK_VK_TO_VSC);
            Send((ushort)scanCode);
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
