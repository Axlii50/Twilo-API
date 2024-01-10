using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api.Models.Shipment
{
    public class ShipmentCreationStatus
    {
        public string CommandId { get; set; }
        public string Status { get; set; }
        //public ErrorsModel Errors { get; set; }
        public string ShipmentId { get; set; }
    }
}
