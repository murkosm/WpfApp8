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
using WpfApp8.DB;

namespace WpfApp8
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class EditAdd : Window
    {
        public Product Product { get; private set; }
        public EditAdd(Product product)
        {
            InitializeComponent();
            Product = product;
            DataContext = Product;
        }

        void ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
