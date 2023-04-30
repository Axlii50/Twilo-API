using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api.Models.category.Parameters
{
    public class CategoryParameter
    {
        public string id { get; set; }

        public string name { get; set; }

        public string type { get; set; }

        public bool required { get; set; }
        
        public bool requiredForProduct { get; set; }

        //TODO displayedIF variable

        public string unit { get; set; }
    }
}
