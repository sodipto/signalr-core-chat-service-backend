using ChatApp.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Data
{
    public class InboxMapping : EntityTypeMigrationConfiguration<Inbox>
    {
        public override void Configure(EntityTypeBuilder<Inbox> builder)
        {
            builder.ToTable("Inboxes");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.OwnerID).IsRequired();
            builder.Property(x => x.ReceiverID).IsRequired();

            builder.HasOne(o => o.Owner).WithMany().HasForeignKey(s => s.OwnerID);
            builder.HasOne(r => r.Receiver).WithMany().HasForeignKey(s => s.ReceiverID);
        }
    }
}
