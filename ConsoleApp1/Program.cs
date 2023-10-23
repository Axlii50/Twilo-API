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
string ClientSecret = "aKgn8GbxJqghLVvqvYpM3Bdlb5eQmCdx6jm2KBybsmSNEfYZtnuHCemwLa5xOvde";
string ClientID = "0292044ee78a47f2a7f315ece84edfe5";

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

//var offers = await AllegroApi.GetAllOffers(OfferState.ACTIVE);

////DateTime now = DateTime.Now;
////now = now.AddHours(-22);
////DateTime Formatted = new DateTime(now.Ticks, DateTimeKind.Utc);
////Formatted = Formatted.AddHours(-3);

////var orders = await Program.AllegroApi.GetOrders(Formatted,OrderStatusType.NEW);
////var orders = await AllegroApi.GetOrders(OrderStatusType.NEW);
////var orders = (await AllegroApi.GetOrders(OrderStatusType.ac));

//List<Allegro_Api.Models.Base> offersid = new List<Allegro_Api.Models.Base>();


//foreach (var order in offers.offers)
//{
//    if (order.external == null) continue;
//    if (order.external.id == "null")
//    {
//        offersid.Add(new Allegro_Api.Models.Base() { id = order.id });
//        Console.WriteLine("Disabled");
//    }

//    if (offersid.Count == 100)
//    {
//        var temp = offersid.Take(100).ToArray();
//        Console.WriteLine("Aktywuje: " + temp.Length);
//        var response = await AllegroApi.BatchChangePublication(offersid.ToArray(), false);
//        Console.WriteLine(response.Item1.Content.ReadAsStringAsync().Result);

//        offersid.Clear();
//    }
//}

//var temp2 = offersid.Take(100).ToArray();
//Console.WriteLine("Aktywuje: " + temp2.Length);
//var response1 = await AllegroApi.BatchChangePublication(offersid.ToArray(), false);
//Console.WriteLine(response1.Item1.Content.ReadAsStringAsync().Result);

//offersid.Clear();

//File.WriteAllLines("test.txt",ordersid.ToArray());

//#region Refaktryzacja bez sygnatury do wyniesienia do innego projektu
//DateTime dateTime = new DateTime(DateTime.Now.AddDays(-46).Ticks, DateTimeKind.Utc);
//var test = await AllegroApi.GetOrders(OrderStatusType.SENT);

//var test2 = test.Where(o => o.lineItems.Any(li => li.offer.external == null)).ToList();

//Console.WriteLine(test2.Count);

//List<string> listedentities = new List<string>();

//int count = 0;

//foreach (var order in test2)
//{
//    foreach (var lineitem in order.lineItems)
//    {
//        DateTime MonthAfter = DateTime.Now.AddMonths(-1);

//        DateTime orderDate = DateTime.Parse(lineitem.boughtAt);
//        //DateTime normaltime = new DateTime(orderDate.Ticks, DateTimeKind.Local);
//        count++;
//        if (MonthAfter.Month != orderDate.Month)
//        {
//            Console.WriteLine(MonthAfter.Month + " " + orderDate.Month + "       " + lineitem.boughtAt);
//            continue;
//        }

//        Console.WriteLine(MonthAfter.Month + "    " + orderDate.Month);

//        if (lineitem.offer.external != null)
//        {
//            Console.WriteLine("skipped = " + lineitem.offer.external.id);
//            continue;
//        }

//        Console.WriteLine(order.id);

//        var detailed = await AllegroApi.GetDetailedOffer(lineitem.offer.id);

//        listedentities.Add($"{lineitem.quantity}    {lineitem.originalPrice.amount}    {detailed.name}       {lineitem.boughtAt}     {order.id}");


//        //Console.WriteLine("t");
//    }
//}

//Console.WriteLine(count);

//File.WriteAllLines("Testowyplik.txt", listedentities.ToArray());
//#endregion


//Console.WriteLine($"Test {test.count}");


// test = await AllegroApi.GetAllOffers(OfferState.ACTIVE);

//Console.WriteLine($"Test {test.count}");
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


//System.Diagnostics.Debug.WriteLine(ClientSecret);

AteneumApi ate = new AteneumApi("kempo_warszawa", "6KsSGWT6dhD9r8Xvvr");

var test = await ate.GetAllBooksWithMagazin(5);

