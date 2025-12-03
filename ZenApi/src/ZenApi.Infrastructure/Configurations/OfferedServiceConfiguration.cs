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
    internal class OfferedServiceConfiguration : IEntityTypeConfiguration<OfferedService>
    {
        public void Configure(EntityTypeBuilder<OfferedService> builder)
        {
            builder.ToTable("OfferedServices");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Price)
                .HasColumnType("decimal(18, 2)");

            builder.Property(s => s.IsActive)
                .HasDefaultValue(true);

            // Relations
            builder.HasOne(s => s.Business)
                .WithMany(b => b.OfferedServices)
                .HasForeignKey(s => s.BusinessId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.Reservations)
                .WithOne(r => r.Service)
                .HasForeignKey(r => r.ServiceId)
                .IsRequired();
        }
    }
}
