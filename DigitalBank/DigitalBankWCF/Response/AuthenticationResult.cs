using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalBankWCF.Response
{
    public class AuthenticationResult
    {
        public bool IsAuthenticated { get; set; }
        public string Username { get; set; }
        public List<string> Roles { get; set; }
        public string ErrorMessage { get; set; }
    }
}