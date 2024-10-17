﻿using Adly.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityTests.EFCore.Configurations
{
    internal class UserClaimEntityConfiguration : IEntityTypeConfiguration<UserClaimEntity>
    {
        public void Configure(EntityTypeBuilder<UserClaimEntity> builder)
        {
            builder.Property(c=>c.ClaimType).HasMaxLength(1000);
            builder.Property(c=>c.ClaimValue).HasMaxLength(1000);

            builder.HasKey(c=>c.Id);

            builder.ToTable("UserClaims","usr");
        }
    }
}
