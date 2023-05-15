using Allegro_Api.Models.category.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api.Models.Product
{
    public class AllegroProduct
    {
        public string id { get; set; }

        public string name { get; set; }

        public Base category { get; set; }

        public CategoryParameter[] parameters { get; set; }

        public Image[] images { get; set; }
    }
}
