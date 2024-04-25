using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    internal class Reg_Audit
    {
        [Key]
        //
        public int id { get; set; }
        public int recheck_id { get; set; }
        public int recheck_opin { get; set; }
        public DateTime create_time { get; set; }
    }
}
