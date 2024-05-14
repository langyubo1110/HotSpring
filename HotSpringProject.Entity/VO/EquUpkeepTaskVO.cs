using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity.VO
{
    public class EquUpkeepTaskVO
    {
        public int id { get; set; }
        public int? status { get; set; }
        public string task_name { get; set; }
        public DateTime? start_time { get; set; }
        public DateTime? end_time { get; set; }
        public int interval { get; set; }
        public int importance { get; set; }
        public string task_info { get; set; }
        public int equ_plan_id { get; set; }
        public DateTime? upkeep_time { get; set; }
        public DateTime? distribute_time { get; set; }
        public DateTime? feedback_time { get; set; }
        public int? distribute_id { get; set; }
        public int? exec_id { get; set; }
        public string upkeep_feedback_info { get; set; }
        public string QRimg { get; set; }

        public int task_id { get; set; }
        public int equ_id { get; set; }
        public int equ_type { get; set; }
        public string people_name { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public string type_name { get; set; }
        public string power { get; set; }
    }
}
