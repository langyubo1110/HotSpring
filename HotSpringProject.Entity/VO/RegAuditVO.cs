using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    public class RegAuditVO
    {
        public int id { get; set; }
        public int reg_equip_reaearch_id { get; set; }

        public int recheck_id { get; set; }
        public string recheck_advice { get; set; }
        public int recheck_opin { get; set; }
        public DateTime create_time { get; set; }
        public int countAudit { get; set; }
    }
}
