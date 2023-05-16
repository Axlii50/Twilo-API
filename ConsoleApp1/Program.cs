using Allegro_Api;
using Allegro_Api.Models.Product;
using Allegro_Api.Models.Product.ProductComponents;
using Libre_API;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;


string ClientSecret = "iHIe9uq4bwuRWYrXGsjFRdMDMuHyvef0fEkiIQh9VYsejZAlmqF2hqRer0lHKg50";
string ClientID = "852325ff150549428cccd54d22726923";

var AllegroApi = new AllegroApi(ClientID, ClientSecret);

Allegro_Api.Models.VerificationULRModel t = AllegroApi.Authenticate().Result;


Console.WriteLine(t.device_code);
Console.WriteLine(t.verification_uri_complete);
Console.WriteLine("");


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

#region ttt
//string[] test = { "LIT. PIĘKNA / FANTASTYKA", "LIT. PIĘKNA / POWIEŚĆ", "LIT. PIĘKNA / FANTASTYKA", "HISTORIA / POWSZECHNA / II WOJNA ŚWIATOWA", "PEDAGOGIKA", "RELIGIE / RELIGIOZNAWSTWO", "LIT. POPULARNONAUKOWA", "RELIGIE / PUBLICYSTYKA", "LIT. FAKTU / PUBLICYSTYKA", "POLITYKA", "SZTUKA", "PARAPSYCHOLOGIA", "LIT. FAKTU / FELIETONY", "EZOTERYKA" };
//string[] test2 = { "Wicehrabia przepołowiony", "Baron drzewołaz", "Rycerz nieistniejący ", "W stronę ciemności. Rozmowy z komendantem Treblinki", "Drama. Teatr przebudzenia", "Życie codzienne w Palestynie w czasach Chrystusa", "Święta i obyczaje żydowskie", "Żydzi, świat, pieniądze", "Wściekłość i duma (dodruk 2018)", "Siła rozumu (dodruk 2018)", "Mała historia fotografii", "Kabała", "Droga Człowieka Według Nauczania Chasydów", "Nieskończone źródło twojej mocy. Klucz do pozytywnego myślenia" };

//int index = 0;
//foreach (string x in test)
//{
//    var tttt = await AllegroApi.GetSuggestionOfCategory(x);
//    Console.WriteLine("kategoria: " + x);
//    if (tttt.matchingCategories.Length > 0)
//    {
//        Console.WriteLine("sugerowany kategoria: " + tttt.matchingCategories[0].name);
//        Console.WriteLine(tttt.matchingCategories[0].id);
//    }
//    else
//    {
//        tttt = await AllegroApi.GetSuggestionOfCategory(test2[index]);
//        Console.WriteLine("tytuł: " + test2[index]);

//        //moze trzeba bedzie sprawdzać czy jest leaf
//        if (tttt.matchingCategories.Length > 0)
//        {
//            Console.WriteLine("sugerowany kategoria: " + tttt.matchingCategories[0].name);
//            Console.WriteLine(tttt.matchingCategories[0].id);
//        }
//    }
//    index += 1;



//    Console.WriteLine();
//    if (tttt.matchingCategories.Length <= 0) continue;

//    var parameters = await AllegroApi.GetCategoryParameters(tttt.matchingCategories[0].id);

//    //    if (parameters.parameters.Length < 16) continue;

//    foreach (var parameter in parameters.parameters)
//    {
//        Console.WriteLine($"nazwa parametru: {parameter.name} ");
//    }

//    //    List<ProductParameter> param = new List<ProductParameter>();


//}







//    Console.WriteLine();

//    Console.WriteLine();
//}

#endregion

//var d = AllegroApi.GetCategoryParameters("66791").Result;


//Console.WriteLine("");


//var d = AllegroApi.CheckForProduct("9788386859849").Result;
//Console.WriteLine(d.Content.ReadAsStringAsync().Result);



//var test = AllegroApi.GetAllOffers().Result;


//LibreApi lib = new LibreApi("38103","38103_2345");

////lib.StringToBook("9788386757220;83-86757-22-1;16;KOS;Nieskończone źródło twojej mocy. Klucz do pozytywnego myślenia;;Murphy Joseph;;KOS;;26.71;25.44;49.00;5%;2008-01-01;2018-02-23;;2;EZOTERYKA;miękka;;205;145;20;0.37", 5);

//var t = await lib.GetAllBooks(5);
////var d = lib.GetPhoto("2286").Result.ReadAsStream();

////var test = AllegroApi.UploadImage().Result;
////Console.WriteLine(test.StatusCode);
////Console.WriteLine(test.Content.ReadAsStringAsync().Result);


//Console.WriteLine();
//Console.ReadLine();


//przestestować po jakim czasie usuwają (allegro) zdjecia z servera