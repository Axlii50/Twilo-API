using AteneumAPI.OrderStructure;
using CsvHelper;
using CsvHelper.Configuration;
using System.Formats.Asn1;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Xml.Serialization;

namespace AteneumAPI
{
    public class AteneumApi
    {
        private static HttpClient _client { get; set; }

        private string UserName { get; set; }
        private string UserPassword { get; set; }

        private string BaseUrl1 { get; init; } = "https://www.ateneum.pl";
        private string BaseUrl2 { get; init; } = "https://ateneum.pl";
        private string BaseUrl3 { get; init; } = "https://www.ateneum.net.pl";

        public AteneumApi(string login, string password)
        {
            UserName = login;
            UserPassword = password;

            _client = new HttpClient();
            _client.Timeout = TimeSpan.FromSeconds(200);
        }

        public async Task<HttpContent> DownloadBase()
        {
            _client.DefaultRequestHeaders.Clear();

            var authenticationString = $"{UserName}:{UserPassword}";
            var base64String = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(authenticationString));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64String);

            HttpResponseMessage response = null;

            do
            {
                try
                {
                    response = await _client.GetAsync($"{BaseUrl1}/bazaksiazek/baza_ksiazek.csv");
                }
                catch (TaskCanceledException e)
                {
                    return null;
                }
                catch (HttpRequestException)
                {
                    return null;
                }
            }while(response == null);

