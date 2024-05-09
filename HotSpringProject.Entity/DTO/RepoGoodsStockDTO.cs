using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity.DTO
{
    public class RepoGoodsStockDTO
    {
        //商品id
        public int id { get; set; }
        //商品名称
        public string goods_name { get; set; }
        //单价
        public decimal price { get; set; }
        //采购商
        public string buyer { get; set; }
        //采购商电话
        public string buyer_phone { get; set; }
        //出库/入库前的库存数量
        public int start_number { get; set; }
        //出库/入库后的库存数量
        public int end_number { get; set; }
        //接收人id
        public int recipient_id { get; set; }
        //出入库人id
        public int outin_person_id { get; set; }
        //出入库人
        public string name { get; set; }
        //出入库数量
        public int oi_number { get; set; }
        //出入库类型
        //0出1入
        public int type { get; set; }
        //商品类型
        //0食物1备件2生活用品
        public int goods_type { get; set; }
        //阈值
        public string threshold { get; set; }
        //厂家
        public string factory { get; set; }
        //图片
        public string picture { get; set; }
        //库存数量
        public int goods_number { get; set; }
        //出入库记录创建时间
        public DateTime create_time { get; set; }
    }
}
