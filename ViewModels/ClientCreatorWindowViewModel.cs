using moduleADO.Models;
using moduleADO.Services.Database.Requests.DTO.PostgreDTO;
using moduleADO.Services.Database.Requests.Postgre;
using moduleADO.Services.Database.Requests;
using moduleADO.Services.Database;
using moduleADO.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using moduleADO.Services.Database.Requests.MSSQL;

namespace moduleADO.ViewModels; 
public class ClientCreatorWindowViewModel : INotifyPropertyChanged {
    private string _firstName;
    private string _lastName;
    private string _middleName;
    private string _email;
    private string _phone;
    private string? _tableName;
    private string? _connectionString;

    public string FirstName {
        get { return _firstName; }
        set {
            _firstName = value;
            OnPropertyChanged(nameof(FirstName));
        }
    }

    public string LastName {
        get { return _lastName; }
        set {
            _lastName = value;
            OnPropertyChanged(nameof(LastName));
        }
    }

    public string MiddleName {
        get { return _middleName; }
        set {
            _middleName = value;
            OnPropertyChanged(nameof(MiddleName));
        }
    }

    public string Email {
        get { return _email; }
        set {
            _email = value;
            OnPropertyChanged(nameof(Email));
        }
    }

    public string Phone {
        get { return _phone; }
        set {
            _phone = value;
            OnPropertyChanged(nameof(Phone));
        }
    }

    public ClientCreatorWindowViewModel() {
        _tableName = DatabaseHelper.GetSqlConnection().Table;
        _connectionString = DatabaseHelper.GetSqlConnection().ConnectionString;
    }

    public RelayCommand CreateClientCommand => new RelayCommand(execute => CreateClient());

    public event EventHandler<string>? ClientCreated;
    public event PropertyChangedEventHandler? PropertyChanged;

    public void CreateClient() {
        try {
            IDatabaseRequest request = new MsSqlInsertRequest(
                new Services.Database.Requests.DTO.MSSQLDTO.InsertIntoMSSQLDTO() {
                Connection = _connectionString,
                TableName = _tableName,
                Client = new Client(_firstName, _lastName, _middleName, _email, _phone)
            });
            request.Execute();
            ClientCreated?.Invoke(this, $"created new client with email: {_email}");
        } catch (Exception ex) {
            ClientCreated?.Invoke(this, "error! : " + ex.Message);
        }
    }

    private void OnPropertyChanged(string propertyName) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
