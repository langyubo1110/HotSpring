using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    public class UpKeepTasksVO
    {
        public int id {  get; set; }
        public string name { get; set; }
        public string task_name {  get; set; }
        public string task_info { get; set; }
        public string QRimg { get; set; }
        public DateTime? upkeep_time { get; set; }
    }
}
