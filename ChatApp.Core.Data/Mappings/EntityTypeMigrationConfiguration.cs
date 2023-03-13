using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Data
{
    public abstract class EntityTypeMigrationConfiguration<T> : IEntityTypeConfiguration<T> where T : class
    {
        public EntityTypeMigrationConfiguration() { }

        public abstract void Configure(EntityTypeBuilder<T> builder);
    }
}
