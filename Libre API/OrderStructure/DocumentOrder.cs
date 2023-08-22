using Libre_API.OrderStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Libre_API.OrderStructure
{
    [XmlRoot("Document-Order")]
    public class DocumentOrder
    {
        [XmlElement("Order-Header")]
        public OrderHead Head { get; set; }

        [XmlElement("Order-Parties")]
        public OrderParties parties { get; set; }

        [XmlElement("Order-Lines")]
        public OrderLines products { get; set; }

        [XmlElement("Order-Summary")]
        public OrderSummary summary { get; set; }
    }
}


//Opis pól:

//Document - Order\ -główny węzeł dokumentu

//Document-Order\Order-Header\

//Document-Order\Order-Header\Remarks - numer zamówienia lub uwagi

//Document-Order\Order-Header\OrderNumber - numer zamówienia

//Document-Order\Order-Header\OrderDate - data zamówienia w formacie
//YYYY-MM-DD

//Document-Order\Order-Header\ExpectedDeliveryDate - data dostawy YYYY-MM-DD
//(może być powtórzona z poprzedniego pola, aktualnie nie brana pod uwagę)

//Document - Order\Order - Header\DocumentFunctionCode - stała wartość "O"

//Document-Order\Order-Header\Combine - opcjonalna wartość "No" dla zamówień
//niepodlegających łączeniu z innymi zamówieniami (jeśli opcja łączenia
//włączona dla Klienta)

//Document - Order\Order - Parties\

//Document - Order\Order - Parties\Buyer\ILN - kod zamawiającego w systemie Liber

//Document-Order\Order-Parties\Seller\ILN - puste

//Document-Order\Order-Parties\DeliveryPoint\ILN - kod miejsca dostawy w
//systemie Liber (powtórzony kod zamawiającego)

//Document - Order\ Order - Lines\ -sekcja z pozycjami zamówienia

//Document-Order\Order-Lines\Line - kolejne pozycje zamówienia

//Document-Order\Order-Lines\Line\Line-Item - dane towaru z pozycji zamówienia

//Document-Order\Order-Lines\Line\Line-Item\LineNumber - kolejny numer pozycji
//w zamówieniu

//Document-Order\Order-Lines\Line\Line-Item\EAN - kod kreskowy towaru

//Document-Order\Order-Lines\Line\Line-Item\BuyerItemCode - opcjonalnie id
//towaru z systemu zamawiającego

//Document-Order\Order-Lines\Line\Line-Item\ItemDescription - opcjonalnie
//nazwa towaru

//Document-Order\Order-Lines\Line\Line-Item\OrderedQuantity - ilość
//zamawianego towaru

//Document-Order\Order-Lines\Line\Line-Item\OrderedUnitNetPrice - opcjonalna
//cena netto zamawianego produktu

//Document-Order\Order-Summary\TotalLines - ilość pozycji w zamówieniu

//Document-Order\Order-Summary\TotalOrderedAmount - suma zamówionej ilości