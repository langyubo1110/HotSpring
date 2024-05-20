using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity.VO
{
    public class FaultAnalyseVO
    {
        public int id {  get; set; }
        public int fault_app_id {  get; set; }
        public int analyse_id {  get; set; }
        public string contents {  get; set; }
        public int final_scheme {  get; set; }  

    }
}
