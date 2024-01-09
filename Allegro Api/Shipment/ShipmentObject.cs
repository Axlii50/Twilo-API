using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api.Shipment
{
    public class ShipmentObject
    {
        public string commandId { get;set; }
        public ShipmentCreateRequestDto input { get;set; }
    }
}
