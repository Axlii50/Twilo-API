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
using System.Data;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using Wszystko_API;
using Wszystko_API.Categories;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;
using Wszystko_API.Orders.Components;
using Wszystko_API.Shipping;
using static System.Net.WebRequestMethods;
using Wszystko_API.Product;
using Wszystko_API.File;
using System.Net.Mime;
using Wszystko_API.Offers.General_Offer_Model.Components;
using Wszystko_API.Offers.Common_Components;
using Wszystko_API.Offers.Simple_Offer_Model.Interface;

////kempo
//string ClientSecret = "TboT3xZ0fH3F5bEEOR0KDVSugW9iLv9gBBphn8U2aKM2TKp9tgJAEoYu0motWoUU";
//string ClientID = "94a3f5cfe4a1412ea0aa0e90392fb7f4";

////kempopakiety
//string ClientSecret = "7dW5xjpaa6S9tzZvrb4Sw8SSsWL1kuRqf27tCdc1Gnk8a7FhJJCLJ5ClyY0xDXIZ";
//string ClientID = "22054e3f234443a1bdaaa373b06d3053";

////twilo1
string ClientSecret = "PjOcDyDm4ZdjOhrdgOqQQMCY6Row2DWJhnwjjPRAwdQcKLCqpV0fbSjrZ2drQnvf";
string ClientID = "31b0bc689e414c608d7098aa3966f8f4";

////twilo2
//string ClientSecret = "TWGWXn93FMpJg95ILioJvBrYr01pODTfSHfrPY2uX190OD9anosHhMEZrnNQGgXG";
//string ClientID = "41eadd79d2dd475cb5697f3802f01775";

////twilo3
//string ClientSecret = "004VkOAgitQGHYgv6aiW8hLt1F2RpJpi1BxehNe6kIyM4TIbkxVty42hQX4EhaNP";
//string ClientID = "731f01af7c8b46e68ddc12030e4f920c";




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

#region Get orders id for auto order
//var offers = AllegroApi.GetAllOffers();

//var orders = await AllegroApi.GetOrders(Allegro_Api.OrderStatusType.PROCESSING);

//List<string> list = new List<string>();
//foreach (var order in orders)
//	list.Add(order.id.ToString());

//System.IO.File.WriteAllLines("TestTestTEStTwilo1.txt", list.ToArray()); 
#endregion

#region Get Emails

var orders = await AllegroApi.GetOrders(Allegro_Api.OrderStatusType.PROCESSING);
var list = new List<string>();

//var temp = orders.Where(x => x.id == "ee74a380-8de6-11ee-b3a8-6133a263ad6b").FirstOrDefault();

foreach (var order in orders)
{
	Console.Write(order.id + "   " + order.buyer.email + "    " + order.delivery.time.from + "     " + order.delivery.time.dispatch.to);
	if (/*order.delivery.time.from == "2023-11-29T23:00:00Z" || *//*order.delivery.time.dispatch.to == "2023-11-29T22:59:59.999Z"*/ order.status != "NEW" || order.status != "PROCESSING")
	{
		Console.Write("  Added \n");

		list.Add(order.buyer.email);
	}
	else
	{
		Console.Write("\n");
	}
}

System.IO.File.WriteAllText("Maile.txt", list.Aggregate((a, b) => a + ";" + b));
#endregion







//var temp = offers.Result.offers.Where(x => x.id == "14636074368").FirstOrDefault();

//var offer = await AllegroApi.GetDetailedOffer("14550670527");

//WszystkoApi wszystkoApi = new(null);

//var test = await wszystkoApi.GenerateDeviceCode();

//bool authenticate = false;

//Console.WriteLine(test.verificationUriPrettyComplete);

//ProcessStartInfo sInfo2 = new ProcessStartInfo(test.verificationUriPrettyComplete);
//sInfo2.UseShellExecute = true;
//Process Verification2 = Process.Start(sInfo2);

//while (!authenticate)
//{
//	authenticate = await wszystkoApi.CheckForAccessToken();
//	Console.WriteLine(authenticate);
//}

////var test1 = await wszystkoApi.GetAllOffers();
////IDownloadOffersModel[] model = test1.Offers;
////foreach (var model2 in model)
////{
////	System.Diagnostics.Debug.WriteLine(model2.Title);
////}

//var test2 = await wszystkoApi.GetAllGuarantees();
//foreach (var guarantee in test2)
//{
//	Debug.WriteLine($"{ guarantee.Name } { guarantee.GuaranteeDataDetails.Id } { guarantee.GuaranteeDataDetails.ProviderType } {guarantee.GuaranteeDataDetails.ProviderType} { guarantee.AdditionalInformation }\n\n");
//}

Console.ReadLine();