using moduleADO.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace moduleADO.Views; 
public partial class ProductsWindow : Window {
    private ProductsWindowViewModel? _viewModel;
    public ProductsWindow() {
        InitializeComponent();
        _viewModel = new();
        DataContext = _viewModel;
    }
    private void RefreshItems_Click(object sender, RoutedEventArgs e) {
        ProductsDataGrid.Items.Refresh();
    }

    private void AddNewProduct_Click(object sender, RoutedEventArgs e) {
        ProductCreatorWindow window = new();
        window.Show();
    }

    private void ChangeWindow_Click(object sender, RoutedEventArgs e) {
        ChoosenWindow window = new();
        window.Show();
        this.Close();
    }
}
