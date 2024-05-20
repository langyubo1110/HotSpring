using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity.VO
{
    public class RegFileVO
    {
        public int id { get; set; }
        public int? reg_buy_id { get; set; }
        public int? equ_id { get; set; }
        public int type { get; set; }
        public string file_path { get; set; }
        public string file_path_name { get; set; }
        public DateTime create_time { get; set; }
    }
}
