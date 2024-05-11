using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    public class EquipmentTypeVO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string location { get; set; }
        public int status { get; set; }
        public string power { get; set; }
        public int? used_time { get; set; }
        public int equ_type { get; set; }
        public DateTime? create_time { get; set; }
        public DateTime? usedtime { get; set; }
        public string typename { get; set; }
        //设备状态
        public string sname { get; set; }
    }
}
