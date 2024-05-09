using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    [Table("Repo_Buy")]
    public class RepoBuy
    {
        [Key]
        public int id { get; set; }
        public int goods_id { get; set; }
        public int goods_number { get; set; }
        public decimal price { get; set; }
        public string buyer { get; set; }
        public string buyer_phone { get; set; }
        public DateTime create_time { get; set; }
    }
}
