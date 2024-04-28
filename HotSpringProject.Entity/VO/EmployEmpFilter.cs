using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    public class EmployEmpFilter
    {
        public string name { get; set; }
        public string identity_card { get; set; }
        public int account_status { get; set; }
        public int role_id { get; set; }
        public string job_number { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
    }
}
