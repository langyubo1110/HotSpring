using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    public class RepairVO
    {
        public int id { get; set; }
        public int reporter_id { get; set; }
        public string confirmer { get; set; }
        public string location { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public string work_context { get; set; }
        public DateTime create_time { get; set; }
        public string name { get; set; }
    }
}
