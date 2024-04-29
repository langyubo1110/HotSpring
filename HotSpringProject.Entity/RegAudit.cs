using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    [Table("Reg_Audit")]
    public class RegAudit
    {
        [Key]
        //
        public int id { get; set; }
        public int reg_equip_reaearch_id { get; set; }
        
        public int recheck_id { get; set; }
        public int recheck_opin { get; set; }
        public DateTime create_time { get; set; }
    }
}
