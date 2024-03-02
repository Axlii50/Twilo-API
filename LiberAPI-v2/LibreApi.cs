using Csv;
using Libre_API.OrderStructure;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Serialization;

namespace Libre_API
{
    public class LibreApi
    {
        private HttpClient _client { get; set; }

        private string Password { get; set; } = string.Empty;
        private string Login { get; set; } = string.Empty;

        private string UrlPhoto { get; init; } = "http://xml.liber.pl/foto.aspx";
        private string UrlDane2 { get; init; } = "http://xml.liber.pl/dane2.aspx";

        public LibreApi(string login, string password)
        {
            this.Password = password;
            this.Login = login;
            _client = new HttpClient();
        }

        private async Task<HttpContent> DownloadDane2()
        {
            _client.DefaultRequestHeaders.Clear();

            //pewnie tego tutaj nie musi byc ale jebac, działa
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

            HttpResponseMessage odp = null;
            try
            {
                odp = await _client.GetAsync(UrlDane2 + $"?login={Login}&password={Password}");
            }
            catch (TaskCanceledException e)
            {
                return null;
            }
            catch (HttpRequestException)
            {
                return null;
            }

            return odp.Content;
        }

        public async Task<List<Book>> GetAllBooks(int minimalMagazineCount)
        {
            HttpContent Data = null;

            while (Data == null)
                Data = await DownloadDane2();

            XmlSerializer serializer = new XmlSerializer(typeof(Books));

            StreamReader rd = new StreamReader(Data.ReadAsStream(), Encoding.UTF8);

            Books books = null;
            try
            {
                 books = (Books)serializer.Deserialize(rd);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine(Data.ReadAsStringAsync().Result);
                return null;
            }
            rd.Close();

            if (books == null) return null;
            if (books.book == null) return null;

            //File.WriteAllText("Liber.xml", Data.ReadAsStringAsync().Result);

            Data = null;
            return books.book.Where(book => book.MagazineCount >= minimalMagazineCount).ToList();
        }

        public async Task<List<Book>> GetAllBooksFromFile(int minimalMagazineCount)
        {
            HttpContent Data = null;

            while (Data == null)
                Data = await DownloadDane2();

            XmlSerializer serializer = new XmlSerializer(typeof(Books));

            StreamReader rd = new StreamReader("Liber.xml", Encoding.UTF8);


            Books books = null;
            try
            {
                books = (Books)serializer.Deserialize(rd);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine(Data.ReadAsStringAsync().Result);
                return null;
            }
            rd.Close();

            if (books == null) return null;
            if (books.book == null) return null;

            //File.WriteAllText("Liber.xml", Data.ReadAsStringAsync().Result);

            Data = null;
            return books.book.Where(book => book.MagazineCount >= minimalMagazineCount).ToList();
        }

        private string MakeXMLFile(DocumentOrder order)
        {
            string FileName = DateTime.Now.ToString("yyyy-MM-dd-H-m-ss");
            using TextWriter writer = new StreamWriter($"{FileName}.xml");

            XmlSerializer serializer = new XmlSerializer(typeof(DocumentOrder));

            serializer.Serialize(writer, order);

            return $"{FileName}.xml";
        }

        public async void MakeOrder(DocumentOrder order, string ftpUserName, string ftpPassword)
        {
            if (order == null) return;

            string FileName = MakeXMLFile(order);

            if(ftpUserName == null || ftpPassword == null) return;
            if(ftpUserName == string.Empty || ftpPassword == string.Empty) return;

            using (var client = new WebClient())
            {
                client.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
                _ = client.UploadFile($"ftp://83.142.195.2/{(ftpUserName == "twilo" ? "Zamówienia/Twilo/" : "")}{FileName}", WebRequestMethods.Ftp.UploadFile, FileName);
            }
        }

        public async Task<HttpContent> GetPhoto(string bookid)
        {
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

            HttpResponseMessage odp = null;
            try
            {
                 odp = await _client.GetAsync(UrlPhoto + $"?login={Login}&password={Password}&foto={bookid}");
            }
            catch (TaskCanceledException)
            {
                return null;
            }
            catch (HttpRequestException)
            {
                return null;
            }

            return odp.Content;

        }
    }
}