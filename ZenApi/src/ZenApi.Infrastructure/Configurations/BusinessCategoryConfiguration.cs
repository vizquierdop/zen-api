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
    internal class BusinessCategoryConfiguration : IEntityTypeConfiguration<BusinessCategory>
    {
        public void Configure(EntityTypeBuilder<BusinessCategory> builder)
        {
            builder.ToTable("BusinessCategories");

            builder.HasKey(bc => new { bc.BusinessId, bc.CategoryId });

            // Relations
            builder.HasOne(bc => bc.Business)
                .WithMany(b => b.BusinessCategories)
                .HasForeignKey(bc => bc.BusinessId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(bc => bc.Category)
                .WithMany(c => c.BusinessCategories)
                .HasForeignKey(bc =>bc.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
