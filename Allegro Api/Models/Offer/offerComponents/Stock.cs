using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api.Models.Offer.offerComponents
{
    public class Stock
    { 
        /// <summary>
        /// >=0
        /// </summary>
        public Int32 available { get; set; }

        /// <summary>
        /// UNIT PAIR SET 
        /// </summary>
        public string unit { get; set; }
    }
}
