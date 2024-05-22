using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    [Table("Employ_Message")]
    public class EmployMessage
    {
        public int id { get; set; }
        public DateTime? send_time { get; set; }
        public string part { get; set; }
        public string link { get; set; }
        public int? sender_id { get; set; }
        public int? recipients_id { get; set; }
        public DateTime? create_time { get; set; }
        public int? state { get; set; }
    }
}
