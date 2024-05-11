using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    [Table("Rep_Fault_App")]
    public class FaultApp
    {
        [Key]
       public  int id {  get; set; }
       public int equip_id { get; set; }
        public string fault_report { get; set; }
        public string fault_describe { get; set;}
        public DateTime fault_time {  get; set; }
        public DateTime create_time {  get; set; }
    }
}
