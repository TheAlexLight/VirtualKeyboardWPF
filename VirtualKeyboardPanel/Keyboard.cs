using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using KeyboardPanelLibrary.AdditionalMetadata;
using KeyboardPanelLibrary.Command;
using KeyboardPanelLibrary.Enums;
using KeyboardPanelLibrary.Extensions;
using KeyboardPanelLibrary.ViewModels;
using VirtualKeyboardWPF.Enums;

namespace KeyboardPanelLibrary
{
    [TemplatePart(Name = "PART_keyboardItemsControl", Type = typeof(ItemsControl))]
    public class Keyboard : Control
    {
        static Keyboard()
        {
            KeyBackgroundProperty = DependencyProperty.Register(nameof(KeyBackground), typeof(Brush), typeof(Keyboard)
                   , new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3d3d3d"))));
            KeyMarginProperty = DependencyProperty.Register(nameof(KeyMargin), typeof(Thickness), typeof(Keyboard)
                   , new PropertyMetadata(new Thickness(2)));
            KeyForegroundProperty = DependencyProperty.Register(nameof(KeyForeground), typeof(Brush), typeof(Keyboard)
                   , new PropertyMetadata(new SolidColorBrush(Colors.White)));
            KeyboardChooseTypeProperty = DependencyProperty.Register(nameof(KeyboardChooseType), typeof(KeyboardType), typeof(Keyboard)
                   , new PropertyMetadata(KeyboardType.MainKeyboard));
        }
        public Keyboard()
        {
            FontSize = 24;
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3d3d3d"));
            languageChanger = new();

            EventManager.RegisterClassHandler(typeof(UIElement), GotKeyboardFocusEvent, (KeyboardFocusChangedEventHandler)OnKeyboardFocusChanged);
        }

        private readonly LanguageChangerViewModel languageChanger;

        public static bool ShiftIsActive = false;

        public static readonly DependencyProperty KeyBackgroundProperty;
        public static readonly DependencyProperty KeyMarginProperty;
        public static readonly DependencyProperty KeyForegroundProperty;
        public static readonly DependencyProperty KeyboardChooseTypeProperty;

        private ICommand symbolsKeyboardCommand2;

        public static readonly DependencyProperty AdditionalMetadataProperty
        = DependencyProperty.RegisterAttached("AdditionalMetadata", typeof(KeyboardAdditionalMetadata), typeof(Keyboard), new PropertyMetadata());

        public static readonly DependencyProperty KeyboardTypeAttachedProperty
        = DependencyProperty.RegisterAttached("KeyboardTypeAttached", typeof(KeyboardType), typeof(UIElement), new PropertyMetadata());

        public static void SetAdditionalMetadataProperty(DependencyObject obj, KeyboardAdditionalMetadata value)
        {
            obj.SetValue(AdditionalMetadataProperty, value);
        }

        public static KeyboardAdditionalMetadata GetAdditionalMetadataProperty(DependencyObject obj)
        {
            return (KeyboardAdditionalMetadata)obj.GetValue(AdditionalMetadataProperty);
        }

        public static void SetKeyboardTypeAttachedProperty(DependencyObject obj, KeyboardType value)
        {
            obj.SetValue(KeyboardTypeAttachedProperty, value);
        }

        public static KeyboardType GetKeyboardTypeAttachedProperty(DependencyObject obj)
        {
            return (KeyboardType)obj.GetValue(KeyboardTypeAttachedProperty);
        }

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

            FillKeyList(true);
        }

        public void OnKeyboardFocusChanged(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (sender is TextBox)
            {
                var textBoxKeyboardType = GetKeyboardTypeAttachedProperty((DependencyObject)sender);
                KeyboardChooseType = textBoxKeyboardType;

                FillKeyList(true);
            }
        }

        private void FillKeyboard(Action<ItemsControl, int> fillOneKeyboard, ItemsControl keyItemsControl)
        {
            if (ShiftIsActive)
            {
                ShiftIsActive = false;
                KeyboardPanel.Send(VirtualKeyCode.Shift, KEYEVENTF.KEYUP);
            }

            switch (KeyboardChooseType)
            {
                case KeyboardType.FullKeyboard:
                    fillOneKeyboard(keyItemsControl, KeyboardPanel.ROWS_COUNT * 0 + 1);
                    FillNumpad(keyItemsControl, KeyboardPanel.ROWS_COUNT * 1 + 1);
                    break;
                case KeyboardType.MainKeyboard:
                    fillOneKeyboard(keyItemsControl, KeyboardPanel.ROWS_COUNT * 0 + 1);
                    break;
                case KeyboardType.Numpad:
                    FillNumpad(keyItemsControl, KeyboardPanel.ROWS_COUNT * 0 + 1);
                    break;
            }
        }

