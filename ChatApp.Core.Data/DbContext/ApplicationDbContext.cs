using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public IDbConnection Connection => Database.GetDbConnection();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var RegisterAllEntity = Assembly.GetExecutingAssembly().GetTypes()
            .Where(entityType => !string.IsNullOrEmpty(entityType.Namespace))
            .Where(entityType => entityType.BaseType != null && entityType.BaseType.IsGenericType &&
                entityType.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeMigrationConfiguration<>));

            foreach (var type in RegisterAllEntity)
            {
                dynamic Tableconfigure = Activator.CreateInstance(type)!;
                modelBuilder.ApplyConfiguration(Tableconfigure);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
