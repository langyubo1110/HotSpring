using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    public class EmployMessageVO
    {
        public int id { get; set; }
        public DateTime? send_time { get; set; }
        public string part { get; set; }
        public string link { get; set; }
        public int? sender_id { get; set; }
        public int? recipients_id { get; set; }
        public DateTime? create_time { get; set; }
        public string sender_name { get; set; }
        public string recipients_name { get; set; }
        public int? apply_id { get; set; }
    }
}
