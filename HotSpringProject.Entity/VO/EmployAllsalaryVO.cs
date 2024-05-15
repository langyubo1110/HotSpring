using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity.VO
{
    public class EmployAllsalaryVO
    {
        public int id { get; set; }
        public int emp_id { get; set; }
        public int pay_month { get; set; }
        public DateTime pay_time { get; set; }
        public int post_status { get; set; }
        public decimal salary { get; set; }
        public decimal perform_money { get; set; }
        public DateTime create_time { get; set; }
        public string name { get; set; }
    }
}
