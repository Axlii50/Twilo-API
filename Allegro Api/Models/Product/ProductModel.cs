using Allegro_Api.Models.Product.ProductComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api.Models.Product
{
    public class ProductModel
    {
        public string name {  get; set; }
        public Base category { get; set; }

        public Image[] images { get; set; }

        public ProductParameter[] parameters { get; set; }

    }
}
