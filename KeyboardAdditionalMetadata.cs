using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardPanelLibrary
{
    public class KeyboardAdditionalMetadata
    {
        public ushort VirtualCode { get; set; }
        public double WidthCoefficient { get; set; }
        public int RowLocation { get; set; }
        //public int CustomKeyCode { get; set; }
    }
}
