// See https://aka.ms/new-console-template for more information
using AteneumAPI;
using Libre_API;
using System.Text.RegularExpressions;

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

//twilo3
//string ClientSecret = "004VkOAgitQGHYgv6aiW8hLt1F2RpJpi1BxehNe6kIyM4TIbkxVty42hQX4EhaNP";
//string ClientID = "731f01af7c8b46e68ddc12030e4f920c";

LibreApi liber = new LibreApi("38231", "38231_3337");

var b = await liber.GetAllBooks(0);

Libre_API.Book tet = b.Where(bb => bb.ExternalId == "220187").FirstOrDefault();


AteneumApi ateneumApiTwilo = new AteneumApi("twilo_krakow", "6mkfeEVRoUFVRF3QCz");
AteneumApi ateneumApiKempo = new AteneumApi("kempo_warszawa", "6KsSGWT6dhD9r8Xvvr");
List<string> strings = new List<string>();

var testtw = await ateneumApiTwilo.GetAllBooks(0);
var testke = await ateneumApiKempo.GetAllBooks(0);

int mismatchCount = testtw.Count(a =>
{
    if (int.TryParse(Regex.Match(a.stawka_vat, @"\d+").Value, out int stawka_vatValue))
    {
        return stawka_vatValue != a.VAT;
    }
    return false; // Return false if stawka_vat cannot be parsed to int
});

Console.WriteLine(mismatchCount.ToString());

//var twilob = testtw.FirstOrDefault(a => a.ExternalId == "10");
//var kempob = testke.FirstOrDefault(a => a.ExternalId == "10");