Console.WriteLine(test);

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
//    auth = new AteneumAPI.OrderStructure.Auth()
//    {
//        Login = "sapiTestUser",
//        salt = "111"
//    },
//    header = new AteneumAPI.OrderStructure.Header()
//    {
//        deliveryaddresscode = "ZZ05",
//        buyerorderreference = "222-111-111-111",
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


//ate.MakeOrder(atesApiOrder, "ALWZywNWMc");


//WszystkoApi wszystkoApi = new WszystkoApi(null, null, null);

//var test = await wszystkoApi.GenerateDeviceCode();

//bool authenticate = false;

//Console.WriteLine(test.verificationUriPrettyComplete);

//ProcessStartInfo sInfo = new ProcessStartInfo(test.verificationUriPrettyComplete);
//sInfo.UseShellExecute = true;
//Process Verification = Process.Start(sInfo);

//while (!authenticate)
//{
//    authenticate = await wszystkoApi.CheckForAccessToken();
//    Console.WriteLine(authenticate);
//}

//var test0 = await wszystkoApi.GetSessions();
//foreach (var session in test0)
//{
//	Console.WriteLine($"{session.Id} {session.UserName}");
//}

//DownloadOfferArrayModel test1 = await wszystkoApi.GetAllOffers();
//foreach (Wszystko_API.Offers.Simple_Offer_Model.Interface.IDownloadOffersModel offer in test1.Offers)
//{
//    Console.WriteLine($"{offer.Title} {offer.Id} {offer.Price}");
//}

//ShippingModel[] test11 = await wszystkoApi.GetShippingMethods();
//foreach (var shippingModel in test11)
//{
//	Console.WriteLine(shippingModel.Name);
//}

//ShippingTariffModel[] test12 = await wszystkoApi.GetAllShippingTariffs();
//foreach (var shippingTariff in test12)
//{
//	Console.WriteLine($"{shippingTariff.Name} {shippingTariff.Id}");
//}



//------------------------------------------------------
//byte[] binaryData = System.IO.File.ReadAllBytes($"D:/Pobrane/czlowiek-w-pozukiwaniu-sensu-viktor-e-frankl-24h.png");
//BinaryFileResponse x = await wszystkoApi.AddBinaryFile(binaryData);

//var shippingPolicies = await wszystkoApi.GetAllShippingTariffs();
//var complaintPolicies = await wszystkoApi.GetAllComplaintsPolicies();
//var returnPolicies = await wszystkoApi.GetAllReturnPolicies();

//Item[] descriptionItem = new Item[]
//{
//	new Item
//	{
//		ContentType = ContentTypeType.text.ToString(),
//		Value = "Your Text Value"
//	}
//};

//Description[] descriptions = new Description[]
//{
//	new Description
//	{
//		Items = descriptionItem
//	}
//};

//ParameterKit[] parameters = new ParameterKit[]
//{
//	new ParameterKit
//	{
//		Id = 1,
//		Value = 5
//	}
//};

//RequestAddProductOffer product = new RequestAddProductOffer()
//{
//	Title = "tytuł",
//	Price = 10,
//	CategoryId = 96,
//	Gallery = new Uri[] { x.Url },
//	VatRate = VatRateType.zero.VatRateToString(),
//	LeadTime = LeadTimeType.Natychmiast.LeadTimeToString(),
//	StockQuantityUnit = StockQuantityUnitType.sztuk.StockQuantityUnitTypeToString(),
//	OfferStatus = Wszystko_API.General_Offer_Model.Components.OfferStatusType.blocked.ToString(),
//	IsDraft = true,
//	UserQuantityLimit = 50,
//	StockQuantity = 50,
//	GuaranteeId = null,
//	ComplaintPolicyId = complaintPolicies[0].Id.ToString(),
//	ReturnPolicyId = returnPolicies[0].Id.ToString(),
//	ShippingTariffId = shippingPolicies[0].Id.ToString(),
//	Parameters = parameters,
//	Descriptions = descriptions,
//	ShowUnitPrice = true
//};

//var test3 = await wszystkoApi.CreateOffer(product);
//-----------------------------------------------------------------

//var test4 = await wszystkoApi.GetOfferData("1006723824");
//System.Diagnostics.Debug.WriteLine(test4.OfferLink);

