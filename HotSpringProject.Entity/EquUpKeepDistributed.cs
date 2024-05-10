using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    [Table("Equ_UpKeep_Distributed")]
    public class EquUpKeepDistributed
    {
        public int id { get; set; }
        public int emp_id { get; set; }
        public DateTime? upkeep_time { get; set; }
        public int equ_plan_id { get; set; }
        public DateTime? create_time { get; set; }

    }
}
