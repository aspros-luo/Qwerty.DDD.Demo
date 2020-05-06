using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qwerty.DDD.Domain;

namespace Qwerty.DDD.Infrastructure.Repository.MapConfigurations.IdentityMapping
{
    public class UserMap : ModelBuilderExtenions.EntityMappingConfiguration<User>
    {
        public override void Map(EntityTypeBuilder<User> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("users");
            entityTypeBuilder.HasKey(i => i.Id);
            entityTypeBuilder.Property(i => i.Id).ValueGeneratedOnAdd();
            entityTypeBuilder.Property(i => i.Name);
            entityTypeBuilder.Property(i => i.Password);
            entityTypeBuilder.Ignore(i => i.UserIdentity);
        }
    }
}
