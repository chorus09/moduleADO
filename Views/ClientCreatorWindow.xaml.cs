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
using moduleADO.ViewModels;

namespace moduleADO.Views; 
public partial class ClientCreatorWindow : Window {
    private ClientCreatorWindowViewModel _viewModel;
    public ClientCreatorWindow() {
        InitializeComponent();
        _viewModel = new();
        _viewModel.ClientCreated += (sender, message) => MessageBox.Show(message);
        DataContext = _viewModel;
    }
}
