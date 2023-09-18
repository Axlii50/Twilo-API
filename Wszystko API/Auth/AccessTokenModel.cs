using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wszystko_API.Auth
{
    public class AccessTokenModel
    {
        public string AccessToken { get; set;}
        public int expiresIn { get; set;}
        public int refreshExpiresIn { get; set;}
        public string refreshToken { get; set;}
        public string tokenType { get; set;}
    }
}
