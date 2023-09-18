using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wszystko_API.Auth
{
    public struct BaseAuthModel
    {
        public string deviceCode { get; set; }
        public string verificationUriComplete { get; set; }
        public string verificationUriPrettyComplete { get; set; }
    }
}
