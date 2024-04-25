using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    public class SystemPageCorrespondence
    {
        [Key]
        public int id { get; set; }
        public int module_id { get; set; }
        public int role_id { get; set; }
    }
}
