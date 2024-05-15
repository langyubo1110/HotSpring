using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity.VO
{
    public class RepoOutInRecordFilter
    {
        //商品ID
        public int? id { get; set; }
        //商品名称
        public string goods_name { get; set; }
        //出库/入库人姓名
        public string name { get; set; }
        //出库人id（员工id）
        public int? outin_person_id { get; set; }
    }
}
