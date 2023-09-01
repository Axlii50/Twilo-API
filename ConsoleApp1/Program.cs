using Allegro_Api;
using Allegro_Api.Models;
using Allegro_Api.Models.Offer;
using Allegro_Api.Models.Product;
using Allegro_Api.Models.Product.ProductComponents;
using AteneumAPI;
using ConsoleApp1;
using Libre_API;
using Libre_API.OrderStructure;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

//kempo
//string ClientSecret = "aKgn8GbxJqghLVvqvYpM3Bdlb5eQmCdx6jm2KBybsmSNEfYZtnuHCemwLa5xOvde";
//string ClientID = "0292044ee78a47f2a7f315ece84edfe5";

//string ClientSecret = "PjOcDyDm4ZdjOhrdgOqQQMCY6Row2DWJhnwjjPRAwdQcKLCqpV0fbSjrZ2drQnvf";
//string ClientID = "31b0bc689e414c608d7098aa3966f8f4";

//twilo3
string ClientSecret = "004VkOAgitQGHYgv6aiW8hLt1F2RpJpi1BxehNe6kIyM4TIbkxVty42hQX4EhaNP";
string ClientID = "731f01af7c8b46e68ddc12030e4f920c";


var AllegroApi = new AllegroApi(ClientID, ClientSecret, null);

Allegro_Api.Models.VerificationULRModel t = AllegroApi.Authenticate().Result;

Console.WriteLine(t.device_code);
Console.WriteLine(t.verification_uri_complete);

ProcessStartInfo sInfo = new ProcessStartInfo(t.verification_uri_complete);
sInfo.UseShellExecute = true;
Process Verification = Process.Start(sInfo);

bool access = false;
while (!access)
{
    Allegro_Api.AllegroPermissionState Permissions = AllegroPermissionState.allegro_api_sale_offers_read | AllegroPermissionState.allegro_api_sale_offers_write;

    access = AllegroApi.CheckForAccessToken(Permissions).Result;

    Thread.Sleep(5000);
}

//AllegroApi.RefreshAccesToken();

//var response = await AllegroApi.ChangeOrderStatus(OrderStatusType.PROCESSING, "3994a290-471d-11ee-abad-8d464aa2c811");

//Console.WriteLine(response.ReadAsStringAsync().Result);
//Console.ReadLine();

//DateTime dateTime = new DateTime(DateTime.Now.AddDays(-1).Ticks, DateTimeKind.Utc);
//var test = await AllegroApi.GetOrders(dateTime);

//    offerids.RemoveRange(0,500);
//}


//var test = await AllegroApi.GetCategoryParameters("147677");
//foreach (var parameter in test.parameters)
//    Console.WriteLine(parameter.name + "    " + parameter.id);

//Console.WriteLine(dateTime.ToString("o"));

//string LibreLogin = "38103_2345";
//string LibrePassword = "38103";

//var LibreApi = new LibreApi(LibrePassword, LibreLogin);

//var books = (await LibreApi.GetAllBooks(2));

//foreach (var book in books)
//    if(book.ID== "184649")
//    {
//        Console.WriteLine("test");
//    }

//DocumentOrder order = new DocumentOrder()
//{
//    Head = new OrderHead()
//    {
//        Remarks = "1",
//        OrderNumber = "1",
//        OrderDate = DateTime.Now.ToString("yyyy-MM-dd"),
//        ExpectedDeliveryDate=DateTime.Now.ToString("yyyy-MM-dd")
//    },
//    parties = new OrderParties()
//    {
//        Buyer = new Libre_API.OrderStructure.Base()
//        {
//            ILN = "38103"
//        },
//        Seller = new Libre_API.OrderStructure.Base()
//        {
//            ILN = ""
//        },
//        DeliveryPoint = new Libre_API.OrderStructure.Base()
//        {
//            ILN = "38103"
//        },
//    },
//    products = new OrderLines()
//    {
//        Line = new Line[]
//        {
//            new Line()
//            {
//                item = new LineItem()
//                {
//                    LineNumber = 1,
//                    EAN = books.EAN,
//                    BuyerItemCode= books.ID,
//                    ItemDescription = books.Title,
//                    OrderedQuantity = 1,
//                    OrderUnitNetPrice = books.PriceNettoAferDiscount
//                }
//            }
//        }
//    },
//    summary = new OrderSummary()
//    {
//        TotalLines = 1,
//        TotalOrderedAmount = 1
//    }
//};


//LibreApi.MakeOrder(order);
//9788366335875
//2023-08-09-20-2.xml

//string ftpUserName = "kempogroup",ftpPassword = "trz2Q_7", FileName = "2023-08-09-20-2.xml";

//using (var client = new WebClient())
//{
//    client.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
//    var response = client.UploadFile($"ftp://83.142.195.2/{FileName}", WebRequestMethods.Ftp.UploadFile, FileName);

//    string bitString = BitConverter.ToString(response);

//    Console.WriteLine(bitString);
//}


//var url = "ftp://83.142.195.2";
//FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
//request.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
//request.Method = WebRequestMethods.Ftp.UploadFile;

//using (Stream fileStream = File.OpenRead(FileName))
//using (Stream ftpStream = request.GetRequestStream())
//{
//    byte[] buffer = new byte[10240];
//    int read;
//    while ((read = fileStream.Read(buffer, 0, buffer.Length)) > 0)
//    {
//        ftpStream.Write(buffer, 0, read);
//        Console.WriteLine("Uploaded {0} bytes", fileStream.Position);
//    }
//}


//AteneumApi ate = new AteneumApi("kempo_warszawa", "6KsSGWT6dhD9r8Xvvr");


//var boks = await ate.GetAllBooksWithMagazin(2);

//foreach(var test in boks)
//{
//   // if(test.BookData.EAN == "9788366335875")
//    if(test.BookData.EAN == "9788367406505")
//    {
//        Console.WriteLine(test.PriceWholeSaleBrutto);
//    }
//}
//39.1439972
//37.28

//foreach (var t in now.GetDateTimeFormats())
//    Console.WriteLine(t);

//
//AllegroApi.CreateOfferSetBasedOnExistingProducts()

//Config config = new Config();
//config.rangeMargins = new List<RangeMargin>();
//config.rangeMargins.Add(new RangeMargin()
//{
//    lowerbound = 1f,
//    upperbound = float.MaxValue,
//    margin = 0.5f,
//    Addmargin = false
//});

//string jsonstring = JsonConvert.SerializeObject(config, Formatting.Indented);

//File.WriteAllText("Config.json", jsonstring);


//AteneumAPI.OrderStructure.AtesApiOrder atesApiOrder = new AteneumAPI.OrderStructure.AtesApiOrder()
//{
//    Auth = new AteneumAPI.OrderStructure.Auth()
//    {
//        Login = "sapiTestUser",
//        salt = "111"
//    },
//    header = new AteneumAPI.OrderStructure.Header()
//    {
//        deliveryaddresscode = "ZZ05",
//        buyerorderreference = "111-111-111-111",
//        remarks = "uwagi do zamowienia"
//    },
//    lines = new AteneumAPI.OrderStructure.Line[]
//    {
//        new AteneumAPI.OrderStructure.Line()
//        {
//            ateid = 413541,
//            quantity = 10,
//            buyerproductreference = "413541-2"
//        }
//    }
//};


//ate.MakeOrder(atesApiOrder, "sapiTestUser", "ALWZywNWMc");


Console.ReadLine();