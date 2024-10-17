﻿using Adly.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityTests.EFCore.Configurations
{
    internal class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(c => c.UserCode).HasMaxLength(10);
            builder.Property(c => c.FirstName).HasMaxLength(100);
            builder.Property(c => c.LastName).HasMaxLength(100);

                builder.Property(c => c.PhoneNumber).HasMaxLength(50);
                builder.Property(c => c.Email).HasMaxLength(50);
                builder.Property(c => c.ConcurrencyStamp).HasMaxLength(200);
                builder.Property(c => c.SecurityStamp).HasMaxLength(500);
                builder.Property(c => c.NormalizedEmail).HasMaxLength(100);
                builder.Property(c => c.NormalizedUserName).HasMaxLength(200);
                builder.Property(c => c.PasswordHash).HasMaxLength(1000);

                builder.Property(c => c.UserName).HasMaxLength(200);

            #region NavigationProperty
            builder.HasMany(c => c.UserRoles)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);

            builder.HasMany(c => c.UserClaims)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);

            builder.HasMany(c => c.UserLogins)
               .WithOne(c => c.User)
               .HasForeignKey(c => c.UserId);

            builder.HasMany(c => c.UserTokens)
              .WithOne(c => c.User)
              .HasForeignKey(c => c.UserId);



            #endregion
            builder.OwnsMany(c => c.Images, navigationBuilder =>
            {
                navigationBuilder.ToJson("Images");
            });

            builder.HasIndex(c => c.NormalizedEmail);
            builder.HasIndex(c => c.NormalizedUserName);
            builder.HasIndex(c => c.PhoneNumber);


            builder.ToTable("Users", "usr");
        }
    }
}
