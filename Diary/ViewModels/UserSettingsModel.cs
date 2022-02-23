using Diary.Commands;
using Diary.Models;
using Diary.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Diary.ViewModels
{
    public class UserSettingsModel : ViewModelBase
    {
        private UserSettings _userSettings;
        private bool _close;
        public UserSettings UserSettings
        {
            get
            {
                return _userSettings;
            }
            set
            {
                _userSettings = value;
                OnPropertyChanged();
            }
        }
        public UserSettingsModel(bool close)
        {
            CloseSettingsCommand = new RelayCommand(Close);
            ConfirmSettingsCommand = new RelayCommand(Confirm);
            _userSettings = new UserSettings();
            _close = close;
        }

        public ICommand CloseSettingsCommand { get; set; }
        public ICommand ConfirmSettingsCommand { get; set; }

        private void Confirm(object obj)
        {
            if(!UserSettings.IsValid)
                return;

            UserSettings.Save();

            CloseWindow(obj as Window);
            RestartAsync();
        }

        private async Task RestartAsync()
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync(
                "Ustawienia połączenia z bazą danych",
                "Czy na pewno chcesz zmienić ustawienia połączenia z bazą danych?",
                MessageDialogStyle.AffirmativeAndNegative);


            if (dialog == MessageDialogResult.Affirmative) 
            {
                Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }
        }

        private void Close(object obj)
        {
            if(_close)
                CloseWindow(obj as Window);
            else
                Application.Current.Shutdown();
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }
    }
}
