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
        public string spare_parts { get; set; }
        public DateTime create_time { get; set; }
    }
}
