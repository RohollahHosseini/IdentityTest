using Adly.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityTests.EFCore.Configurations
{
    internal class UserLoginEntityConfiguration : IEntityTypeConfiguration<UserLoginEntity>
    {
        public void Configure(EntityTypeBuilder<UserLoginEntity> builder)
        {
            builder.HasKey(c => new { c.LoginProvider, c.ProviderKey });
            builder.Property(c => c.LoginProvider).HasMaxLength(500);
            builder.Property(c => c.ProviderKey).HasMaxLength(500);
            builder.Property(c => c.ProviderDisplayName).HasMaxLength(500);

            builder.ToTable("UserLogins","usr");
        }
    }
}
