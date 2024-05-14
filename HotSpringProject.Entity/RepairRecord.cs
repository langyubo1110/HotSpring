using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    [Table("Rep_Repair_Record")]
    public class RepairRecord
    {
        [Key]
        public int Id { get; set; }
        public int fault_app_id { get; set; }
        public string repair_spend { get; set; }
        public string repair_people { get; set; }
        public string repair_phone { get; set; }
        public string create_time { get; set; }

    }
}
