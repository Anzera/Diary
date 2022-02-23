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
        public string ServerAdres
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
        public string ServerName
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
        public string DbName
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
        public string User
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
        public string Password
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

        private bool _isServerAdresValid;
        private bool _isServerNameValid;
        private bool _isDbNameValid;
        private bool _isUserValid;
        private bool _isPasswordValid;
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
                            _isServerAdresValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isServerAdresValid = true;
                        }
                        break;
                    case nameof(ServerName):
                        if (string.IsNullOrWhiteSpace(ServerName))
                        {
                            Error = "Nazwa serwera jest wymagana";
                            _isServerNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isServerNameValid = true;
                        }
                        break;
                    case nameof(DbName):
                        if (string.IsNullOrWhiteSpace(DbName))
                        {
                            Error = "Nazwa bazy danych jest wymagana";
                            _isDbNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isDbNameValid = true;
                        }
                        break;
                    case nameof(User):
                        if (string.IsNullOrWhiteSpace(User))
                        {
                            Error = "Nazwa użytkownika jest wymagana";
                            _isUserValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isUserValid = true;
                        }
                        break;
                    case nameof(Password):
                        if (string.IsNullOrWhiteSpace(Password))
                        {
                            Error = "Hasło jest wymagane";
                            _isPasswordValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isPasswordValid = true;
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
                return _isServerAdresValid && _isServerNameValid && _isDbNameValid && _isUserValid && _isPasswordValid;
            }
        }

        public void Save()
        {
            Settings.Default.Save();
        }
    }
}
