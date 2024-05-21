using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity.VO
{
    public class EquUpkeepTaskFilter
    {
        //过滤条件
        public int page { get; set; }
        public int limit { get; set; }
        public DateTime time { get; set; }
    }
}
