using Allegro_Api;
using Allegro_Api.Models;
using Allegro_Api.Models.Offer;
using Allegro_Api.Models.Product;
using Allegro_Api.Models.Product.ProductComponents;
using AteneumAPI;
using Libre_API;
using Libre_API.OrderStructure;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http.Headers;
using System.Text;

//kempo
//string ClientSecret = "aKgn8GbxJqghLVvqvYpM3Bdlb5eQmCdx6jm2KBybsmSNEfYZtnuHCemwLa5xOvde";
//string ClientID = "0292044ee78a47f2a7f315ece84edfe5";

//string ClientSecret = "PjOcDyDm4ZdjOhrdgOqQQMCY6Row2DWJhnwjjPRAwdQcKLCqpV0fbSjrZ2drQnvf";
//string ClientID = "31b0bc689e414c608d7098aa3966f8f4";

//var AllegroApi = new AllegroApi(ClientID, ClientSecret);

//Allegro_Api.Models.VerificationULRModel t = AllegroApi.Authenticate().Result;

//Console.WriteLine(t.device_code);
//Console.WriteLine(t.verification_uri_complete);

//ProcessStartInfo sInfo = new ProcessStartInfo(t.verification_uri_complete);
//sInfo.UseShellExecute = true;
//Process Verification = Process.Start(sInfo);

//bool access = false;
//while (!access)
//{
//    Allegro_Api.AllegroPermissionState Permissions = AllegroPermissionState.allegro_api_sale_offers_read | AllegroPermissionState.allegro_api_sale_offers_write;

//    access = AllegroApi.CheckForAccessToken(Permissions).Result;

//    Thread.Sleep(5000);
//}
//DateTime dateTime = new DateTime(DateTime.Now.AddDays(-1).Ticks, DateTimeKind.Utc);
//var test = await AllegroApi.GetOrders(dateTime);



//var test = await AllegroApi.GetCategoryParameters("147677");
//foreach (var parameter in test.parameters)
//    Console.WriteLine(parameter.name + "    " + parameter.id);

//Console.WriteLine(dateTime.ToString("o"));

string LibreLogin = "38103_2345";
string LibrePassword = "38103";

var LibreApi = new LibreApi(LibrePassword, LibreLogin);

var books = (await LibreApi.GetAllBooks(2)).First();

DocumentOrder order = new DocumentOrder()
{
    Head = new OrderHead()
    {
        Remarks = "1",
        OrderNumber = "1",
        OrderDate = DateTime.Now.ToString("yyyy-MM-dd"),
        ExpectedDeliveryDate=DateTime.Now.ToString("yyyy-MM-dd")
    },
    parties = new OrderParties()
    {
        Buyer = new Libre_API.OrderStructure.Base()
        {
            ILN = "38103"
        },
        Seller = new Libre_API.OrderStructure.Base()
        {
            ILN = ""
        },
        DeliveryPoint = new Libre_API.OrderStructure.Base()
        {
            ILN = "38103"
        },
    },
    products = new OrderLines()
    {
        Line = new Line[]
        {
            new Line()
            {
                item = new LineItem()
                {
                    LineNumber = 1,
                    EAN = books.EAN,
                    BuyerItemCode= books.ID,
                    ItemDescription = books.Title,
                    OrderQuantity = 1,
                    OrderUnitNetPrice = books.PriceNettoAferDiscount
                }
            }
        }
    },
    summary = new OrderSummary()
    {
        TotalLines = 1,
        TotalOrderedAmount = 1
    }
};


LibreApi.MakeOrder(order);



//foreach (var t in now.GetDateTimeFormats())
//    Console.WriteLine(t);

//
//AllegroApi.CreateOfferSetBasedOnExistingProducts()






Console.ReadLine();