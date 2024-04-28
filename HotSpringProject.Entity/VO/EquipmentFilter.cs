using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    public class EquipmentFilter
    {
        //表中搜索条件
        public int?id { get; set; }
        public string name { get; set; }
        public int? used_time { get; set; }
        public string equ_type { get; set; }

        //过滤条件
        public int page { get; set; }
        public int limit { get; set; }
    }
}
