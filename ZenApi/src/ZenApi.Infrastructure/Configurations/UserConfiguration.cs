using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Domain.Entities;

namespace ZenApi.Infrastructure.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.IsActive)
                .HasDefaultValue(true);

            // Relations
            builder.HasOne(u => u.Province)
                .WithMany(p => p.Users)
                .HasForeignKey(u => u.ProvinceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Reservations)
                .WithOne(r => r.User)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(u => u.Business)
                .WithOne(b => b.User)
                .HasPrincipalKey<User>(u => u.Id);
        }
    }
}
