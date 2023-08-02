using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Config
    {
       public List<RangeMargin> rangeMargins { get; set; } 

        public Dictionary<string, float> ExceptionPublishersPriceMargin { get; set; }
        public Dictionary<string, float> CategoryMargin { get; set; }

        public float NormalMargin { get; set; }

        public string AllegroClientSecret { get; set; }
        public string AllegroClientID { get; set; }
        public string RefreshToken { get; set; }

        public wholesalerType WholeSalerType { get; set; }

        public string RealizationTime { get; set; }

        public string WholeSalerPassword { get; set; }
        public string WholeSalerLogin { get; set; }
    }

    public class RangeMargin
    {
        public float lowerbound { get; set; }
        public float upperbound { get; set; }

        public float margin { get; set; }
        public bool Addmargin { get; set; }
    }
    public enum wholesalerType
    {
        None = 0,
        Liber = 1,
        Ateneum = 2
    }
}
