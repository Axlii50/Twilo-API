using Allegro_Api;
using Allegro_Api.Shipment;
using AteneumAPI;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

////kempo
//string ClientSecret = "TboT3xZ0fH3F5bEEOR0KDVSugW9iLv9gBBphn8U2aKM2TKp9tgJAEoYu0motWoUU";
//string ClientID = "94a3f5cfe4a1412ea0aa0e90392fb7f4";

////kempopakiety
//string ClientSecret = "7dW5xjpaa6S9tzZvrb4Sw8SSsWL1kuRqf27tCdc1Gnk8a7FhJJCLJ5ClyY0xDXIZ";
//string ClientID = "22054e3f234443a1bdaaa373b06d3053";

////twilo1
//string ClientSecret = "PjOcDyDm4ZdjOhrdgOqQQMCY6Row2DWJhnwjjPRAwdQcKLCqpV0fbSjrZ2drQnvf";
//string ClientID = "31b0bc689e414c608d7098aa3966f8f4";

////twilo2
//string ClientSecret = "TWGWXn93FMpJg95ILioJvBrYr01pODTfSHfrPY2uX190OD9anosHhMEZrnNQGgXG";
//string ClientID = "41eadd79d2dd475cb5697f3802f01775";

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
	Allegro_Api.AllegroPermissionState Permissions = AllegroPermissionState.allegro_api_sale_offers_read | AllegroPermissionState.allegro_api_sale_offers_write | AllegroPermissionState.allegro_api_shipments_read | AllegroPermissionState.allegro_api_shipments_write;

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

//var orders = await AllegroApi.GetOrders(Allegro_Api.OrderStatusType.PROCESSING);
//var list = new List<string>();

////var temp = orders.Where(x => x.id == "ee74a380-8de6-11ee-b3a8-6133a263ad6b").FirstOrDefault();

//foreach (var order in orders)
//{
//	Console.Write(order.id + "   " + order.buyer.email + "    " + order.delivery.time.from + "     " + order.delivery.time.dispatch.to);
//	if (order.delivery.time.from == "2024-01-09T23:00:00Z" /*order.status != "NEW" || order.status != "PROCESSING"*/)
//	{
//		Console.Write("  Added \n");

//		list.Add(order.buyer.email);
//	}
//	else
//	{
//		Console.Write("\n");
//	}
//}

//System.IO.File.WriteAllText("Maile.txt", list.Aggregate((a, b) => a + ";" + b));
#endregion

#region Changing ID
//string LibreLogin = "38103_2345";
//string LibrePassword = "38103";

//var LibreApi = new Libre_API.LibreApi(LibrePassword, LibreLogin);
//var books = await LibreApi.GetAllBooks(0);

//var Offers = await AllegroApi.GetAllOffers(OfferState.ACTIVE);

//foreach (var Offer in Offers.offers)
//{
//    if (Offer.external == null)
//        continue;

//    if (Offer.external.id.Contains("-2"))
//        continue;

//    var book = books.Find(bk => bk.ID == Offer.external.id.Replace("-1", ""));

//    if (book == null) continue;

//    if (book.Publisher == "ZYSK I S-KA" || book.Publisher == "NOWA BAŚŃ")
//    {
//        AllegroApi.ChangeExternal(Offer.id, Offer.external.id + "--R");
//        Console.WriteLine(Offer.name);
//    }

//}
#endregion




//static string[] SplitString(string input)
//{
//    string[] result = new string[2];

//    // Wyrażenie regularne do odnalezienia pierwszej spacji i liczby po niej
//    Regex regex = new Regex(@"\s+\d.*");

//    Match match = regex.Match(input);

//    if (match.Success)
//    {
//        // Grupa 1 zawiera tekst przed spacją, a grupa 2 zawiera liczbę
//        result[0] = input.Replace(match.Groups[0].Value,"");
//        result[1] = match.Groups[0].Value;
//        return result;

//    }
//    // W przypadku braku dopasowania
//    return null;
//}
//847bd2c0-a4b4-11ee-8db6-6ff55152933d

//invoice id
//49856987-c2fa-49ae-826e-0ac0ca4c8c9f

//await AllegroApi.GetListOfDelivery();

//var orders = await AllegroApi.GetOrders(Allegro_Api.OrderStatusType.NEW);

//var order = orders.Find(or => or.id == "3add8360-b2e3-11ee-896b-cb253fa9f7e3");

//var services = await AllegroApi.GetDeliveryServices();

//var OrderService = services.services.Where(o => o.id.deliveryMethodId == order.delivery.method.id).FirstOrDefault();

////await AllegroApi.GetParcelNumbers("d7a31770-a648-11ee-bda1-4bfbe848971d");

////var test = await AllegroApi.PostNewInvoice("847bd2c0-a4b4-11ee-8db6-6ff55152933d", "test.pdf", "FV 54/12/2023");

