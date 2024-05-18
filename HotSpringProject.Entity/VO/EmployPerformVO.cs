using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProject.Entity.VO
{
    public class EmployPerformVO
    {
        //绩效id
        public int id { get; set; }
        //维修任务id
        public int repair_id { get; set; }

        public int emp_id { get; set; }
        //提成金额
        public decimal repair_up_money { get; set; }
        //创建时间
        public DateTime create_time { get; set; }
        //开始时间
        public DateTime start_time { get; set; }
        //结束时间
        public DateTime end_time { get; set; }
        //确认人
        public string confirmer { get; set; }
    }
}
