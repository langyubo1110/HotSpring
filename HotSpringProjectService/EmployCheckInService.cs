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
        public decimal GetWorkRate(int id)
        {
            //每月1号调度执行，这里-1小时拿到上月的天数
            DateTime dtNow = DateTime.Now.AddHours(-1);
            //获取上月天数
            int days = DateTime.DaysInMonth(dtNow.Year, dtNow.Month);
            //linq 时间段筛选
            //上月第一天00:00:00到本月第一天00:00:00时间内该员工的出勤天数
            int workday = _db.GetList().Where(x=>x.create_time<DateTime.UtcNow.AddMonths(-1)&&x.create_time<DateTime.UtcNow&&x.emp_Id==id).Count();
            decimal rate = workday/days;
            return rate;
        }

        public ResMessage Verify(int empId)
        {
            return _db.Verify(empId) == true ? ResMessage.Success() : ResMessage.Fail();

        }
    }
}
