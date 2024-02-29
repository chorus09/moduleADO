using moduleADO.ViewModels;
using System.Windows;

namespace moduleADO.Views;
public partial class ClientsWindow : Window {
    private ClientsWindowViewModel _viewModel;
    public ClientsWindow() {
        InitializeComponent();
        _viewModel = new();
        DataContext = _viewModel;
    }

    private void AddNewClient_Click(object sender, RoutedEventArgs e) {
        ClientCreatorWindow window = new();
        window.Show();
    }

    private void RefreshItems_Click(object sender, RoutedEventArgs e) {
        ClientsDataGrid.Items.Refresh();
    }

    private void ChangeWindow_Click(object sender, RoutedEventArgs e) {
        ChoosenWindow window = new();
        window.Show();
        this.Close();
    }
}
