using Allegro_Api;
using Allegro_Api.Models;
using Allegro_Api.Models.Offer;
using Allegro_Api.Models.Product;
using Allegro_Api.Models.Product.ProductComponents;
using AteneumAPI;
using Libre_API;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http.Headers;
using System.Text;

//kempo
string ClientSecret = "aKgn8GbxJqghLVvqvYpM3Bdlb5eQmCdx6jm2KBybsmSNEfYZtnuHCemwLa5xOvde";
string ClientID = "0292044ee78a47f2a7f315ece84edfe5";

//string ClientSecret = "PjOcDyDm4ZdjOhrdgOqQQMCY6Row2DWJhnwjjPRAwdQcKLCqpV0fbSjrZ2drQnvf";
//string ClientID = "31b0bc689e414c608d7098aa3966f8f4";

var AllegroApi = new AllegroApi(ClientID, ClientSecret);

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
//DateTime dateTime = new DateTime(DateTime.Now.AddDays(-1).Ticks, DateTimeKind.Utc);
//var test = await AllegroApi.GetOrders(dateTime, OrderStatusType.SENT);

//var offers = await AllegroApi.GetAllOffers(OfferState.ACTIVE);
//List<Base> offerids = new List<Base>();

//foreach(var offer in offers.offers)
//{
//    Base temp = new Base()
//    {
//        id = offer.id
//    };
//    offerids.Add(temp);
//}

//while(offerids.Count > 0)
//{
//    var portion = offerids.Take(500);

//    await AllegroApi.BatchChangePublication(portion.ToArray(),false);

//    offerids.RemoveRange(0,500);
//}

//Console.WriteLine(dateTime.ToString("o"));

//string LibreLogin = "38103_2345";
//string LibrePassword = "38103";

//var LibreApi = new LibreApi(LibrePassword, LibreLogin);

//var books = LibreApi.GetAllBooks(2);




//foreach (var t in now.GetDateTimeFormats())
//    Console.WriteLine(t);

//
//AllegroApi.CreateOfferSetBasedOnExistingProducts()






Console.ReadLine();