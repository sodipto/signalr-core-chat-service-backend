using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Utils.DTOs
{
    public class AuthDTO
    {
        public TokenDTO Token { get; set; }
        public UserDTO User { get; set; }
    }
}
