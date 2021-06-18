using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KeyboardPanelLibrary
{
    public abstract class KeyboardBase
    {
        public abstract double Width { get; set; }
        public abstract List<UIElement> KeyList { get; set; }
        public abstract int KeysInRow { get; set; }
    }
}
