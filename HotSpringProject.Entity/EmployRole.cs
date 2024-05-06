using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    public class EmployRole
    {
        public int id { get; set; }
        public string role_name { get; set; }
        public int role { get; set; }
        public decimal Salary { get; set; }
        public string Labor_hours { get; set; }
      
        public DateTime create_time { get; set; }
        
    }
}
