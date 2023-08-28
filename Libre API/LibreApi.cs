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
        private string password = string.Empty;
        private string login = string.Empty;

        private string UrlPhoto = "http://xml.liber.pl/foto.aspx";
        private string UrlDane2 = "http://xml.liber.pl/dane2.aspx";

        public LibreApi(string login, string password)
        {
            this.password = password;
            this.login = login;
        }

        private async Task<HttpContent> DownloadDane2()
        {
            HttpClient client = new HttpClient();

            //pewnie tego tutaj nie musi byc ale jebac, działa
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

            HttpResponseMessage odp = null;
            try
            {
                odp = await client.GetAsync(UrlDane2 + $"?login={login}&password={password}");
            }
            catch (TaskCanceledException e)
            {
                return null;
            }
            catch (HttpRequestException)
            {
                return null;
            }

            //System.Diagnostics.Debug.WriteLine(odp.Content.ReadAsStringAsync().Result);

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
                _ = client.UploadFile($"ftp://83.142.195.2/{FileName}", WebRequestMethods.Ftp.UploadFile, FileName);
            }
        }

        public async Task<HttpContent> GetPhoto(string bookid)
        {
            using HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

            HttpResponseMessage odp = null;
            try
            {
                 odp = await client.GetAsync(UrlPhoto + $"?login={login}&password={password}&foto={bookid}");
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