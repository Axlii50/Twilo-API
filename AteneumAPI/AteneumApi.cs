﻿using CsvHelper;
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

        string userName;
        string userPassword;

        public AteneumApi(string login, string password)
        {
            userName = login;
            userPassword = password;

            _client = new HttpClient();
        }

        public async Task<HttpContent> DownloadBase()
        {
           

            _client.DefaultRequestHeaders.Clear();

            var authenticationString = $"{userName}:{userPassword}";
            var base64String = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(authenticationString));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64String);

            var response = await _client.GetAsync("https://www.ateneum.pl/bazaksiazek/baza_ksiazek.csv");

            return response.Content;
        }

        public async Task<HttpContent> DownloadMagazinAndDetalicPrices()
        {
           

            _client.DefaultRequestHeaders.Clear();

            var authenticationString = $"{userName}:{userPassword}";
            var base64String = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(authenticationString));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64String);

            var response = await _client.GetAsync("https://www.ateneum.net.pl/dbupdate/stanyceny.php");

            return response.Content;
        }

        private async Task<Dictionary<string, BookRecord>> GetAllBooks()
        {
            var content = await DownloadBase();
            Dictionary<string,BookRecord> books = null;

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
                    books = (CsvReader.GetRecords<BookRecord>()).ToDictionary(b => b.ident_ate);
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
                int magazin = int.Parse(item.Stan_magazynowy);

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

        public async Task<List<Book>> GetAllBooksWithMagazin(int minimalMagazineCount)
        {
            var bookrecords = await GetAllBooks();
            var magazine = await GetMagazinAndDetalicPrices(minimalMagazineCount);

            Console.WriteLine(magazine.Count);

            List<Book> Books = new List<Book>();

            foreach(var state in magazine)
            {
                if (!bookrecords.ContainsKey(state.ident_ate)) continue;

                var book = bookrecords[state.ident_ate];

                Book book1 = new Book()
                {
                    ident_ate = book.ident_ate,
                    MagazinCount = state.MagazinCount,
                    Cena_detaliczna_brutto = state.Cena_detaliczna_brutto,
                    Cena_detaliczna_netto = state.Cena_detaliczna_netto,
                    BookData = book,
                };
                Books.Add(book1);
                book1 = null;
            }
            return Books;
        }

        public async Task<HttpContent> GetPhoto(string bookid)
        {
            _client.DefaultRequestHeaders.Clear();

            var authenticationString = $"{userName}:{userPassword}";
            var base64String = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(authenticationString));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64String);


            HttpResponseMessage odp = await _client.GetAsync($"https://www.ateneum.net.pl/dbupdate/imagelarge.php?id={bookid}");

            return odp.Content;
        }

    }
}