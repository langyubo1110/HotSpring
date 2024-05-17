using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity.VO
{
    public class EmployEmpVO
    {

        public int id { get; set; }
        public string name { get; set; }
        public int gendar { get; set; }
        public string identity_card { get; set; }
        public string avatar { get; set; }
        public DateTime? onboarding_time { get; set; }
        public int account_status { get; set; }
        public int log_count { get; set; }
        public DateTime? last_log_time { get; set; }
        public DateTime? create_time { get; set; }
        public int role_id { get; set; }
        public string job_number { get; set; }
        public string password { get; set; }
        //角色
        public string role_name { get; set; }
        //是否领导
        public int is_leader { get; set; }
    }
}
