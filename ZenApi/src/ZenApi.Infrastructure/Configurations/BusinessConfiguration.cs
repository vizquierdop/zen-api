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
    internal class BusinessConfiguration : IEntityTypeConfiguration<Business>
    {
        public void Configure(EntityTypeBuilder<Business> builder)
        {
            builder.ToTable("Businesses");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.SimultaneousBookings)
                .HasDefaultValue(1);

            builder.Property(b => b.IsActive)
                .HasDefaultValue(true);

            // Relations
            builder.HasOne(b => b.Province)
                .WithMany(p => p.Businesses)
                .HasForeignKey(b => b.ProvinceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.User)
                .WithOne(u => u.Business)
                .HasForeignKey<Business>(b => b.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(b => b.Availabilities)
                .WithOne(a => a.Business)
                .HasForeignKey(a => a.BusinessId);

            builder.HasMany(b => b.OfferedServices)
                .WithOne(s => s.Business)
                .HasForeignKey(s => s.BusinessId);

            builder.HasMany(b => b.Holidays)
                .WithOne(h => h.Business)
                .HasForeignKey(h => h.BusinessId);

            builder.HasMany(b => b.BusinessCategories)
                .WithOne(bc => bc.Business)
                .HasForeignKey(bc => bc.BusinessId);

        }
    }
}
