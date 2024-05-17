using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    [Table("Equ_Upkeep_Plan")]
    public class EquUpkeepPlan
    {
            [Key]
            public int id { get; set; }
            public DateTime? start_time { get; set; }
            public DateTime? end_time { get; set; }
            public int? interval { get; set; }
            public int? importance { get; set; }
            public string task_name { get; set; }
            public string task_info { get; set; }
        }
    
}
