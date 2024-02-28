using moduleADO.Models;
using moduleADO.Services;
using moduleADO.Services.Database.Requests;
using moduleADO.Services.Database.Requests.Postgre;
using moduleADO.Utils;
using System.Collections.ObjectModel;
using System.ComponentModel;
using moduleADO.Services.Database.Requests.DTO.PostgreDTO;
using moduleADO.Services.Database;
using moduleADO.Models.Observer;

namespace moduleADO.ViewModels;
public class ProductsWindowViewModel : INotifyPropertyChanged {
    private string? _email;
    private Product? _selectedProduct;
    private string? _tableName;
    private string? _connectionString;
    private ProductUpdater _productUpdater;

    public string? Email {
        get => _email;
        set {
            if (_email == value) return;
            _email = value;
            OnPropertyChanged(nameof(Email));
        }
    }
    public Product? SelectedProduct {
        get => _selectedProduct;
        set {
            if (_selectedProduct == value) return;
            _selectedProduct = value;
            OnPropertyChanged(nameof(SelectedProduct));
        }
    }
    private ObservableCollection<Product>? _products;
    public ObservableCollection<Product>? Products {
        get => _products;
        set {
            _products = value;
            OnPropertyChanged(nameof(Products));
        }
    }
    public event PropertyChangedEventHandler? PropertyChanged;

    public RelayCommand RefreshProductsCommand => new(execute => RefreshProducts());
    public RelayCommand GetProductsCommand => new(execute => GetProducts(), canExecute => Email != null);
    public RelayCommand DeleteProductCommand => new(execute => DeleteProduct(), canExecute => SelectedProduct?.Id != null);
    public RelayCommand DeleteAllProductsCommand => new(execute => DeleteAllProducts(), canExecute => Products?.Count > 0);
    public RelayCommand DeleteProductsForOneCommand => new(execute => DeleteProductsForOne(), canExecute => Email != null);
    public ProductsWindowViewModel() {
        Products = [];
        _connectionString = $"Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=26061;";
        _tableName = $"products";
        DatabaseHelper.SetConnection(new PostgreService(_connectionString, _tableName));
        _productUpdater = new(GetAction());
    }
    private Action<Product> GetAction() => (product) => {
        IDatabaseRequest request = new PostgreUpdateProductRequest(new UpdatePostgresDTO() {
            Code = product.Code,
            Name = product.Name,
            Connection = _connectionString,
            Email = Email!,
            Id = product.Id,
            TableName = _tableName
        });
        request.Execute();
    };

    protected virtual void OnPropertyChanged(string propertyName) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    private void DeleteProductsForOne() {
        IAllDeleteForOne request = new PostgreDeleteRequest(new DeletePostgresDTO() {
            Connection = _connectionString!,
            TableName = _tableName!,
            Email = Email!,
        });
        request.ExecuteAllForOne();
        GetProducts();
    }
    private void DeleteProduct() {
        IDatabaseRequest request = new PostgreDeleteRequest(new DeletePostgresDTO() {
            Connection = _connectionString!,
            TableName = _tableName!,
            Id = _selectedProduct!.Id
        });
        request.Execute();
        GetProducts();
    }
    private void DeleteAllProducts() {
        IAll request = new PostgreDeleteRequest(new DeletePostgresDTO() {
            Connection = _connectionString!,
            TableName = _tableName!,
            Id = _selectedProduct!.Id
        });
        request.ExecuteAll();
        GetProducts();
    }
    private void GetProducts() {
        IDatabaseRequest request = new PostgreSelectRequest(new SelectPostgresDTO() {
            Connection = _connectionString,
            TableName = _tableName,
            Email = this.Email
        });
        request.Execute();
        IValues values = (PostgreSelectRequest)request;
        Products = new ObservableCollection<Product>(ProductManage.GetProductsFromList(values.Values));
        foreach (var item in Products) {
            item.Attach(_productUpdater);
        }
    }
    private void RefreshProducts() {
        IAll request = new PostgreSelectRequest(new SelectPostgresDTO() {
            Connection = _connectionString,
            TableName = _tableName,
        });
        request.ExecuteAll();
        IValues values = (PostgreSelectRequest)request;
        Products = new ObservableCollection<Product>(ProductManage.GetProductsFromList(values.Values));
    }
}
