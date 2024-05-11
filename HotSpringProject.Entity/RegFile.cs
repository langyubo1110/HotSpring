using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    [Table("Reg_Files")]
    public class RegFile
    {
        public int id { get; set; }
        public int reg_buy_id { get; set; }
        public int type { get; set; }
        public string file_path { get; set; }
        public DateTime create_time { get; set; }
    }
}
