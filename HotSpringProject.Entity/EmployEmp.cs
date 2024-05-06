using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    [Table("Employ_Emp")]
    public class EmployEmp
    {
        
        public int id {  get; set; }
        public string name { get; set; }
        public int gendar { get; set; }
        public string identity_card { get; set; }
        public string avatar { get; set; }
        public DateTime onboarding_time { get; set; }
        public int account_status { get; set; }
        public string log_count { get; set; }
        public DateTime last_log_time { get; set; }
        public DateTime create_time { get; set; }
        public int role_id { get; set; }
        public string job_number { get; set; }

    }
}
