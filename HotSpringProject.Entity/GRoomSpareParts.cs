using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    [Table("GRoom_Spare_Parts")]
    public class GRoomSpareParts
    {
        [Key]
        public int id { get; set; }
        public int repair_id { get; set; }
        public int spare_parts_id { get; set; }
        public int used_number { get; set; }
        public DateTime create_time { get; set; }
    }
}
