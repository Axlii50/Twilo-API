namespace Allegro_Api.Models.Order.checkoutform
{
    public class CheckoutFormInvoiceAddress
    {
        public string street { get; set; }
        public string city { get; set; }
        public string zipCode { get; set; }
        public string countryCode { get; set; }
        public CheckoutFormInvoiceAddressCompany company { get; set; }
        public CheckoutFormInvoiceAddressNaturalPerson naturalPerson { get; set; }

    }
}