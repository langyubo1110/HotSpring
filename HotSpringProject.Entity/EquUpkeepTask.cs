using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    [Table("Equ_Upkeep_Task")]
    public  class EquUpkeepTask
    {
        [Key]
        public int id { get; set; }
        public int equ_plan_id { get; set; }
        public int equ_id { get; set; }
        public DateTime? upkeep_time { get; set; }
        public DateTime? distribute_time { get; set; }
        public DateTime? feedback_time { get; set; }
        public int? distribute_id { get; set; }
        public int? exec_id { get; set; }
        public string upkeep_feedback_info { get; set; }
        public string QRimg { get; set; }
        public int? status { get; set; }

    }
}
