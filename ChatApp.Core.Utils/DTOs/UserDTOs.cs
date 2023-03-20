using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Utils.DTOs
{
    public class UserDTO
    {
        public Guid ID { get; set; }
        public string FullName { get; set; }
        public string ProfileImageSrc { get; set; }
        public string Email { get; set; }
    }
}
