using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KeyboardPanelLibrary
{
   public class Numpad : KeyboardBase
    {
        public override double FullWidth { get => throw new NotImplementedException(); }
        public override List<UIElement> KeyList { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override int[] KeysInRow { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
