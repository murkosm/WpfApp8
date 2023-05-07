using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WpfApp8.DB
{
    public class Product 
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }

        public string? Description { get; set; }
        BitmapImage QrCode { get; set; }
    }
}
