using KeyboardPanelLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace KeyboardPanelLibrary.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
       public MainWindowViewModel()
        {

            key1 = new();

            //Style keyboardStyle = Application.Current.FindResource("keyboardStyle") as Style;
            //Keyboard key = new Keyboard();

            //key.Style = keyboardStyle;
            //key1 = key.KeyList;
            //var a = key.Margin;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private List<UIElement> key1;

        public List<UIElement> Key { get; set; } = new List<UIElement>() { new RepeatButton()};
        //{
        //    get
        //    {
        //        return key1;
        //    }
        //    set
        //    {
        //        key1 = value;
        //        OnPropertyChanged();
        //    }
        //} 


        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