            return response.Content;
        }

        public async Task<HttpContent> DownloadPriceWholeSeller()
        {
            _client.DefaultRequestHeaders.Clear();

            var authenticationString = $"{UserName}:{UserPassword}";
            var base64String = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(authenticationString));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64String);

            var response = await _client.GetAsync($"{BaseUrl2}/{UserName}/ceny.csv");

            return response.Content;
        }

        public async Task<HttpContent> DownloadMagazinAndDetalicPrices()
        {
            _client.DefaultRequestHeaders.Clear();

            var authenticationString = $"{UserName}:{UserPassword}";
            var base64String = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(authenticationString));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64String);

            var response = await _client.GetAsync($"{BaseUrl3}/dbupdate/stanyceny.php");

            return response.Content;
        }

        private async Task<Dictionary<string, BookRecord>> GetAllBookRecords()
        {
            var content = await DownloadBase();
            Dictionary<string,BookRecord> books = null;

            //System.Diagnostics.Debug.WriteLine(content.ReadAsStringAsync().Result);

            using (var sr = new StreamReader(await content.ReadAsStreamAsync(), Encoding.GetEncoding("iso-8859-1")))
            {
                string csv = sr.ReadToEnd();

                //System.Diagnostics.Debug.WriteLine(csv);

                csv = @"ident_ate,EAN,ISBN,Tytuł,autor,wydawnictwo,opis_wydania,rok_wydania,krótka_charakterystyka,cena_detal_netto,stawka_vat,cena_detal_brutto,kategoria_poziom_1,kategoria_poziom_2,kategoria_poziom_3,plik_zdjecia,hash," +
                    csv;

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ",",
                    HasHeaderRecord = true,
                    TrimOptions = TrimOptions.Trim,
                    MissingFieldFound = null,
                    BadDataFound = null
                };

                using (var textreader = new StringReader(csv))
                using (var CsvReader = new CsvReader(textreader, config))
                {
                    books = (CsvReader.GetRecords<BookRecord>())
                        .ToDictionary(b => b.ident_ate);
                }
            }

            Console.WriteLine(books.Count);

            return books;
        }

        private async Task<Dictionary<string, PriceWholeSale>> GetAllWholeSellerPrices()
        {
            var content = await DownloadPriceWholeSeller();
            Dictionary<string, PriceWholeSale> books = null;

            using (var sr = new StreamReader(await content.ReadAsStreamAsync(), Encoding.GetEncoding("iso-8859-1")))
            {
                string csv = sr.ReadToEnd();

                csv = "AteneumID,cena_detaliczna_brutto,cena_hurtowa_netto,vat_procentowy," +
                    csv;

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ",",
                    HasHeaderRecord = true,
                    TrimOptions = TrimOptions.Trim,
                    MissingFieldFound = null,
                    BadDataFound = null

                };

                using (var textreader = new StringReader(csv))
                using (var CsvReader = new CsvReader(textreader, config))
                {
                    books = CsvReader.GetRecords<PriceWholeSale>().ToDictionary(b => b.AteneumID);
                }
            }

            Console.WriteLine(books.Count);

            return books;
        }

        public async Task<List<MagazinAndPrice>> GetMagazinAndDetalicPrices(int minimalMagazineCount)
        {
            var content = await DownloadMagazinAndDetalicPrices();
            List<MagazinAndPriceRecord> States = null;

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
                    States = (CsvReader.GetRecords<MagazinAndPriceRecord>()).ToList();
                }
            }

            List<MagazinAndPrice> Results = new List<MagazinAndPrice>();

            foreach (var item in States)
            {
                int magazin = 0;
                try
                {
                     magazin = int.Parse(item.Stan_magazynowy);
                }catch(FormatException)
                {
                    Console.WriteLine(item.Stan_magazynowy);
                    continue;
                }

                if (magazin >= minimalMagazineCount)
                {
                    MagazinAndPrice magazinAndPrice = null;
                    try
                    {
                        magazinAndPrice = new MagazinAndPrice()
                        {
                            ident_ate = item.ident_ate,
                            MagazinCount = magazin,
                            Cena_detaliczna_brutto = float.Parse(item.Cena_detaliczna_brutto, CultureInfo.InvariantCulture.NumberFormat),
                            Cena_detaliczna_netto = float.Parse(item.Cena_detaliczna_netto, CultureInfo.InvariantCulture.NumberFormat),
                        };
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine(item.Cena_detaliczna_brutto);
                        Console.WriteLine(item.Cena_detaliczna_netto);
                    }

                    Results.Add(magazinAndPrice);
                    magazinAndPrice = null;
                }
            }
            States = null;
            content = null;

            return Results;
        }

        public async Task<List<Book>> GetAllBooks(int minimalMagazineCount)
        {
            var bookrecords = await GetAllBookRecords();
            var magazine = await GetMagazinAndDetalicPrices(minimalMagazineCount);
            var wholesalerprices = await GetAllWholeSellerPrices();

            Console.WriteLine(magazine.Count);

            List<Book> Books = new List<Book>();

            foreach (var state in magazine)
            {
                if (!bookrecords.ContainsKey(state.ident_ate)) continue;
                if (!wholesalerprices.ContainsKey(state.ident_ate)) continue;

                var book = bookrecords[state.ident_ate];
                var pricewhole = wholesalerprices[state.ident_ate];

                Book book1 = new Book()
                {
                    ExternalId = book.ident_ate,
                    EAN = book.EAN,
                    ISBN = book.ISBN,
                    Title = book.Tytuł,
                    Publisher = book.wydawnictwo,
                    opis_wydania = book.opis_wydania,
                    IssueYear = book.rok_wydania,
                    krótka_charakterystyka = book.krótka_charakterystyka,
                    cena_detal_netto = book.cena_detal_netto,
                    stawka_vat = book.stawka_vat,
                    cena_detal_brutto = book.cena_detal_brutto,
                    kategoria_poziom_1 = book.kategoria_poziom_1,
                    kategoria_poziom_2 = book.kategoria_poziom_2,
                    kategoria_poziom_3 = book.kategoria_poziom_3,
                    plik_zdjecia = book.plik_zdjecia,
                    hash = book.hash,
                    MagazinCount = state.MagazinCount,
                    PriceWholeSaleBrutto = pricewhole.cena_hurtowa_netto * ((pricewhole.vat_procentowy / 100f) + 1),
                    PriceWholeSaleNetto = pricewhole.cena_hurtowa_netto,
                    VAT = pricewhole.vat_procentowy
                };
                Books.Add(book1);
                book1 = null;
            }
            return Books;
        }

        public async Task<HttpContent> GetPhoto(string bookid)
        {
            _client.DefaultRequestHeaders.Clear();


            var authenticationString = $"{UserName}:{UserPassword}";
            var base64String = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(authenticationString));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64String);


            HttpResponseMessage odp = await _client.GetAsync($"{BaseUrl3}/dbupdate/imagelarge.php?id={bookid}");

            return odp.Content;
        }

        private string MakeXMLFile(AtesApiOrder order, string password)
        {
            string FileName = DateTime.Now.ToString("yyyy-MM-dd-H-m-ss");

            order.header.buyerorderreference = FileName;
            order.auth.passfingerprint = Sha256(password + order.auth.salt);

            using TextWriter writer = new StreamWriter($"{FileName}.xml");

            XmlSerializer serializer = new XmlSerializer(typeof(AtesApiOrder));

            serializer.Serialize(writer, order);

            return $"{FileName}.xml";
        }

        public async void MakeOrder(AtesApiOrder order, string password)
        {
            if (order == null) return;

            string FileName = MakeXMLFile(order,password);

            if (password == null) return;
            if (password == string.Empty) return;

            _client.DefaultRequestHeaders.Clear();

            string xmlcontent = File.ReadAllText(FileName);

            var dict = new Dictionary<string, string>
            {
                { "xml", xmlcontent }
            };

            var httpContent = new FormUrlEncodedContent(dict);

            var response = await _client.PostAsync($"{BaseUrl3}/sapi/index.php", httpContent);

            var stream = new StreamReader(response.Content.ReadAsStream(), Encoding.UTF8);

            string stringcontent = stream.ReadToEnd();

            Console.WriteLine(stringcontent);
        }

        private string Sha256(string randomString)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}