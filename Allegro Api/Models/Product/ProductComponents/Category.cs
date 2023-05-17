using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api.Models.Product.ProductComponents
{
    public class Category
    {
        public string id { get; set; }

        public Base[] similar { get; set; }
    }
}
