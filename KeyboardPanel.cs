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

            KeyWidthProperty = DependencyProperty.Register(nameof(KeyWidth), typeof(double), typeof(KeyboardPanel)
                    , new PropertyMetadata(60.0));
           KeyHeightProperty = DependencyProperty.Register(nameof(KeyHeight), typeof(double), typeof(KeyboardPanel)
                    , new PropertyMetadata(60.0));
            KeyMarginProperty = DependencyProperty.Register(nameof(KeyMargin), typeof(Thickness), typeof(KeyboardPanel)
                    , new PropertyMetadata(new Thickness(2.0)));
        }

        public KeyboardPanel()
        {
            MainKeyboard = new(KeyWidth, KeyHeight, KeyMargin);
        }

        public static readonly DependencyProperty KeyWidthProperty;
        public static readonly DependencyProperty KeyHeightProperty;
        public static readonly DependencyProperty KeyMarginProperty;

        private const int MAPVK_VK_TO_VSC = 0;

        public Keyboard MainKeyboard { get; set; }

        public double KeyWidth
        {
            get => (double)base.GetValue(KeyWidthProperty);
            set => SetValue(KeyWidthProperty, value);
        }

        public double KeyHeight
        {
            get => (double)GetValue(KeyHeightProperty);
            set => SetValue(KeyHeightProperty, value);
        }

        public Thickness KeyMargin
        {
            get => (Thickness)GetValue(KeyMarginProperty);
            set => SetValue(KeyMarginProperty, value);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (UIElement child in InternalChildren)
            {
                var ch = child as ButtonBase;
                ch.Content = GetCharsFromKeys((VirtualKeyCode)ch.Tag, false, false);
                child.Measure(availableSize);
            }

            return base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double currentHeight = 0.0;
            double currentWidth = 0.0;
            double maxWidth = (KeyWidth + KeyMargin.Left + KeyMargin.Right) * 16;

            foreach (UIElement child in InternalChildren)
            {
                if (currentWidth + child.DesiredSize.Width > maxWidth)
                {
                    currentWidth = 0;
                    currentHeight += child.DesiredSize.Height;
                }

                child.Arrange(new Rect(new Point(currentWidth, currentHeight), child.DesiredSize));

                currentWidth += child.DesiredSize.Width;
            }

            return base.ArrangeOverride(finalSize);
        }

        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            if (visualAdded != null)
            {
                visualAdded.SetValue(MarginProperty, KeyMargin);

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
            uint tag = UInt32.Parse(button.Tag.ToString());
            int scanCode =  WinApi.MapVirtualKey(tag, MAPVK_VK_TO_VSC);
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
