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

namespace moduleADO.Views {
    public partial class ProductCreatorWindow : Window {
        private ProductCreatorWindowViewModel _viewModel;
        public ProductCreatorWindow() {
            InitializeComponent();
            _viewModel = new();
            _viewModel.ProductCreated += (sender, message) => MessageBox.Show(message);
            DataContext = _viewModel;
        }
    }
}
