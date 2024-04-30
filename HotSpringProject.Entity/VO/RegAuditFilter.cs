using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    public class RegAuditFilter
    {
        public int? recheckid { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
    }
}
