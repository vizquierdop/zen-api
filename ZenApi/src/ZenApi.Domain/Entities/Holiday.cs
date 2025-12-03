using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Domain.Common;

namespace ZenApi.Domain.Entities
{
    public class Holiday : BaseAuditableEntity
    {
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        
        // Relations
        public required int BusinessId { get; set; }
        public required Business Business { get; set; }
    }
}
