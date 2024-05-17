using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    [Table("Equ_To_Taskes")]
    public class EquToTask
    {
        [Key]
        public int id { get; set; }
        public int equ_id { get; set; }
        public int? task_id { get; set; }
        public int equ_plan_id { get; set; }
    }
}
