using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Framework.Infrastructure.Interfaces.Core.Interface;

namespace Qwerty.DDD.Infrastructure.Repository
{
    public class UserDbContext : DbContext, IDbContext
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddEntityConfigurationsFromAssembly(GetType().GetTypeInfo().Assembly);
        }
    }
}
