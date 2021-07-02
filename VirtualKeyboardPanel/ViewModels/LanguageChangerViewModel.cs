using KeyboardPanelLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace KeyboardPanelLibrary.ViewModels
{
    public class LanguageChangerViewModel : INotifyPropertyChanged
    {
        public LanguageChangerViewModel()
        {
            languages = new();

            ReceiveCurrentLanguage();
            ReceiveLanguages();
        }

        private UInt16 currentSystemLanguage;

        private ObservableCollection<Language> languages;
        private Language selectedLanguage;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Language> Languages
        {
            get => languages;
            set
            {
                languages = value;
                OnPropertyChanged();
            }
        }
        public Language SelectedLanguage
        {
            get => selectedLanguage;
            set
            {
                selectedLanguage = value;
                OnPropertyChanged();
            }
        }

        private void ReceiveCurrentLanguage()
        {
            IntPtr currentLanguage = WinApi.GetKeyboardLayout(0);

            currentSystemLanguage = (UInt16)((UInt32)currentLanguage & 0xFFFF);
        }

        private void ReceiveLanguages()
        {
            uint nElements = WinApi.GetKeyboardLayoutList(0, null);
            IntPtr[] keyboardsIds = new IntPtr[nElements];
            WinApi.GetKeyboardLayoutList(keyboardsIds.Length, keyboardsIds);

            foreach (var keyboardId in keyboardsIds)
            {
                var languageId = (UInt16)((UInt32)keyboardId & 0xFFFF);

                CultureInfo languageInfo = new CultureInfo(languageId, false);

                Language systemLanguage = new();
                systemLanguage.Id = languageId;
                systemLanguage.Name = languageInfo.ThreeLetterWindowsLanguageName;

                Languages.Add(systemLanguage);

                if (languageId == currentSystemLanguage)
                {
                    SelectedLanguage = systemLanguage;
                }
            }
        }

        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
