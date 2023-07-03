using Allegro_Api;
using Allegro_Api.Models.Offer;
using Allegro_Api.Models.Product;
using Allegro_Api.Models.Product.ProductComponents;
using Libre_API;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;


string ClientSecret = "aKgn8GbxJqghLVvqvYpM3Bdlb5eQmCdx6jm2KBybsmSNEfYZtnuHCemwLa5xOvde";
string ClientID = "0292044ee78a47f2a7f315ece84edfe5";

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

//var offers = AllegroApi.GetAllOffers(OfferState.ACTIVE).Result.offers;


var test = AllegroApi.GetDetailedOffer("13903417760");

System.Diagnostics.Debug.WriteLine(test.Result.Content.ReadAsStringAsync().Result); 

//int count = 0;
//foreach(var offer in offers)
//{
//    System.Diagnostics.Debug.WriteLine(offer.name);

//    if (offer.id == "13807407424")
//        System.Diagnostics.Debug.WriteLine("test");

//    if(offer.external == null) continue;
    
//    if(offer.external.id.Contains("-")) continue;

//    var result = await AllegroApi.ChangeExternal(offer.id, offer.external.id + "-1");

//    if (!result.IsSuccessStatusCode) count++;

//    System.Diagnostics.Debug.WriteLine(result.Content.ReadAsStringAsync().Result);  
//}

//var product = AllegroApi.CheckForProduct("9788381597913", "CHIMERYKI. TEKSTY SATYRYCZNE - DAGNY").Result;


//var bo = AllegroApi.ValidateProduct(product, "9788381597913");



//Console.WriteLine();

//Console.WriteLine();
//}

//#endregion


//var offers = new List<SimpleOfferModel>();

//offers = AllegroApi.GetAllOffers(OfferState.ENDED).Result.offers;


//for (int i = 0; i < 89; i++)
//{
//    AllegroApi.ChangeExternal(offers[i].id, "null");
//}

//var product = await AllegroApi.CheckForProduct("9788365796660");

//var productvalidated = await AllegroApi.ValidateProduct(product);

//var response = await AllegroApi.CreateOfferBasedOnExistingProduct(product, new Allegro_Api.Models.BaseValue() { value = 1 }, "1111", "90c012a8-549c-495c-95f2-379e865372a8", "testowa nazwa oferty", "999");
// Console.WriteLine(response.Item1.ReadAsStringAsync().Result);
// Console.WriteLine(response.Item2.ToString());

//using HttpClient client = new HttpClient();

//client.DefaultRequestHeaders.Clear();
//client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AllegroApi.AccessToken);
//client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("pl-PL"));
//client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.allegro.public.v1+json"));

//OffersModel retrvied = new OffersModel()
//{
//    offers = new List<SimpleOfferModel>(),
//    totalCount = 0,
//    count = 0
//};

//bool keepgoin = true;
//int offset = 0;
//do
//{
//    keepgoin = true;
//    HttpResponseMessage odp = await client.GetAsync("https://api.allegro.pl" + $"/sale/offers?limit={1000}&offset={offset}");

//    OffersModel model = JsonConvert.DeserializeObject<OffersModel>(odp.Content.ReadAsStringAsync().Result);
//    if (model.offers == null)
//        break;
//    retrvied.offers.AddRange(model.offers);
//    retrvied.count += model.count;
//    retrvied.totalCount = model.totalCount;
//    Console.WriteLine(model.count);

//    if (model.count >= 1000)
//    {

//        offset += 1000;

//    }

//    Console.WriteLine("tet:    " + retrvied.count);
//}
//while (retrvied.count != retrvied.totalCount);
//var d = AllegroApi.GetCategoryParameters("66791").Result;


//Console.WriteLine("");


//var d = AllegroApi.CheckForProduct("9788386859849").Result;
//Console.WriteLine(d.Content.ReadAsStringAsync().Result);



//var test = AllegroApi.GetAllOffers(true).Result;


//LibreApi lib = new LibreApi("38103", "38103_2345");

//////lib.StringToBook("9788386757220;83-86757-22-1;16;KOS;Nieskończone źródło twojej mocy. Klucz do pozytywnego myślenia;;Murphy Joseph;;KOS;;26.71;25.44;49.00;5%;2008-01-01;2018-02-23;;2;EZOTERYKA;miękka;;205;145;20;0.37", 5);

//////var d = lib.GetPhoto("2286").Result.ReadAsStream();

//////var test = AllegroApi.UploadImage().Result;
//////Console.WriteLine(test.StatusCode);
//////Console.WriteLine(test.Content.ReadAsStringAsync().Result);


//Console.WriteLine(t);
Console.ReadLine();


//przestestować po jakim czasie usuwają (allegro) zdjecia z servera





