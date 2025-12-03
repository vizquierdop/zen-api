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
    internal class HolidayConfiguration : IEntityTypeConfiguration<Holiday>
    {
        public void Configure(EntityTypeBuilder<Holiday> builder)
        {
            builder.ToTable("Holidays");

            builder.HasKey(h => h.Id);

            // Relations
            builder.HasOne(h => h.Business)
                .WithMany(b => b.Holidays)
                .HasForeignKey(h => h.BusinessId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
