using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    [Table("Reg_Equip_Research")]
    public class RegEquipRes
    {
        [Key]
        public int id { get; set; }
        public int? reg_buy_id { get; set; }
        public string prod_name { get; set; }
        public string fac_name { get; set; }
        public decimal price{ get; set;}
        public string batch_number { get; set; }
        public string basic_info { get; set; }
        public string fac_phone { get; set; }
        public string fac_address { get; set; }
        public string prod_img { get; set; }
        public DateTime? create_time { get; set; }
        public int is_buy { get; set; }
    }
}
