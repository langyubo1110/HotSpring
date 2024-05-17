using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace HotSpringProject.Entity
{
    [Table("Rep_Fault_Analyse")]
    public class FaultAnalyse
    {
        [Key]
        public int id { get; set; } 
        public int  fault_app_id {  get; set; }
        public int analyse_id {  get; set; }    
        public string contents {  get; set; }
        public int final_scheme {  get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime create_time {  get; set; }
        public string auditor {  get; set; }

    }
}
