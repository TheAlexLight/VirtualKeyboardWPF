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
        public abstract double FullWidth { get; set; }
        public abstract List<UIElement> KeyList { get; set; }
        public abstract int KeysInRow { get; set; }
    }
}
