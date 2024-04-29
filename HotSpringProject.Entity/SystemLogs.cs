using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    [Table("System_Logs")]
    public class SystemLogs
    {
        [Key]
        //
        public int id { get; set; }
        public string events { get; set; }
        public int emp_id { get; set; }
        public string opetate { get; set; }
        public DateTime? create_time { get; set; }
    }
}
