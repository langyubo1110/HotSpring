using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    public class ModuleVO
    {
        public int id { get; set; }
        public string module_name { get; set; }
        public string icon { get; set; }
        public DateTime? create_time { get; set; }
        public IEnumerable<SystemPages> systemPages { get; set; }
    }
}
