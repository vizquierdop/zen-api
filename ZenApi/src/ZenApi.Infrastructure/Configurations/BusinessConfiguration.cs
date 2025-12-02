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

            // Relations
            builder.HasOne(b => b.Province)
                .WithMany()
                .HasForeignKey(b => b.ProvinceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(b => b.SimultaneousBookings)
                .HasDefaultValue(1);

            builder.Property(b => b.IsActive)
                .HasDefaultValue(true);
        }
    }
}
