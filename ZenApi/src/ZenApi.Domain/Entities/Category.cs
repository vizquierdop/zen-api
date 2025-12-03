using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Domain.Common;

namespace ZenApi.Domain.Entities
{
    public class Category : BaseAuditableEntity
    {
        public required string Name { get; set; }

        // Relations
        public virtual IList<BusinessCategory> BusinessCategories { get; set; } = new List<BusinessCategory>();
    }
}
