using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity
{
    [Table("Reg_Research_Vote_Record")]
    public class RegVote
    {
        public int id { get; set; }
        public int reg_buy_id { get; set; }
        public int equip_research_id { get; set; }
        public int vote_id { get; set; }
        public int? vote_status { get; set; }
       
        public DateTime create_time { get; set; }
    }
}
