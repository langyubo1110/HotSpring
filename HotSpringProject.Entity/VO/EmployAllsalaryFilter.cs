using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity.VO
{
    public class EmployAllsalaryFilter
    {
        public int? page { get; set; }
        public int? limit { get; set; }
        public string name { get; set; }
        public int? pay_month { get; set; }
    }
}
