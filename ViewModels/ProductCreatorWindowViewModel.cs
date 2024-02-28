using moduleADO.Services.Database.Requests;
using moduleADO.Services.Database.Requests.Postgre;
using moduleADO.Utils;
using System.ComponentModel;
using moduleADO.Services.Database.Requests.DTO.PostgreDTO;
using moduleADO.Services.Database;

namespace moduleADO.ViewModels; 
public class ProductCreatorWindowViewModel : INotifyPropertyChanged {
    private string _code;
    private string _name;
    private string _email;
    private string? _tableName;
    private string? _connectionString;

    public string Code {
        get { return _code; }
        set {
            _code = value;
            OnPropertyChanged(nameof(Code));
        }
    }

    public string Name {
        get { return _name; }
        set {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    public string Email {
        get { return _email; }
        set {
            _email = value;
            OnPropertyChanged(nameof(Email));
        }
    }
    public ProductCreatorWindowViewModel() {
        _tableName = DatabaseHelper.GetConnection().Table;
        _connectionString = DatabaseHelper.GetConnection().ConnectionString;
    }
    public RelayCommand CreateProductCommand => new(execute => CreateProduct());
    public event EventHandler<string>? ProductCreated;
    public event PropertyChangedEventHandler? PropertyChanged;

    public void CreateProduct() {
        try {
            IDatabaseRequest request = new PostgreInsertRequest(new InsertIntoPostgresDTO() {
                Connection = _connectionString,
                TableName = _tableName,
                Product = new(_code, _name, _email)
            });
            request.Execute();
            ProductCreated?.Invoke(this, $"created new product for email: {_email}, name: {_name}");
        } catch(Exception ex) {
            ProductCreated?.Invoke(this, "error! : " + ex.Message);
        }
    }


    private void OnPropertyChanged(string propertyName) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
