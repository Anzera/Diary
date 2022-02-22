using Diary.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Diary.Models

{
    public class UserSettings : IDataErrorInfo
    {
        private bool _isServerAdresCorrect;
        private bool _isServerNameCorrect;
        private bool _isDbNameCorrect;
        private bool _isUserCorrect;
        private bool _isPasswordCorrect;

        public static string ServerAdres
        {
            get
            {
                return Settings.Default.ServerAdres;
            }
            set
            {
                Settings.Default.ServerAdres = value;
            }
        }
        public static string ServerName
        {
            get
            {
                return Settings.Default.ServerName;
            }
            set
            {
                Settings.Default.ServerName = value;
            }
        }
        public static string DbName
        {
            get
            {
                return Settings.Default.DbName;
            }
            set
            {
                Settings.Default.DbName = value;
            }
        }


        public static string User
        {
            get
            {
                return Settings.Default.User;
            }
            set
            {
                Settings.Default.User = value;
            }
        }
        public static string Password
        {
            get
            {
                return Settings.Default.Password;
            }
            set
            {
                Settings.Default.Password = value;
            }
        }

        public string Error {get; set;}

        public string this[string columnName]
        {
            get
            {
                switch(columnName)
                {
                    case nameof(ServerAdres):
                        if(string.IsNullOrWhiteSpace(ServerAdres))
                        {
                            Error = "Adres serwera jest wymagany";
                            _isServerAdresCorrect = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isServerAdresCorrect = true;
                        }
                        break;
                    case nameof(ServerName):
                        if (string.IsNullOrWhiteSpace(ServerName))
                        {
                            Error = "Nazwa serwera jest wymagana";
                            _isServerNameCorrect = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isServerNameCorrect = true;
                        }
                        break;
                    case nameof(DbName):
                        if (string.IsNullOrWhiteSpace(DbName))
                        {
                            Error = "Nazwa bazy danych jest wymagana";
                            _isDbNameCorrect = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isDbNameCorrect = true;
                        }
                        break;
                    case nameof(User):
                        if (string.IsNullOrWhiteSpace(User))
                        {
                            Error = "Nazwa użytkownika jest wymagana";
                            _isUserCorrect = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isUserCorrect = true;
                        }
                        break;
                    case nameof(Password):
                        if (string.IsNullOrWhiteSpace(Password))
                        {
                            Error = "Hasło jest wymagane";
                            _isPasswordCorrect = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isPasswordCorrect = true;
                        }
                        break;
                    default:
                        break;
                }
                return Error;
            }
        }
        public bool IsValid
        {
            get
            {
                return _isServerAdresCorrect && _isServerNameCorrect && _isDbNameCorrect && _isUserCorrect && _isPasswordCorrect;
            }
        }

        public static string ConnectingStringBuilder()
        { 
            return $"Server={ServerAdres}\\{ServerName};Database={DbName};User Id={User};Password={Password};";
        }


        public void Save()
        {
            Settings.Default.Save();
        }
    }
}
