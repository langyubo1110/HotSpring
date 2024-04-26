using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    [Table("System_Module")]
    public class SystemModule
    {
        public int id {  get; set; }
        public string module_name { get; set; }
        public string icon { get; set;}
        public DateTime? create_time { get; set; }
    }
}