//UpdateOfferModel updateOfferModel = new UpdateOfferModel()
//{
//	Title = "xdxd",
//	Price = 21.37,
//	CategoryId = 96,
//	Gallery = new Uri[] { },
//	VatRate = VatRateType.five.VatRateToString(),
//	LeadTime = LeadTimeType.Natychmiast.LeadTimeToString(),
//	StockQuantityUnit = StockQuantityUnitType.sztuk.StockQuantityUnitTypeToString(),
//	OfferStatus = Wszystko_API.General_Offer_Model.Components.OfferStatusType.blocked.ToString(),
//	IsDraft = true,
//	UserQuantityLimit = 50,
//	StockQuantity = 50,
//	GuaranteeId = null,
//	ComplaintPolicyId = complaintPolicies[0].Id.ToString(),
//	ReturnPolicyId = returnPolicies[0].Id.ToString(),
//	ShippingTariffId = shippingPolicies[0].Id.ToString(),
//	Parameters = parameters,
//	Descriptions = descriptions,
//	ShowUnitPrice = true
//};
//var test14 = await wszystkoApi.UpdateOfferData(1007020828, updateOfferModel);
//System.Diagnostics.Debug.WriteLine(test14);


//var test5 = await wszystkoApi.GetAllOrders();
//foreach (var order in test5.simpleOrderModels)
//{
//	Type type = order.GetType();
//	PropertyInfo[] properties = type.GetProperties();

//	foreach (PropertyInfo property in properties)
//	{
//		object value = property.GetValue(order);
//		System.Diagnostics.Debug.WriteLine(value);
//	}
//}

//var test6 = await wszystkoApi.GetOrderWithId("");
//Type type = test6.GetType();
//PropertyInfo[] properties = type.GetProperties();
//foreach (PropertyInfo property in properties)
//{
//    object value = property.GetValue(test6);
//    System.Diagnostics.Debug.WriteLine(value);
//}

//var test7 = await wszystkoApi.GetWaybillsAddedToOrder("");
//System.Diagnostics.Debug.WriteLine(test7);

//var test8 = await wszystkoApi.GetCategoryTreeAndAllParameters();
//StringBuilder sb = new StringBuilder();
//foreach (Wszystko_API.Categories.CategoryBatchInTree categoryBatch in test8)
//{
//	foreach (Wszystko_API.Categories.CategoryInTree category in categoryBatch.Categories)
//	{
//		sb.AppendLine(category.Name + " " + category.Id + category.);
//	}
//}

//System.IO.File.WriteAllText(@"C:\Users\ATEM\source\repos\Axlii50\categories.txt", test8.ToString());
//using (StreamWriter writer = new StreamWriter(@"C:\Users\ATEM\source\repos\Axlii50\categories.txt"))
//{
//	writer.Write(sb.ToString());
//}

//var testx = await wszystkoApi.GetCategoryTreeAndAllParameters(8829);


//StringBuilder sb = new StringBuilder();
//using (StreamWriter writer = new StreamWriter(@"C:\Users\ATEM\source\repos\Axlii50\categories.txt"))
//{
//	var test9 = await wszystkoApi.GetCategoriesByLevel(0);

//	foreach (Wszystko_API.Categories.Category category in test9)
//	{
//		sb.AppendLine($"{category.Name}\t{category.Id}\t{category.ParentId}\t{category.UrlPart}");
//	}
//	writer.WriteLine(sb.ToString());
//}

//var test10 = await wszystkoApi.GetShippingMethods();
//foreach (ShippingModel model in test10)
//{
//    Console.WriteLine($"{model.Id} {model.Name} {model.Shipping} {model.logoUri} {model.AvailableShippingMethodOptions.AdvancePayment} {model.AvailableShippingMethodOptions.CashOnDelivery} {model.MinShippingDays} {model.MaxShippingDays} {model.EarliestEstimatedShippingDate} {model.LatestEstimatedShippingDate}");
//}


//TESTY BEZ ARGUMENTÓW OPCJONALNYCH W REQUESTACH

//Shipping:
//DZIAŁA: GetShippingMethods()

//OFFERS:
//DZIAŁA: GetSessions(), GetAllOffers(), GetOfferData("1006723824"), CreateOffer()

//ORDERS:
//RACZEJ DZIAŁA:GetOrderWithId("")
//DO PRZETESTOWANIA: GetWaybillsAddedToOrder(""), GetAllOrders()


Console.ReadLine();