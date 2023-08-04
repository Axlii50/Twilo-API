using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allegro_Api
{
    public enum OrderStatusType
    {
        NEW = 1,
        PROCESSING = 2,
        READY_FOR_SHIPMENT = 4,
        READY_FOR_PICKUP = 8,
        SENT = 16,
        PICKED_UP = 32,
        CANCELLED = 64,
        SUSPENDED = 128,
    }
}
