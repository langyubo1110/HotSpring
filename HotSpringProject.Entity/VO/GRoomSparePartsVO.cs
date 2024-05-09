using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity.VO
{
    public class GRoomSparePartsVO
    {
        //商品id
        public int id { get; set; }
        //维修任务id
        public int repair_id { get; set; }
        //备件id
        public int spare_parts_id { get; set; }
        //备件使用数量
        public int used_number { get; set; }
        //创建时间
        public DateTime create_time { get; set; }
        //剩余数量
        public int oi_number { get; set; }
        //库存数量
        public int goods_number { get; set; }

    }
}
