using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WholesalerApiCommons
{
    public class IProduct
    {
        string Author { get; set; }
        string EAN { get; set; }
        string ISBN { get; set; }
        string ExternalId { get; set; }
        string ImageName { get; set; }
        string IssueYear { get; set; }
        float PriceWholeSaleBrutto { get; set; }
        string Publisher { get; set; }
        string Title { get; set; }
    }
}
