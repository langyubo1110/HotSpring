using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity.VO
{
    public class EquUpkeepPlanFilter
    {
        //过滤条件
        public int page { get; set; }
        public int limit { get; set; }
        public string name { get; set; }
    }
}
