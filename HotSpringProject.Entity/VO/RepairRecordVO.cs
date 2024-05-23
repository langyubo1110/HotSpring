using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity.VO
{
    public class RepairRecordVO
    {
        public int Id { get; set; }
        public int fault_app_id { get; set; }
        public string repair_spend { get; set; }
        public int repair_people_id { get; set; }
        public string repair_phone { get; set; }
        public DateTime create_time { get; set; }
        public string repair_people {  get; set; }
    }
}
