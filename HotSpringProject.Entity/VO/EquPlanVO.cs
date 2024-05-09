using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity.VO
{
    public class EquPlanVO
    {
        public int id { get; set; }
        public int equ_id { get; set; }
        public DateTime? start_time { get; set; }
        public DateTime? end_time { get; set; }
        public int? interval { get; set; }
        public int? importance { get; set; }
        public string upkeep_distribute { get; set; }
        public string equ_name { get; set; }
        public string task_name { get; set; }
    }
}
