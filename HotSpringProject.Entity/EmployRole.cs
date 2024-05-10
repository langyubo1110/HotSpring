using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    [Table("Employ_Role")]
    public class EmployRole
    {
        public int id { get; set; }
        public int? is_leader {  get; set; }
        public string role_name { get; set; }
        public decimal? labor_hours { get; set; }
        public decimal? salary { get; set; }
        public DateTime create_time { get; set; }
    }
}
