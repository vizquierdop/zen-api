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
    internal class AvailabilityConfiguration : IEntityTypeConfiguration<Availability>
    {
        public void Configure(EntityTypeBuilder<Availability> builder)
        {
            builder.ToTable("Availabilities");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.IsActive)
                .HasDefaultValue(true);

            // Relations
            builder.HasOne(a => a.Business)
                .WithMany(b => b.Availabilities)
                .HasForeignKey(a => a.BusinessId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
