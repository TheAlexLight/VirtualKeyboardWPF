using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KeyboardPanelLibrary.Extensions
{
    public static class AttachedProperty
    {
        public static readonly DependencyProperty ButtonVirtualKeyCodeProperty
                = DependencyProperty.RegisterAttached("SetVirtualKeyCode", typeof(ushort), typeof(KeyboardBase), new PropertyMetadata());
        public static readonly DependencyProperty ButtonWidthCoefficientProperty
                = DependencyProperty.RegisterAttached("SetWidthCoefficient", typeof(double), typeof(KeyboardBase), new PropertyMetadata());

        public static ushort GetVirtualKeyCodeProperty(DependencyObject obj)
        {
            return (ushort)obj.GetValue(ButtonVirtualKeyCodeProperty);
        }

        public static void SetVirtualKeyCodeProperty(DependencyObject obj, ushort value)
        {
            obj.SetValue(ButtonVirtualKeyCodeProperty, value);
        }

        public static double GetWidthCoefficientProperty(DependencyObject obj)
        {
            return (double)obj.GetValue(ButtonWidthCoefficientProperty);
        }

        public static void SetWidthCoefficientProperty(DependencyObject obj, double value)
        {
            obj.SetValue(ButtonWidthCoefficientProperty, value);
        }
    }
}
