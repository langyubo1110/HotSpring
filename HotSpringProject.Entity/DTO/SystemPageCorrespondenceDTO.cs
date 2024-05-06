using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    public class SystemPageCorrespondenceDTO
    {
        public int id { get; set; }
        public int pages_id { get; set; }
        public int role_id { get; set; }
        public string page_name { get; set; }
        public string page_address { get; set; }
        public string module_name { get; set; }
    }
}
