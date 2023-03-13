using ChatApp.Core.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Data
{
    public class MessageMapping : EntityTypeMigrationConfiguration<Message>
    {
        public override void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.InboxID).IsRequired();
            builder.Property(x => x.SenderID).IsRequired();
            builder.Property(x => x.Content).IsRequired();

            builder.HasOne(x => x.Inbox);
            builder.HasOne(x => x.Sender);
        }
    }
}
