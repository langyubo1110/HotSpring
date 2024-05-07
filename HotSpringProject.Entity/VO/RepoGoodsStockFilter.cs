using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity.VO
{
    public class RepoGoodsStockFilter
    {
        //商品名称
        public string goods_name { get; set; }
        //库存数量
        public int? goods_number { get; set; }
    }
}