        private void FillKeyList(bool isStandartKeyboard)
        {
            ItemsControl keyItemsControl = GetTemplateChild("PART_keyboardItemsControl") as ItemsControl;

            keyItemsControl.Items.Clear();

            if (isStandartKeyboard)
            {
                FillKeyboard(FillMainKeyboard, keyItemsControl);
            }
            else
            {
                FillKeyboard(FillSymbolsKeyboard, keyItemsControl);
            }

                Style keyStyle = Application.Current.FindResource("keyStyle") as Style;
            Style changeSymbolsStyle = Application.Current.FindResource("symbolsChangerStyle") as Style;

            foreach (UIElement key in keyItemsControl.Items)
            {
                key.SetValue(MarginProperty, KeyMargin);

                var virtualCode = GetAdditionalMetadataProperty(key).VirtualCode;

                if (key is ButtonBase)
                {
                    if (virtualCode != 0x00)
                    {
                        key.SetValue(StyleProperty, keyStyle);
                    }
                    else
                    {
                        key.SetValue(StyleProperty, changeSymbolsStyle);
                    }
                };
            }
        }

        private void FillSymbolsKeyboard(ItemsControl keyItemsControl, int startRow)
        {
            int currentRow = startRow;

            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Tab, 2, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.N1, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.N2, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.N3, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.N4, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.N5, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Back, 2, currentRow));

            currentRow++;

            Button symbButton = new() { Content = "&123" };
            SymbolsAdditionalMetadata symbolsMetadata = new();
            symbolsMetadata.VirtualCode = 0;
            symbolsMetadata.WidthCoefficient = 2;
            symbolsMetadata.RowLocation = currentRow;
            symbolsMetadata.IsStandartKeyboard = false;

            symbButton.DataContext = symbolsMetadata;

            SetAdditionalMetadataProperty(symbButton, symbolsMetadata);

            keyItemsControl.Items.Add(symbButton);
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.N6, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.N7, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.N8, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.N9, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.N0, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Return, 2, currentRow));

            currentRow++;

            keyItemsControl.Items.Add(SetOneKey(new ToggleButton(), VirtualKeyCode.Shift, 2, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEM3, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEM7, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEM4, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEM6, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEM2, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEM5, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEM1, 1, currentRow));

            currentRow++;

            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Space, 2, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEMMinus, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEMPlus, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Multiply, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEMComma, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEMPeriod, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Left, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Right, 1, currentRow));
        }

        private void FillMainKeyboard(ItemsControl keyItemsControl, int startRow)
        {
            int currentRow = startRow;

            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Tab, 2, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Q, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.W, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.E, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.R, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.T, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Y, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.U, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.I, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.O, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.P, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEM5, 1, currentRow)); //\
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Back, 2, currentRow)); //Backspace 

            currentRow++;

            Button symbButton = new() { Content = "&123" };
            SymbolsAdditionalMetadata symbolsMetadata = new();
            symbolsMetadata.VirtualCode = 0;
            symbolsMetadata.WidthCoefficient = 2;
            symbolsMetadata.RowLocation = currentRow;
            symbolsMetadata.IsStandartKeyboard = true;

            symbButton.DataContext = symbolsMetadata;

            SetAdditionalMetadataProperty(symbButton, symbolsMetadata);

            keyItemsControl.Items.Add(symbButton);

            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.A, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.S, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.D, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.F, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.G, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.H, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.J, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.K, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.L, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEM1, 1, currentRow)); //;
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEM7, 1, currentRow)); //'
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Return, 2, currentRow)); //Enter

            currentRow++;

            keyItemsControl.Items.Add(SetOneKey(new ToggleButton(), VirtualKeyCode.Shift, 3, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Z, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.X, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.C, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.V, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.B, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.N, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.M, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEMComma, 1, currentRow)); //,
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEMPeriod, 1, currentRow)); //.
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEM2, 1, currentRow)); //?
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEM4, 1, currentRow)); //[
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.OEM6, 1, currentRow)); //]

            currentRow++;

            ComboBox combo = new();
            combo = (ComboBox)SetOneKey(combo, 0, 2, currentRow);
            combo.SelectionChanged += Combo_SelectionChanged;

            combo.DataContext = languageChanger;
            var comboBoxStyle = Application.Current.FindResource("comboBoxStyle") as Style;
            combo.SetValue(StyleProperty, comboBoxStyle);

            keyItemsControl.Items.Add(combo);
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Space, 11, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Left, 1, currentRow)); //Left arrow
            keyItemsControl.Items.Add(SetOneKey(new Button(), VirtualKeyCode.Right, 1, currentRow)); //Right arrow
        }

        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            languageChanger.SelectedLanguage = (Language)comboBox.SelectedItem;
        }

        private ItemsControl FillNumpad(ItemsControl keyItemsControl, int startRow)
        {
            int currentRow = startRow;

            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad7, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad8, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad9, 1, currentRow));

            currentRow++;

            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad4, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad5, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad6, 1, currentRow));

            currentRow++;

            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad1, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad2, 1, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad3, 1, currentRow));

            currentRow++;

            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Numpad0, 2, currentRow));
            keyItemsControl.Items.Add(SetOneKey(new RepeatButton(), VirtualKeyCode.Decimal, 1, currentRow));

            return keyItemsControl;
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

        public ICommand ChangeSymbolsKey => symbolsKeyboardCommand2 ??= new RelayCommand(symbButton =>
        {
            var isStandartKeyboard = (symbButton as SymbolsAdditionalMetadata).IsStandartKeyboard;
            isStandartKeyboard = !isStandartKeyboard;

            FillKeyList(isStandartKeyboard);
        });
    }
}
