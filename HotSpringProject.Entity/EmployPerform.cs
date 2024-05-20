using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    [Table("Employ_Perform")]
    public class EmployPerform
    {
        [Key]
        public int id { get; set; }
        public int emp_id { get; set; }
        public int repair_id { get; set; }
        public decimal repair_up_money { get; set; }
        public DateTime create_time { get; set; }
        
    }
}
