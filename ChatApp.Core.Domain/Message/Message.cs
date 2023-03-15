using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Domain
{
    [Table("Messages")]
    public class Message : BaseDomain
    {
        public Guid InboxID { get; set; }
        public virtual Inbox Inbox { get; set; }
        public string Content { get; set; }
        public Guid SenderID { get; set; }
        public virtual User Sender { get; set; }
        public bool SenderDeleted { get; set; }
        public bool ReceiverDeleted { get; set; }
        public bool SeenStatus { get; set; }
        public bool DeliveredStatus { get; set; }
    }
}
