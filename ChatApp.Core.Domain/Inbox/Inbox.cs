using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Domain
{
    public class Inbox : BaseDomain
    {
        public Guid OwnerID { get; set; }
        public Guid ReceiverID { get; set; }
        public bool OwnerDeleted { get; set; }
        public bool ReceiverDeleted { get; set; }
    }
}
