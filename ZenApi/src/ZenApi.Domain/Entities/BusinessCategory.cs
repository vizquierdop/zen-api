using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Domain.Common;

namespace ZenApi.Domain.Entities
{
    public class BusinessCategory : BaseAuditableEntity
    {
        public int BusinessId { get; set; }
        public int CategoryId { get; set; }
        
        // Relations
        public Business Business { get; set; }
        public Category Category { get; set; }
    }
}
