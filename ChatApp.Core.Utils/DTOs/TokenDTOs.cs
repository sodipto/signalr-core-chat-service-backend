using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Utils.DTOs
{
    public class TokenDTO
    {
        public string Type { get; } = "Bearer";
        public long ExpiredAt { get; set; }
        public string AccessToken { get; set; }

    }
}
