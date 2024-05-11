using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity.VO
{
    public class RegResVO
    {
        public int id { get; set; }
        public int? reg_buy_id { get; set; }
        public string prod_name { get; set; }
        public string fac_name { get; set; }
        public decimal price { get; set; }
        public string batch_number { get; set; }
        public string basic_info { get; set; }
        public string fac_phone { get; set; }
        public string fac_address { get; set; }
        public string prod_img { get; set; }
        public DateTime? create_time { get; set; }
        public int? vote_status { get; set; }
        public int? vote_id { get; set; }
        public string name { get; set; }
        public int Is_show { get; set; }
        public List<ResLessVO> lessList { get; set; }
    }
}
