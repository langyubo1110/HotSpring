using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    [Table("Rep_Repair_Task_Report")]
    public class RepaieTaskReport
    {
        [Key]
       public int id {  get; set; }
       public int reporter_id {  get; set; }
       public DateTime start_time{ get; set; }
       public DateTime end_time { get; set;}
       public string confirmer {  get; set; }
       public string location { get; set;}
        public string work_context {  get; set; }
        public DateTime create_time {  get; set; }
       
    }
}
