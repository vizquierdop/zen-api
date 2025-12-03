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
        public required int BusinessId { get; set; }
        public required int CategoryId { get; set; }
        
        // Relations
        public required Business Business { get; set; }
        public required Category Category{ get; set; }
    }
}
