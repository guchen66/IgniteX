using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgniteX.Shared.Models.Authentication
{
    public class LoginInfo
    {
        public string LoginName { get; set; }
        public string LoginPassword { get; set; }
        public string LoginProvider { get; set; }
    }
}
