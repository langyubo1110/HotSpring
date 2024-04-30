using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    [Table("Equ_Type")]
    public class EquipType
    {
            [Key]
            public int id { get; set; }
            public string type_name { get; set; }
            public DateTime? create_time { get; set; }
        }
}
