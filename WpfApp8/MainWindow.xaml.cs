using Microsoft.EntityFrameworkCore;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp8.DB;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext db = new ApplicationContext();

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            db.Database.EnsureCreated();

            db.Products.Load();

            DataContext = db.Products.Local.ToObservableCollection();
            Product product = new Product();

            string str = "Id " + product.Id + "\n" + "Название: " + product.Name + "\n" + "Цена: " + product.Price + "₽" + "\n" + "Описание: " + product.Description;
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(str, QRCodeGenerator.ECCLevel.Q))
            using (QRCode qrCode = new QRCode(qrCodeData))
            {
                Bitmap qrCodeImage = qrCode.GetGraphic(20);
            }
          
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            EditAdd add = new EditAdd(new Product());
            if (add.ShowDialog() == true)
            {
                Product User = add.Product;
                db.Products.Add(User);
                db.SaveChanges();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {

            Product? product = productList.SelectedItem as Product;
            if (product is null) return;

            EditAdd EditAdd = new EditAdd(new Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price

            });

            if (EditAdd.ShowDialog() == true)
            {

                product = db.Products.Find(EditAdd.Product.Id);
                if (product != null)
                {
                    product.Name = EditAdd.Product.Name;
                    product.Description = EditAdd.Product.Description;
                    product.Price = EditAdd.Product.Price;
                    db.SaveChanges();
                    productList.Items.Refresh();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

            Product? product = productList.SelectedItem as Product;
            if (product is null) return;
            db.Products.Remove(product);
            db.SaveChanges();
        }
        public BitmapImage Convert(Bitmap src)
        {
            MemoryStream ms = new MemoryStream();
            ((System.Drawing.Bitmap)src).Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;

        }

       
    }
}
