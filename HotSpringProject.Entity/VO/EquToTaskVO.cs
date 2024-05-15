using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity.VO
{
    public class EquToTaskVO
    {
        //设备对应关系表id
        public int id { get; set; }
        public int value { get; set; }
        public int equ_id { get; set; }
        public int equ_plan_id { get; set; }
        public int interval { get; set; }
        public int? task_id{ get; set; }
        public string title { get; set; }
        public string task_name { get; set; }
        public string task_info { get; set; }
        public string upkeep_feedback_info { get; set; }
        public DateTime? upkeep_time { get; set; }
        public DateTime? distribute_time { get; set; }
        public DateTime? feedback_time { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
    }
}
