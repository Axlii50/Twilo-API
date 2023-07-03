using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api.Models.Product.ProductComponents
{
    public class DescriptionSectionItem
    {
        /// <summary>
        /// default "TEXT"
        /// "TEXT" "IMAGE"
        /// </summary>
        public string type { get; set; }

        public string content;
        public string url;
    }
}
