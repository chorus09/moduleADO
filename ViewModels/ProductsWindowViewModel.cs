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

    public RelayCommand RefreshProductsCommand => new(async execute => await RefreshProducts());
    public RelayCommand GetProductsCommand => new(async execute => await GetProducts(), canExecute => Email != null);
    public RelayCommand DeleteProductCommand => new(async execute => await DeleteProduct(), canExecute => SelectedProduct?.Id != null);
    public RelayCommand DeleteAllProductsCommand => new(async execute => await DeleteAllProducts(), canExecute => Products?.Count > 0);
    public RelayCommand DeleteProductsForOneCommand => new(async execute => await DeleteProductsForOne(), canExecute => Email != null);
    public ProductsWindowViewModel() {
        Products = [];
        PostgreService service = DatabaseHelper.GetPostgreConnection();
        _connectionString = service.ConnectionString;
        _tableName = service.Table;

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
    private async Task DeleteProductsForOne() {
        IAllDeleteForOne request = new PostgreDeleteRequest(new DeletePostgresDTO() {
            Connection = _connectionString!,
            TableName = _tableName!,
            Email = Email!,
        });
        await request.ExecuteAllForOne();
        await GetProducts();
    }
    private async Task DeleteProduct() {
        IDatabaseRequest request = new PostgreDeleteRequest(new DeletePostgresDTO() {
            Connection = _connectionString!,
            TableName = _tableName!,
            Id = _selectedProduct!.Id
        });
        await request.Execute();
        await GetProducts();
    }
    private async Task DeleteAllProducts() {
        IAll request = new PostgreDeleteRequest(new DeletePostgresDTO() {
            Connection = _connectionString!,
            TableName = _tableName!,
        });
        await request.ExecuteAll();
    }
    private async Task GetProducts() {
        IDatabaseRequest request = new PostgreSelectRequest(new SelectPostgresDTO() {
            Connection = _connectionString,
            TableName = _tableName,
            Email = this.Email
        });
        await request.Execute();
        IValues values = (PostgreSelectRequest)request;
        Products = new ObservableCollection<Product>(ProductManage.GetProductsFromList(values.Values));
        foreach (var item in Products) {
            item.Attach(_productUpdater);
        }
    }
    private async Task RefreshProducts() {
        IAll request = new PostgreSelectRequest(new SelectPostgresDTO() {
            Connection = _connectionString,
            TableName = _tableName,
        });
        await request.ExecuteAll();
        IValues values = (PostgreSelectRequest)request;
        Products = new ObservableCollection<Product>(ProductManage.GetProductsFromList(values.Values));
    }
}
