using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frapper.API.Helpers
{
    public class AppSettings
    {
        public string SecretKey { get; set; }
        public string Expires { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
