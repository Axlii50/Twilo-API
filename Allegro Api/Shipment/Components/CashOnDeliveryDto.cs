namespace Allegro_Api.Shipment.Components
{
    public class CashOnDeliveryDto
    {
        public string amount { get; set; }
        public string currency { get; set; }
        public string ownerName { get; set; }
        public string iban { get; set; }
    }
}