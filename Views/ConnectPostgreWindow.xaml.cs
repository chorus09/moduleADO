using moduleADO.ViewModels.ConnectionViewModels;
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

public partial class ConnectPostgreWindow : Window {
    private PostgreConnectionViewModel _viewModel;
    public ConnectPostgreWindow() {
        InitializeComponent();
        _viewModel = new();
        DataContext = _viewModel;
    }
    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e) {
        _viewModel.GetPassword(PasswordBox.Password);
    }
    private void NextWindow_Click(object sender, RoutedEventArgs e) {
        if (_viewModel.IsConnected) {
            ChoosenWindow window = new();
            window.Show();
            this.Close();
        }
    }
}
