using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity.VO
{
    public class FaultAppVO
    {
        public int id { get; set; }
        public int equip_id { get; set; }
        public string fault_report { get; set; }
        public string fault_describe { get; set; }
        public DateTime fault_time { get; set; }
        public DateTime create_time { get; set; }
        public int count { get; set; }
        public int auditcount {  get; set; }    
        public int show {  get; set; }
        public string name { get; set; }
        public int end_count {  get; set; }//最终解决方案数量
        public int record_count {  get; set; }  
    }
}
