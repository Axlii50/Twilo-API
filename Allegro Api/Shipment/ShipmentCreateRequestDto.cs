using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allegro_Api.Shipment.Components;

namespace Allegro_Api.Shipment
{
    public class ShipmentCreateRequestDto
    {
        public string deliveryMethodId { get; set; }

        public SenderAddressDto sender { get; set; }

        public ReceiverAddressDto receiver { get; set; }

        public Packages[] packages { get; set; }

        public CashOnDeliveryDto cachOnDelivery { get; set; }

        public string labelFormat { get; set; } = "PDF";

    }
}
