using Allegro_Api.Models.Order.checkoutform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api.Models.Order
{
    public class OrdersModel
    {
        public List<CheckOutForm> checkoutForms { get; set; }
        public int count { get; set; }
        public int totalCount { get; set; }

    }
}
