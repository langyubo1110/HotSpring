using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    [Table("Reg_Buy")]
    public class RegApply
    {
        public int id { get; set; }
        public string equip_name { get; set; }

        public string fac_name { get; set; }
        public decimal price { get; set; }
        public string batch_number { get; set; }
        public string basic_info { get; set; }
        public string buy_reason { get; set; }
        public decimal income { get; set; }
        public int apply_id { get; set; }
        public DateTime create_time { get; set; }
    }
}
