using CsvHelper;
using CsvHelper.Configuration;
using System.Formats.Asn1;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;

namespace AteneumAPI
{
    public class AteneumApi
    {
        public static HttpClient _client { get; private set; }

        string userName = "kempo_warszawa";
        string userPassword = "6KsSGWT6dhD9r8Xvvr";

        public AteneumApi()
        {
            _client = new HttpClient();
        }


        public async Task<HttpContent> DownloadBase()
        {
            var _client = new HttpClient();

            _client.DefaultRequestHeaders.Clear();

            var authenticationString = $"{userName}:{userPassword}";
            var base64String = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(authenticationString));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64String);

            var response = await _client.GetAsync("https://www.ateneum.pl/bazaksiazek/baza_ksiazek.csv");

            return response.Content;
        }
        
        public async Task<HttpContent> DownloadMagazinAndDetalicPrices()
        {
            var _client = new HttpClient();

            _client.DefaultRequestHeaders.Clear();

            var authenticationString = $"{userName}:{userPassword}";
            var base64String = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(authenticationString));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64String);

            var response = await _client.GetAsync("https://www.ateneum.net.pl/dbupdate/stanyceny.php");

            return response.Content;
        }


        
        public async Task<List<Book>> GetAllBooks()
        {
            var content = await DownloadBase();
            List<Book> books = null;

            using (var sr = new StreamReader(await content.ReadAsStreamAsync(), Encoding.GetEncoding("iso-8859-1")))
            {
                string csv = sr.ReadToEnd();

                csv = "ident_ate,EAN,ISBN,Tytuł,autor,wydawnictwo,opis_wydania,rok_wydania,krótka_charakterystyka,cena_detal_netto,stawka_vat,cena_detal_brutto,kategoria_poziom_1,kategoria_poziom_2,kategoria_poziom_3,plik_zdjecia,hash," +
                    csv;

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ",",
                    HasHeaderRecord = true,
                    TrimOptions = TrimOptions.Trim,
                    MissingFieldFound = null
                };

                using (var textreader = new StringReader(csv))
                using (var CsvReader = new CsvReader(textreader, config))
                {
                    books = (CsvReader.GetRecords<Book>()).ToList();
                }
            }

            Console.WriteLine(books.Count);

            return books;
        }
        
        public async Task<List<MagazinAndPrice>> GetMagazinAndDetalicPrices()
        {
            var content = await DownloadMagazinAndDetalicPrices();
            List<MagazinAndPrice> States = null;

            using (var sr = new StreamReader(await content.ReadAsStreamAsync(), Encoding.GetEncoding("iso-8859-1")))
            {
                string csv = sr.ReadToEnd();

                csv = "ident_ate,Stan_magazynowy,Cena_detaliczna_netto,Cena_detaliczna_brutto,Stawka_VAT," +
                    csv;

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ",",
                    HasHeaderRecord = true,
                    TrimOptions = TrimOptions.Trim,
                    MissingFieldFound = null
                };

                using (var textreader = new StringReader(csv))
                using (var CsvReader = new CsvReader(textreader, config))
                {
                    States = (CsvReader.GetRecords<MagazinAndPrice>()).ToList();
                }
            }

            Console.WriteLine(States.Count);

            return States;
        }
    }
}