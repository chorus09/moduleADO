﻿using moduleADO.Models;
using moduleADO.Services.Database;
using moduleADO.Services.Database.Requests;
using moduleADO.Services.Database.Requests.MSSQL;
using moduleADO.Utils;
using System.Collections.ObjectModel;
using System.ComponentModel;
using moduleADO.Services;
using moduleADO.Models.Observer;
using moduleADO.Services.Database.Requests.DTO.MSSQLDTO;

namespace moduleADO.ViewModels; 
public class ClientsWindowViewModel : INotifyPropertyChanged {
    private ObservableCollection<Client> _clients;
    private Client _selectedClient;
    private string? _connectionString;
    private string? _tableName;
    private ClientUpdater _clientUpdater;

    public ObservableCollection<Client> Clients {
        get => _clients;
        set {
            _clients = value;
            OnPropertyChanged(nameof(Clients));
        }
    }

    public Client SelectedClient {
        get => _selectedClient;
        set {
            _selectedClient = value;
            OnPropertyChanged(nameof(SelectedClient));
        }
    }
    public RelayCommand GetClientsCommand => new(execute => GetClients());
    public RelayCommand RefreshClientsCommand => new(execute => RefreshClients());
    public RelayCommand DeleteClientCommand => new(execute => DeleteClient());
    public RelayCommand DeleteAllClientsCommand => new(execute => DeleteAllClients());
    public ClientsWindowViewModel() {
        Clients = [];
        _connectionString = $"Server=localhost;Database=mssqllocaldb;User Id=sa;Password=sa;";
        _tableName = $"clients";
        DatabaseHelper.SetConnection(new MSSQLService(_connectionString, _tableName));
        _clientUpdater = new(GetAction());
    }
    private Action<Client> GetAction() => (client) => {
        IDatabaseRequest request = new MsSqlUpdateRequest(new UpdateMSSQLDTO() {
            FirstName = client.FirstName,
            Id = client.Id,
            LastName = client.LastName,
            MiddleName = client.MiddleName,
            Email = client.Email,
            Phone = client.Phone,
            Connection = _connectionString,
            TableName = _tableName
        });
        request.Execute();
    };
    private void GetClients() {
        IDatabaseRequest request = new MsSqlSelectRequest(
            new SelectMSSQLDTO() {
                Connection = _connectionString,
                TableName = _tableName,
            });
        request.Execute();
        IValues values = (MsSqlSelectRequest)request;
        Clients = new ObservableCollection<Client>(ClientManage.GetClientsFromList(values.Values));
        foreach (var item in Clients) {
            item.Attach(_clientUpdater);
        }
    }

    private void RefreshClients() {
        GetClients();
    }

    private void DeleteClient() {
        IDatabaseRequest request = new MsSqlDeleteRequest(new DeleteMSSQLDTO() {
            Connection = _connectionString,
            TableName = _tableName,
            Id = _selectedClient.Id
        });
        request.Execute();
        GetClients();
    }

    private void DeleteAllClients() {
        IAll request = new MsSqlDeleteRequest(new DeleteMSSQLDTO() {
            Connection = _connectionString,
            TableName = _tableName,
        });
        request.ExecuteAll();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
