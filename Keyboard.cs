using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using KeyboardPanelLibrary.AdditionalMetadata;
using KeyboardPanelLibrary.Enums;
using KeyboardPanelLibrary.Extensions;
using VirtualKeyboardWPF.Enums;

namespace KeyboardPanelLibrary
{
    [TemplatePart(Name = "PART_keyboardItemsControl", Type =typeof(ItemsControl))]
    public class Keyboard : Control
    {
        static Keyboard()
        {
            BackgroundProperty.OverrideMetadata(typeof(Keyboard), new FrameworkPropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3d3d3d"))));
            ForegroundProperty.OverrideMetadata(typeof(Keyboard), new FrameworkPropertyMetadata(new SolidColorBrush(Colors.White)));
            FontSizeProperty.OverrideMetadata(typeof(Keyboard), new FrameworkPropertyMetadata(16.0));
            HorizontalContentAlignmentProperty.OverrideMetadata(typeof(Keyboard), new FrameworkPropertyMetadata(HorizontalAlignment.Left));
            VerticalAlignmentProperty.OverrideMetadata(typeof(Keyboard), new FrameworkPropertyMetadata(VerticalAlignment.Top));

            KeyBackgroundProperty = DependencyProperty.Register(nameof(KeyBackground), typeof(Brush), typeof(Keyboard)
                   , new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3d3d3d"))));
            KeyMarginProperty = DependencyProperty.Register(nameof(KeyMargin), typeof(Thickness), typeof(Keyboard)
                   , new PropertyMetadata(new Thickness(2)));
            KeyForegroundProperty = DependencyProperty.Register(nameof(KeyForeground), typeof(Brush), typeof(Keyboard)
                   , new PropertyMetadata(new SolidColorBrush(Colors.White)));
            KeyboardChooseTypeProperty = DependencyProperty.Register(nameof(KeyboardChooseType), typeof(KeyboardType), typeof(Keyboard)
                   , new PropertyMetadata(KeyboardType.FullKeyboard));
        }

        public ComboBoxItem LanguageList { get; set; }

        public static bool ShiftIsActive = false;

        public static readonly DependencyProperty KeyBackgroundProperty;
        public static readonly DependencyProperty KeyMarginProperty;
        public static readonly DependencyProperty KeyForegroundProperty;
        public static readonly DependencyProperty KeyboardChooseTypeProperty;

        public static readonly DependencyProperty AdditionalMetadataProperty
        = DependencyProperty.RegisterAttached("SetAdditionalMetadata", typeof(KeyboardAdditionalMetadata), typeof(Keyboard), new PropertyMetadata());

        public static readonly DependencyProperty KeyboardTypeAttachedProperty
        = DependencyProperty.RegisterAttached("SetKeyboardAttachedType", typeof(KeyboardType), typeof(UIElement), new PropertyMetadata());

        public static void SetAdditionalMetadataProperty(DependencyObject obj, KeyboardAdditionalMetadata value)
        {
            obj.SetValue(AdditionalMetadataProperty, value);
        }

        public static KeyboardAdditionalMetadata GetAdditionalMetadataProperty(DependencyObject obj)
        {
            return (KeyboardAdditionalMetadata)obj.GetValue(AdditionalMetadataProperty);
        }

        public static void SetSetKeyboardTypeAttachedProperty(DependencyObject obj, KeyboardType value)
        {
            obj.SetValue(KeyboardTypeAttachedProperty, value);
        }

        public static KeyboardType GetSetKeyboardTypeAttachedProperty(DependencyObject obj)
        {
            return (KeyboardType)obj.GetValue(KeyboardTypeAttachedProperty);
        }

        //public string ComboBoxName { get; set; }
        public List<ComboBoxItem> ComboBoxList { get; set; }

        public Brush KeyBackground
        {
            get => (Brush)base.GetValue(KeyBackgroundProperty);
            set => SetValue(KeyBackgroundProperty, value);
        }

        public Thickness KeyMargin
        {
            get => (Thickness)base.GetValue(KeyMarginProperty);
            set => SetValue(KeyMarginProperty, value);
        }

        public Brush KeyForeground
        {
            get => (Brush)base.GetValue(KeyForegroundProperty);
            set => SetValue(KeyForegroundProperty, value);
        }

        public KeyboardType KeyboardChooseType
        {
            get => (KeyboardType)base.GetValue(KeyboardChooseTypeProperty);
            set => SetValue(KeyboardChooseTypeProperty, value);
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            FillKeyList();
        }

        private void FillKeyList()
        {
            ItemsControl keyItemsControl = GetTemplateChild("PART_keyboardItemsControl") as ItemsControl;

            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Q, 1, 1));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.W, 1, 1));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.E, 1, 1));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.R, 1, 1));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.T, 1, 1));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Y, 2, 1));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.U, 1, 1));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.I, 1, 1));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.O, 1, 1));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.P, 1, 1));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEM4, 1, 1)); //[
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEM6, 1, 1)); //]
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEM5, 1, 1)); //\
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Back, 2, 1)); //Backspace 

            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Tab, 2, 2));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.A, 1, 2));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.S, 1, 2));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.D, 1, 2));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.F, 1, 2));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.G, 1, 2));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.H, 1, 2));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.J, 1, 2));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.K, 1, 2));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.L, 1, 2));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEM1, 1, 2)); //;
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEM7, 1, 2)); //'
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Return, 2, 2)); //Enter

            ToggleButton shiftButton = (ToggleButton)SetOneKey(new ToggleButton(), VirtualKeyCode.Shift, 3, 3);

            ShiftAdditionalMetadata additionalMetadata = new();
            additionalMetadata.VirtualCode = (ushort)VirtualKeyCode.Shift;
            additionalMetadata.WidthCoefficient = 3;
            additionalMetadata.RowLocation = 3;

            SetAdditionalMetadataProperty(shiftButton, additionalMetadata);
            keyItemsControl.Items.Add(shiftButton);



            //keyItemsControl.Items.Add(SetOneKey(new ToggleButton(), VirtualKeyCode.Shift, 3, 3));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Z, 1, 3));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.X, 1, 3));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.C, 1, 3));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.V, 2, 3));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.B, 1, 3));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.N, 1, 3));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.M, 1, 3));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEMComma, 1, 3)); //,
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEMPeriod, 1, 3)); //.
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEM2, 1, 3)); //?

            ComboBox combo = new();
            combo = (ComboBox)SetOneKey(combo, 0, 2, 3);
            var comboBoxStyle = Application.Current.FindResource("comboBoxStyle") as Style;
            combo.SetValue(StyleProperty, comboBoxStyle);
           
            combo.ItemsSource = GetLocalLanguages();
            //ComboBoxName = "ABC";
            keyItemsControl.Items.Add(combo);

            //keyItemsControl.Items.Add(SetOneKey(new ComboBox() { Background = KeyBackground, Foreground = Foreground, FontSize = FontSize }, 0, 2)); //Change language
            //KeyList.Items.Last().SetValue(StyleProperty, comboBoxStyle);
            //((ComboBox)KeyList.Last()).ItemsSource = GetLocalLanguages();
            //((ComboBox)KeyList.Last()).SelectedIndex = 0;

            keyItemsControl.Items.Add(SetOneKey(new Button() { Content = "&123" }, 0, 2, 4)); //Change to numbers
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Space, 11, 4));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Left, 1, 4)); //Left arrow
            keyItemsControl.Items.Add(SetOneKey(new Button(), VirtualKeyCode.Right, 1, 4)); //Right arrow

            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad7, 1, 5));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad8, 1, 5));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad9, 1, 5));

            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad4, 1, 6));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad5, 1, 6));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad6, 1, 6));

            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad1, 1, 7));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad2, 1, 7));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad3, 1, 7));

            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad0, 2, 8));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Decimal, 1, 8));

            Style keyStyle = Application.Current.FindResource("keyStyle") as Style;

            foreach (UIElement key in keyItemsControl.Items)
            {
                key.SetValue(MarginProperty, KeyMargin);

                if (key is ButtonBase)
                {
                    key.SetValue(StyleProperty, keyStyle);
                    var a = (Style)key.GetValue(StyleProperty);
                };
            }
        }

        private UIElement SetOneKey(UIElement buttonType, VirtualKeyCode virtualKey, double widthCoefficient, int rowLocation)
        {
            KeyboardAdditionalMetadata additionalMetadata = new();
            additionalMetadata.VirtualCode = (ushort)virtualKey;
            additionalMetadata.WidthCoefficient = widthCoefficient;
            additionalMetadata.RowLocation = rowLocation;

            SetAdditionalMetadataProperty(buttonType, additionalMetadata);

            return buttonType;
        }

        private List<ComboBoxItem> GetLocalLanguages()
        {
            var keyboardLanguages = new List<ComboBoxItem>();

            uint nElements = WinApi.GetKeyboardLayoutList(0, null);
            IntPtr[] keyboardsIds = new IntPtr[nElements];
            WinApi.GetKeyboardLayoutList(keyboardsIds.Length, keyboardsIds);

            foreach (var keyboardId in keyboardsIds)
            {
                var languageId = (UInt16)((UInt32)keyboardId & 0xFFFF);

                CultureInfo languageInfo = new CultureInfo(languageId, false);

                ComboBoxItem comboBoxItem = new();
                comboBoxItem.Content = languageInfo.ThreeLetterWindowsLanguageName;
                comboBoxItem.Tag = languageId;

                keyboardLanguages.Add(comboBoxItem);
            }

            return keyboardLanguages;
        }


    }
}
