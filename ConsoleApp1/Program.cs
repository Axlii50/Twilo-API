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
//string ClientSecret = "aKgn8GbxJqghLVvqvYpM3Bdlb5eQmCdx6jm2KBybsmSNEfYZtnuHCemwLa5xOvde";
//string ClientID = "0292044ee78a47f2a7f315ece84edfe5";

//string ClientSecret = "aKgn8GbxJqghLVvqvYpM3Bdlb5eQmCdx6jm2KBybsmSNEfYZtnuHCemwLa5xOvde";
//string ClientID = "0292044ee78a47f2a7f315ece84edfe5";

//var AllegroApi = new AllegroApi(ClientID, ClientSecret);

//Allegro_Api.Models.VerificationULRModel t = AllegroApi.Authenticate().Result;


//Console.WriteLine(t.device_code);
//Console.WriteLine(t.verification_uri_complete);
//Console.WriteLine("");


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



//var offers = AllegroApi.GetAllOffers(OfferState.ACTIVE).Result.offers;
//////Console.WriteLine(AllegroApi.AccessToken);

////await AllegroApi.RefreshAccesToken();
////Console.WriteLine(AllegroApi.AccessToken);
////var test = AllegroApi.GetDetailedOffer("13903417760");

////System.Diagnostics.Debug.WriteLine(test.Result.Content.ReadAsStringAsync().Result); 
////var oferta = offers.Where(of => of.id == "14043093012").FirstOrDefault();
////var test = await AllegroApi.ChangeDeliveryTime(oferta.id, oferta.delivery, "P2D");

//int count = 0;
//foreach (var offer in offers)
//{
//    //if (offer.delivery.handlingTime == "P2D") return;
//    if (count % 100 == 0) System.Diagnostics.Debug.WriteLine(count);

//    Console.WriteLine(offer.name);

//    var result = await AllegroApi.ChangeDeliveryTime(offer.id, offer.delivery, "P2D");

//    System.Diagnostics.Debug.WriteLine(result.Content.ReadAsStringAsync().Result);


//    count++;
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

////////lib.StringToBook("9788386757220;83-86757-22-1;16;KOS;Nieskończone źródło twojej mocy. Klucz do pozytywnego myślenia;;Murphy Joseph;;KOS;;26.71;25.44;49.00;5%;2008-01-01;2018-02-23;;2;EZOTERYKA;miękka;;205;145;20;0.37", 5);

//var d = lib.GetPhoto("229410").Result;

//////var test = AllegroApi.UploadImage().Result;
//////Console.WriteLine(test.StatusCode);
//////Console.WriteLine(test.Content.ReadAsStringAsync().Result);


//Console.WriteLine(t);



//przestestować po jakim czasie usuwają (allegro) zdjecia z servera




//var _client = new HttpClient();

//_client.DefaultRequestHeaders.Clear();

//var userName = "kempo_warszawa";
//var userPassword = "6KsSGWT6dhD9r8Xvvr";

//var authenticationString = $"{userName}:{userPassword}";
//var base64String = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(authenticationString));

//_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64String);

//var response = await _client.GetAsync("https://www.ateneum.pl/bazaksiazek/baza_ksiazek.csv");

//string s = null;
//using (var sr = new StreamReader(await response.Content.ReadAsStreamAsync(), Encoding.GetEncoding("iso-8859-1")))
//{
//    s = sr.ReadToEnd();
//}

//AteneumApi ate = new AteneumApi("kempo_warszawa", "6KsSGWT6dhD9r8Xvvr");

