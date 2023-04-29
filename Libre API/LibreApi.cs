using Csv;

namespace Libre_API
{
    public class LibreApi
    {
        private string password = string.Empty;
        private string login = string.Empty;

        private string UrlDaneRaport = "http://xml.liber.pl/daneRaport.aspx";

        public LibreApi(string password, string login)
        {
            this.password = password;
            this.login = login;
        }

        private async Task<HttpContent> DownloadDaneRaport()
        {
            HttpClient client = new HttpClient();

            HttpResponseMessage odp = await client.GetAsync(UrlDaneRaport + $"?login={login}&password={password}");

            return odp.Content;
        }

        public async Task<List<Book>> GetAllBooks(int minimalMagazineCount)
        {
            var Data = DownloadDaneRaport().Result;

            List<Book> books = new List<Book>();
            foreach (var st in CsvReader.ReadFromStream(Data.ReadAsStream()))
            {
                var book = StringToBook(st.Raw, minimalMagazineCount);

                if (book != null) books.Add(book);
            }

            System.Diagnostics.Debug.WriteLine(books.Count);

            return books;
        }

        public Book StringToBook(string str, int minimal)
        {
            string[] fields = null;
            fields = str.Split(';');
            Book book = null;
            try
            {
                if (int.Parse(fields[2]) <= minimal) return null;

                 book = new Book();
                book.EAN = fields[0];
                book.ISBN = fields[1];
                book.MagazineCount = int.Parse(fields[2]);
                book.Group = fields[3];
                book.Title = fields[4];
                book.OriginalTitle = fields[5];
                book.Author = fields[6];
                book.Transaltor = fields[7];
                book.Publisher = fields[8];
                book.Series = fields[9];
                book.PriceBruttoAferDiscount = float.Parse(fields[10].Replace('.', ','));
                book.PriceNettoAferDiscount = float.Parse(fields[11].Replace('.', ','));
                book.PriceBrutto = float.Parse(fields[12].Replace('.', ','));
                book.Vat = int.Parse(fields[13].Replace("%", ""));
                book.PublishDate = fields[14].IsEmpty() ? DateOnly.MinValue : DateOnly.Parse(fields[14]);
                book.SaleDate = fields[15].IsEmpty() ? DateOnly.MinValue : DateOnly.Parse(fields[15]);
                book.YearOfPublish = fields[16].IsEmpty() ? -1 : int.Parse(fields[16]);
                book.NumberOfPublish = fields[17].IsEmpty() ? -1 : int.Parse(fields[17]);
                book.Category = fields[18];
                book.Cover = fields[19];
                book.NumberOfPages = fields[20].IsEmpty() ? -1 : int.Parse(fields[20]);
                book.Height = fields[21].IsEmpty() ? -1 : int.Parse(fields[21]);
                book.Width = fields[22].IsEmpty() ? -1 : int.Parse(fields[22]);
                book.Thickness = fields[23].IsEmpty() ? -1f : float.Parse(fields[23].Replace('.', ','));
                book.Weight = fields[24].IsEmpty() ? -1f : float.Parse(fields[24].Replace('.', ','));


            }
            catch (IndexOutOfRangeException)
            {
                
            }
            catch (FormatException)
            {
                return null;
            }
            return book;
        }
    }
}