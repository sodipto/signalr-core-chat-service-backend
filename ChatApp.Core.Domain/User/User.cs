using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Domain
{
    public class User : BaseDomain
    {
        public string FullName { get; set; }
        public string ProfileImageSrc { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
