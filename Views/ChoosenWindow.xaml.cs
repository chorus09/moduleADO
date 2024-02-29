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
public partial class ChoosenWindow : Window {
    public ChoosenWindow() {
        InitializeComponent();
    }

    private void ClientsWindow_Click(object sender, RoutedEventArgs e) {
        ClientsWindow window = new();
        window.Show();
        this.Close();
    }

    private void ProductsWindow_Click(object sender, RoutedEventArgs e) {
        ProductsWindow window = new();
        window.Show();
        this.Close();
    }
}
