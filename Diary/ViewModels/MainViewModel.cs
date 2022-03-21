using Diary.Commands;
using Diary.Models;
using Diary.Models.Configurations;
using Diary.Models.Domains;
using Diary.Models.Wrappers;
using Diary.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Diary.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private Repository _repository = new Repository();
        public MainViewModel()
        {
            AddStudentsCommand = new RelayCommand(AddEditStudents);
            EditStudentsCommand = new RelayCommand(AddEditStudents, CanEditDeleteStudent);
            DeleteStudentsCommand = new AsyncRelayCommand(DeleteStudents, CanEditDeleteStudent);
            RefreshStudentsCommand = new RelayCommand(RefreshStudents);
            SettingsCommand = new RelayCommand(NewSettings);
            LoadedWindowCommand = new RelayCommand(LoadedWindow);//event odpalający się wraz z programem

            LoadedWindow(null);

        }

       
        public ICommand RefreshStudentsCommand { get; set; }
        public ICommand AddStudentsCommand { get; set; }
        public ICommand EditStudentsCommand { get; set; }
        public ICommand DeleteStudentsCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }

        private StudentWrapper _selectedStudent;
        public StudentWrapper SelectedStudent
        {
            get { return _selectedStudent; }
            set 
            { 
                _selectedStudent = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<StudentWrapper> _students;
        public ObservableCollection<StudentWrapper> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged();
            }
        }

        private int _selectedGroupId;
        public int SelectedGroupId
        {
            get { return _selectedGroupId; }
            set 
            { 
                _selectedGroupId = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Group> _groups;
        public ObservableCollection<Group> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                OnPropertyChanged();
            }
        }
        private bool CanEditDeleteStudent(object obj)
        {
            return SelectedStudent != null;
        }
        private void RefreshStudents(object obj)
        {
            RefreshDiary();
        }
        private async Task DeleteStudents(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync(
                "Usuwanie ucznia", 
                $"Czy na pewno chcesz usunąć ucznia {SelectedStudent.FirstName} {SelectedStudent.LastName}?", 
                MessageDialogStyle.AffirmativeAndNegative);

            if (dialog != MessageDialogResult.Affirmative)
                return;

            _repository.DeleteStudent(SelectedStudent.Id);

            RefreshDiary();
        }
        private void NewSettings(object obj)
        {
            var userSettingsWindow = new UserSettingsView(false);//parametr false "zamyka" okno ustawień jeśli użytkownik wciśnie przycisk "Anuluj"
            userSettingsWindow.ShowDialog();
        }
        private void AddEditStudents(object obj)
        {
            var addEditStudentWindow = new AddEditStudentView(obj as StudentWrapper);
            addEditStudentWindow.Closed += addEditStudentWindow_Closed;
            addEditStudentWindow.ShowDialog();
        }
        private async void LoadedWindow(object obj)
        {
            if (!IsConnectionCorrect())//sprawdzanie czy połączenie z bazą danych jest poprawne
            {//jeśli nie
                var metroWindow = Application.Current.MainWindow as MetroWindow;//pokazanie na ekranie wiadomości o błedzie z połączeniem
                var dialog = await metroWindow.ShowMessageAsync(
                "Błąd połączenia z bazą danych!",
                "Nie udało połączyć się z bazą danych. Czy chcesz sprawdzić swoje ustawienia?",
                MessageDialogStyle.AffirmativeAndNegative);

                if (dialog != MessageDialogResult.Affirmative)//jęli użytkownik nie chce poprawiać danych
                    Application.Current.Shutdown();// aplikacja zamyka się
                else//jeśli użytkownik chce poprawić dane do połączenia z bazą 
                {
                    var userSettingsWindow = new UserSettingsView(true);//parametr true "zamyka" aplikację w momencie kiedy użytkownik wciścnie przycisk "Anuluj"
                    userSettingsWindow.ShowDialog();//to otwiera się okno ustawień
                }
            }
            else
            {
                RefreshDiary();
                InitGroups();
            }
        }

        private void addEditStudentWindow_Closed(object sender, EventArgs e)
        {
            RefreshDiary();
        }

        private void InitGroups()
        {
            var groups = _repository.GetGroups();
            groups.Insert(0, new Group { Id = 0, Name = "Wszystkie" });

            Groups = new ObservableCollection<Group>(groups);

            SelectedGroupId = 0;
        }
        private void RefreshDiary()
        {
            Students = new ObservableCollection<StudentWrapper>(
                _repository.GetStudents(SelectedGroupId));
        }

        private bool IsConnectionCorrect()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    context.Database.Connection.Open();
                    context.Database.Connection.Close();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
