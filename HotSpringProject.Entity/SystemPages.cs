using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    [Table("System_Pages")]
    public class SystemPages
    {
        [Key]
        public int id { get; set; }
        public string page_address { get; set; }
        public string page_name { get; set;}
        public string icon { get; set;}
        public int module_id { get; set;}
        public DateTime? create_time { get; set; }
    }
}
