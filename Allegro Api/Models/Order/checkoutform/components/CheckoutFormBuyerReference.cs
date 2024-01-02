using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api.Models.Order.checkoutform.components
{
    public class CheckoutFormBuyerReference
    {
        public string id { get; set; }

        public string email { get; set; }
        
        public string login { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string phoneNumber { get; set; }
    }
}
