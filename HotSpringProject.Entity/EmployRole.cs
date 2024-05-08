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
        public int? role {  get; set; }
        public string role_name { get; set; }
        public decimal? Labor_hours { get; set; }
        public decimal? Salary { get; set; }
        public DateTime create_time { get; set; }
    }
}
