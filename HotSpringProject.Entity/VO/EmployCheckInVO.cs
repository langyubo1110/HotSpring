using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    public class EmployCheckInVO
    {
        public int id { get; set; }
        public int emp_Id { get; set; }
        public int check_event { get; set; }
        public int work_type { get; set; }
        public DateTime? create_time { get; set; }
        public string emp_name { get; set; }
    }
}
