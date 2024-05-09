using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    [Table("Employ_Check_In")]
    public class EmployCheckIn
    {
        public int id { get; set; }
        public int emp_Id { get; set;}
        public int check_event { get; set; }
        public int work_type { get; set; }
        public DateTime? create_time { get; set; }
    }
}
