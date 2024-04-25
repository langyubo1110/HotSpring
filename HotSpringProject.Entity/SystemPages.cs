using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    public class SystemPages
    {
        public int id { get; set; }
        public string page_address { get; set; }
        public string page_name { get; set;}
        public string icon { get; set;}
        public int module_id { get; set;}
        public DateTime? create_time { get; set; }
    }
}
