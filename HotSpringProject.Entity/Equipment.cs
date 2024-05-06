using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    [Table("Equ_Equipment")]
    public class Equipment
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public int status { get; set; }
        public string power { get; set; }
        public int? used_time { get; set; }
        public int equ_type { get; set; }
        public DateTime? create_time { get; set; }
    }
}
