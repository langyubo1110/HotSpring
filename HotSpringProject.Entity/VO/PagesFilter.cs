using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    public class PagesFilter
    {
        public int id { get; set; }
        public string page_address { get; set; }
        public string page_name { get; set; }
        public int module_id { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
    }
}
