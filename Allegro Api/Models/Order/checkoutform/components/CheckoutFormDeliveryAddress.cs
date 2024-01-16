using System.Text.RegularExpressions;

namespace Allegro_Api.Models.Order.checkoutform.components
{
    public class CheckoutFormDeliveryAddress
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string[] streetAndNumber
        {
            get
            {
                return SplitString(street);
            }
        }

        public string zipCode { get; set; }
        public string countryCode { get; set; }
        public string companyName { get; set; }
        public string phoneNumber { get; set; }
        public string modifiedAt { get; set; }

        string[] SplitString(string input)
        {
            string[] result = new string[2];

            // Wyrażenie regularne do odnalezienia pierwszej spacji i liczby po niej
            Regex regex = new Regex(@"\s+\d.*");

            Match match = regex.Match(input);

            if (match.Success)
            {
                // Grupa 1 zawiera tekst przed spacją, a grupa 2 zawiera liczbę
                result[0] = input.Replace(match.Groups[0].Value, "");
                result[1] = match.Groups[0].Value;
                return result;

            }
            else
            {
                result[0] = input;
                result[1] = string.Empty;
                return result;
            }
            // W przypadku braku dopasowania
            return null;
        }
    }
}