//var test = await ate.GetAllBooksWithMagazin(5);
 string[] Liber = { "229410-1-1", "230141-1-1", "247028-1-1", "247026-1-1", "247023-1-1", "247021-1-1", "247020-1", "247019-1", "247016-1", "247015-1", "247013-1", "247010-1",
            "192409-1","237758-1","192408-1","192403-1","192402-1","192399-1","192395-1","193991-1","193992-1","193987-1","193974-1","193973-1","192421-1","192419-1","192415-1","231596-1","230852-1",
            "230484-1","230192-1","230209-1","230700-1","230854-1","229420-1","228932-1","224162-1","224155-1","221308-1","238665-1","238646-1","238645-1","238640-1","238639-1","238638-1","238623-1",
            "238612-1","238610-1","238453-1","238452-1","238451-1","238385-1","238219-1","238012-1","237763-1","237542-1","237391-1","237197-1","236748-1","236747-1","235959-1","235802-1","241195-1",
            "241179-1","241150-1","241090-1","241088-1","241087-1","241085-1","241082-1","240912-1","240373-1","240718-1","240444-1","240107-1","239848-1","239465-1","239455-1","239420-1","239416-1","239349-1",
            "239345-1","239324-1","239306-1","239254-1","239249-1","239175-1","239170-1","239164-1","239113-1","239057-1","239039-1","238968-1","238965-1","238959-1","238927-1","238926-1","238919-1","238904-1",
            "238812-1","238676-1","238669-1","238666-1","239566-1","242318-1","242317-1","242316-1","242055-1","241622-1","239477-1","239470-1","238454-1","235100-1","241770-1","242393-1","242391-1","247007-1",
            "247006-1","247005-1","247004-1","247002-1","247001-1","247000-1","246999-1","246998-1","246997-1","246996-1","246994-1","246993-1","246992-1","246989-1","246988-1","246987-1","246986-1","246985-1",
            "246984-1","246983-1","246981-1","246980-1","246979-1","246977-1","246976-1","246975-1","246974-1","246973-1","246972-1","246971-1","246970-1","246969-1","246968-1","246967-1","246966-1","246965-1",
            "246964-1","246963-1","246961-1","246958-1","246957-1","246955-1","246954-1","246931-1","246928-1","246824-1","246717-1","246709-1","246526-1","245927-1","245925-1","245246-1","245243-1","245061-1",
            "245060-1","245058-1","245057-1","245053-1","245051-1","245050-1","245049-1","245048-1","245047-1","245046-1","245045-1","245044-1","244824-1","244523-1","244172-1","244171-1","236813-1",
            "243408-1","242875-1","247131-1","247130-1","247099-1","247096-1","247031-1","247028-1","247026-1","247023-1","247021-1","247017-1","247014-1","247012-1","247009-1","247008-1","239473-1",
            "238673-1","238653-1","238642-1","238599-1","237279-1","230141-1","238674-1","247377-1","247369-1","247361-1","247359-1","247356-1","247318-1","247317-1","247314-1","247253-1",
            "247243-1","247242-1","247240-1","247238-1","247214-1","247213-1","247144-1","247139-1","247135-1","236974-1","230853-1","271488-1","271486-1","271226-1","271215-1","266626-1","264999-1",
            "264489-1","267248-1","268154-1","264021-1","264032-1","263539-1","263538-1","263184-1","263107-1","261618-1","261172-1","260944-1","260828-1","260765-1","260764-1","259448-1","259391-1",
            "259192-1","259122-1","259029-1","258986-1","258754-1","258744-1","258484-1","258478-1","258477-1","258476-1","258474-1","258473-1","258472-1","258468-1","257942-1","257925-1","257912-1",
            "257695-1","256888-1","256193-1","255275-1","255221-1","254706-1","254703-1","253691-1","253426-1","249921-1","249739-1","249727-1","249373-1","248978-1","248952-1","248397-1","248395-1",
            "248390-1","248389-1","248386-1","248355-1","248354-1","248199-1","248139-1","247758-1","247741-1","247737-1","247622-1","247471-1","247461-1","240189-1","227807-1","267683-1","192398-1",
            "192407-1","192412-1","192422-1","192418-1","192412-1","192407-1","192398-1","264988-1","260766-1","259467-1","257235-1","256587-1","245916-1","267725-1","266163-1","265167-1","267957-1",
            "259928-1","254374-1","252542-1","256387-1","271747-1","271359-1","271235-1","270729-1","270702-1","270701-1","270602-1","270590-1","270443-1","269014-1","268747-1","267246-1","266294-1",
            "264994-1","263816-1","263507-1","261768-1","260827-1","259121-1","259045-1","258475-1","258303-1","257534-1","257351-1","257130-1","256094-1","253960-1","249726-1","249011-1","249010-1",
            "249009-1","249008-1","248980-1","248369-1","248367-1","247551-1","268264-1","270712-1","263510-1","256883-1","257358-1","259118-1","264474-1","264981-1","266796-1","270203-1","252997-1",
            "252559-1","251782-1","251660-1","251007-1","250857-1","250627-1","250453-1","249371-1","248754-1","248394-1","248393-1","248391-1","248279-1","248267-1","248263-1","248262-1","248244-1",
            "248216-1","248202-1","248189-1","248133-1","247795-1","247792-1","247791-1","247790-1","247773-1","247757-1","247748-1","247743-1","247742-1","247697-1","247695-1","247694-1","247693-1",
            "247691-1","247690-1","247689-1","247688-1","247684-1","247581-1","247558-1","247550-1","246759-1","246154-1","243056-1","243013-1","242866-1","242863-1","242862-1","242808-1","238588-1",
            "237392-1","236814-1","229410-1","261612-1","258274-1","257863-1","257684-1","247466-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1","-1"};


File.WriteAllLines("LiberBlocked.txt",Liber);


Console.ReadLine();