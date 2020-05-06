using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Qwerty.DDD.Domain.Events;

namespace Qwerty.DDD.Infrastructure.Repository.MapConfigurations.IdentityMapping
{
    public class UserIdentityMap: ModelBuilderExtenions.EntityMappingConfiguration<UserIdentity>
    {
        public override void Map(EntityTypeBuilder<UserIdentity> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("identities");
            entityTypeBuilder.HasKey(i => i.UserId);
            entityTypeBuilder.Property(i => i.UserId).HasColumnName("user_id");
            entityTypeBuilder.Property(i => i.RealName).HasColumnName("real_name");
            entityTypeBuilder.Property(i => i.IdentityNo).HasColumnName("identity_no");
        }
    }
}
