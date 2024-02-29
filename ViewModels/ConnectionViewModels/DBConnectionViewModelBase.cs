using moduleADO.Utils;
using System.ComponentModel;
using moduleADO.Services.Database.Args;

namespace moduleADO.ViewModels.ConnectionViewModels
{
    public abstract class DBConnectionViewModelBase : INotifyPropertyChanged
    {
        protected string _server;
        protected string _database;
        protected string _username;
        protected string _password;
        protected string _connectionStatus;
        protected string _tableName;
        public string Server
        {
            get { return _server; }
            set
            {
                _server = value;
                OnPropertyChanged(nameof(Server));
            }
        }

        public string Database
        {
            get { return _database; }
            set
            {
                _database = value;
                OnPropertyChanged(nameof(Database));
            }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = Password;
                OnPropertyChanged(nameof(Password));
            }
        }
        public void GetPassword(string password) => _password = password;
        public string ConnectionStatus
        {
            get { return _connectionStatus; }
            set
            {
                _connectionStatus = value;
                OnPropertyChanged(nameof(ConnectionStatus));
            }
        }
        public string TableName
        {
            get => _tableName;
            set
            {
                _tableName = value;
                OnPropertyChanged(nameof(TableName));
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public RelayCommand? ConnectCommand => new RelayCommand(execute => Connect());
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected abstract void Connect();
    }
}
