using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api.Models.category.CategorySuggestions
{

    /// <summary>
    /// "Książki i Komiksy" 
    /// ID = 7
    /// </summary>
    public class CategoryNode
    {
        public string id { get; set; }
        public string name { get; set; }
        public CategoryNode parent { get; set; }
    }
}
