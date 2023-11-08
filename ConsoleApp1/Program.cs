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
using Wszystko_API.Global_Components;
using Wszystko_API.Offers;
using Wszystko_API.Offers.General_Offer_Model;

//kempo
string ClientSecret = "TboT3xZ0fH3F5bEEOR0KDVSugW9iLv9gBBphn8U2aKM2TKp9tgJAEoYu0motWoUU";
string ClientID = "94a3f5cfe4a1412ea0aa0e90392fb7f4";

//twilo1
//string ClientSecret = "PjOcDyDm4ZdjOhrdgOqQQMCY6Row2DWJhnwjjPRAwdQcKLCqpV0fbSjrZ2drQnvf";
//string ClientID = "31b0bc689e414c608d7098aa3966f8f4";

////twilo3
////string ClientSecret = "004VkOAgitQGHYgv6aiW8hLt1F2RpJpi1BxehNe6kIyM4TIbkxVty42hQX4EhaNP";
////string ClientID = "731f01af7c8b46e68ddc12030e4f920c";




//var AllegroApi = new AllegroApi(ClientID, ClientSecret, null);

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

//var offer = await AllegroApi.GetDetailedOffer("14550670527");

WszystkoApi wszystkoApi = new(null);

var test = await wszystkoApi.GenerateDeviceCode();


Console.ReadLine();