using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    public class Repo_Goods_Stock
    {
        public  int id { get; set; }
        public string goods_name { get; set; }
        public int goods_number { get; set; }
        public string threshold { get; set; }
        public string factory { get; set; }
        public string picture { get; set; }
        public DateTime create_time { get; set; }

    }
}
