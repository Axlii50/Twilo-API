using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api.Models.Product.ProductComponents
{
    //na razie klasa jest okrojona do minimum według tutorialu 
    public class ProductParameter
    {
        public string id { get; set; }

        //public ParameterRangeValue rangeValue { get; set; }

        public string[] values { get; set; }

        //public string[] valuesIds { get; set; }

        //public string[] valuesLabels { get; set; }

        //public string unit { get;set; }

        //public Options options { get; set; }
    }
}
