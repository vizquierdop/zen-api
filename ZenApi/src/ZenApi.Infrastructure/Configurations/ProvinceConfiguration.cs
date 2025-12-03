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
    internal class ProvinceConfiguration : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.ToTable("Provinces");

            builder.HasKey(p => p.Id);

            // Relations
            builder.HasMany(p => p.Users)
                .WithOne(u => u.Province)
                .HasForeignKey(u => u.ProvinceId)
                .IsRequired();

            builder.HasMany(p => p.Businesses)
                .WithOne(b => b.Province)
                .HasForeignKey(b => b.ProvinceId)
                .IsRequired();
        }
    }
}