//var shimpment = new ShipmentCreateRequestDto()
//{
//	deliveryMethodId = order.delivery.method.id,
//	credentialsId = OrderService.id.credentialsId,
//	sender = new Allegro_Api.Shipment.Components.SenderAddressDto()
//	{
//		company = "TWILO SP. Z O.O.",
//		street = "ul. Igołomska",
//		streetNumber = "1/30",
//		postalCode = "31-980",
//		city = "Kraków",
//		countryCode = "PL",
//		email = "Arkadiusz.kruszyna@twilo.pl",
//		phone = "+48 572 353 814",
//	},
//	receiver = new Allegro_Api.Shipment.Components.ReceiverAddressDto()
//	{
//		name = order.buyer.firstName + ' ' + order.buyer.lastName,
//		street = order.delivery.address.streetAndNumber[0],
//		streetNumber = order.delivery.address.streetAndNumber[1],//do ogarniecia jest złożony ticket na allegro github
//		postalCode = order.delivery.address.zipCode,
//		city = order.delivery.address.city,
//		countryCode = order.delivery.address.countryCode,
//		email = order.buyer.email,
//		phone = order.buyer.phoneNumber,
//		point = order.delivery.pickupPoint?.id
//	},

//	packages = new Allegro_Api.Shipment.Components.Packages[]
//	{
//		new Allegro_Api.Shipment.Components.Packages()
//		{
//			type = "PACKAGE",
//			weight = new Allegro_Api.Shipment.Components.WeightValue(){value = 25},
//			width = new Allegro_Api.Shipment.Components.DimensionValue(){value = 38},
//			height = new Allegro_Api.Shipment.Components.DimensionValue(){value = 8},
//			length = new Allegro_Api.Shipment.Components.DimensionValue(){value = 64}
//		}
//	},
//	cachOnDelivery = null
//};

//var shipmentobject = new ShipmentObject()
//{
//	commandId = Guid.NewGuid().ToString(),
//	input = shimpment
//};

//var test = await AllegroApi.CreatePackage(shipmentobject);

while (true)
{
	var te = await AllegroApi.CheckPackageCreationStatus("dc17a707-0626-4f4a-8583-35d8a7660bd5");

    System.Diagnostics.Debug.WriteLine($"{te.Status}");
	Thread.Sleep(5000);
}

//Console.WriteLine("");

#region PreSold
//int count = 0;
//foreach (var Offer in Offers.offers)
//{
//	count++;

//	if (count % 100 == 0) Console.WriteLine(count / (float)Offers.totalCount * 100 + "     " + DateTime.Now.ToString());

//	if (Offer.external == null) 
//		continue;

//	if (!Offer.external.id.Contains("-2"))
//		continue;

//	try
//	{
//		var temp = await AllegroApi.ChangeDeliveryTime(Offer.id,
//			new Allegro_Api.Models.Offer.offerComponents.delivery.DeliveryOfferModel()
//			{
//				shipmentDate = "2024-01-03T00:00:00Z",
//				handlingTime = "P2D",
//				shippingRates = new Allegro_Api.Models.Base() { id = "90c012a8-549c-495c-95f2-379e865372a8" }
//			});


//	}
//	catch (HttpRequestException)
//	{
//        _ = await AllegroApi.ChangeDeliveryTime(Offer.id,
//            new Allegro_Api.Models.Offer.offerComponents.delivery.DeliveryOfferModel()
//            {
//                shipmentDate = "2024-01-03T00:00:00Z",
//                handlingTime = "P2D",
//                shippingRates = new Allegro_Api.Models.Base() { id = "90c012a8-549c-495c-95f2-379e865372a8" }
//            });

//    }


#endregion
//}


//Console.ReadLine();

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
//string AteneumLogin = "kempo_warszawa";
//string AteneumPassword = "6KsSGWT6dhD9r8Xvvr";

//AteneumApi ateneumApi = new AteneumApi(AteneumLogin, AteneumPassword);

//var list = new List<string>();

//var TEST = await ateneumApi.GetAllBooksWithMagazin(0);

//var ter = TEST.Where(t => t.ident_ate == "501339").FirstOrDefault();

//foreach (var book in TEST)
//    if (book.BookData.wydawnictwo == "IUVI Games")
//        list.Add(book.BookData.Tytuł + ";" + book.PriceWholeSaleBrutto.ToString());

//var test2 = await wszystkoApi.GetAllGuarantees();
//foreach (var guarantee in test2)
//{
//	Debug.WriteLine($"{ guarantee.Name } { guarantee.GuaranteeDataDetails.Id } { guarantee.GuaranteeDataDetails.ProviderType } {guarantee.GuaranteeDataDetails.ProviderType} { guarantee.AdditionalInformation }\n\n");
//}

//var test3 = await wszystkoApi.GetAllOffers();
//foreach (var offer in test3.Offers)
//{
//	System.Diagnostics.Debug.WriteLine(offer.Title);
//}
//System.IO.File.WriteAllLines("Ceny.csv",list.ToArray());; ;

Console.ReadLine();