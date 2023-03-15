using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Domain
{
    //[Table("Inboxes")]
    public class Inbox : BaseDomain
    {
        public Guid OwnerID { get; set; }
        public User Owner { get; set; }
        public Guid ReceiverID { get; set; }
        public User Receiver { get; set; }

        public bool OwnerDeleted { get; set; }
        public bool ReceiverDeleted { get; set; }
    }
}
