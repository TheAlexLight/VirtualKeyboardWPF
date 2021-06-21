using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KeyboardPanelLibrary
{
    public abstract class KeyboardBase: Control
    {
        public abstract double FullWidth { get;}
        public abstract List<UIElement> KeyList { get; set; }
        public abstract int[] KeysInRow { get; set; }

        public static readonly DependencyProperty AdditionalMetadataProperty
        = DependencyProperty.RegisterAttached("SetAdditionalMetadata", typeof(KeyboardAdditionalMetadata), typeof(KeyboardBase), new PropertyMetadata());

        public static KeyboardAdditionalMetadata GetAdditionalMetadataProperty(DependencyObject obj)
        {
            return (KeyboardAdditionalMetadata)obj.GetValue(AdditionalMetadataProperty);
        }

        public static void SetAdditionalMetadataProperty(DependencyObject obj, KeyboardAdditionalMetadata value)
        {
            obj.SetValue(AdditionalMetadataProperty, value);
        }
    }
}
