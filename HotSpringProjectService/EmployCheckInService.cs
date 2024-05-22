using DotNet.Utilities;
using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using HotSpringProjectService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectService
{
    public class EmployCheckInService : IEmployCheckInService
    {
        private readonly IEmployCheckInRepository _db;

        public EmployCheckInService(IEmployCheckInRepository db)
        {
            _db = db;
        }
        public ResMessage Add(int empId,int type)
        {
            EmployCheckIn EmployCheckIn = new EmployCheckIn();
            int flag = _db.Add(EmployCheckIn, empId,type);
            return flag > 0 ? ResMessage.Success() : ResMessage.Fail();
        }

        public IEnumerable<EmployCheckIn> GetList()
        {
            return _db.GetList();
        }

        public IEnumerable<EmployCheckInVO> GetListUnionSql()
        {
            IEnumerable<EmployCheckInVO> list = _db.QueryBySql<EmployCheckInVO>($@"SELECT ci.*, e.name AS emp_name FROM Employ_Check_In AS ci JOIN Employ_Emp AS e ON ci.emp_id = e.id WHERE ci.check_event = 1;");
            return list;
        }

        //获取出勤天数
        public double GetWorkRate(int id)
        {
            //每月1号调度执行，这里-1小时拿到上月的天数
            DateTime dtNow = DateTime.Now.AddHours(-1);
            //获取上月天数
            int month = dtNow.Month;
            int year = dtNow.Year;
            decimal days = DateTime.DaysInMonth(year, month);
            // 获取上个月的第一天和最后一天
            DateTime firstDayOfLastMonth = new DateTime(dtNow.Year, dtNow.Month - 1, 1);
            DateTime lastDayOfLastMonth = firstDayOfLastMonth.AddMonths(1).AddDays(-1);
            //linq 时间段筛选
            //上月第一天00:00:00到本月第一天00:00:00时间内该员工的出勤天数
            decimal workday = _db.GetList().Where(x=>x.create_time> firstDayOfLastMonth && x.create_time< lastDayOfLastMonth && x.emp_Id==id).Count();
            double rate = Math.Round(Convert.ToDouble(workday) / Convert.ToDouble(days),2);
            return rate;
        }

        public ResMessage Verify(int empId)
        {
            return _db.Verify(empId) == true ? ResMessage.Success() : ResMessage.Fail();

        }
    }
}
