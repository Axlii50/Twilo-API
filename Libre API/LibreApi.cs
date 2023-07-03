using Csv;
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

            var books = (Books)serializer.Deserialize(rd);
            rd.Close();

            //System.Diagnostics.Debug.WriteLine(books.book.Length);
            return books.book.Where(book => book.MagazineCount >= minimalMagazineCount).ToList();
        }

        public async Task<HttpContent> GetPhoto(string bookid)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

            HttpResponseMessage odp = await client.GetAsync(UrlPhoto + $"?login={login}&password={password}&foto={bookid}");

            return odp.Content;
        }
    }
}