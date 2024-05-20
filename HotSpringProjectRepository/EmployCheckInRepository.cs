using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository
{
    public class EmployCheckInRepository : IEmployCheckInRepository
    {
        private readonly HotSpringDbContext _db;

        public EmployCheckInRepository(HotSpringDbContext hotSpringDbContext)
        {
            _db = hotSpringDbContext;
        }
        public int Add(EmployCheckIn EmployCheckIn, int empId, int type)
        {
            if (EmployCheckIn != null)
            EmployCheckIn.create_time = DateTime.Now;
            EmployCheckIn.check_event = 1;
            EmployCheckIn.work_type = type;
            EmployCheckIn.emp_Id = empId;
            _db.Entry(EmployCheckIn).State = EntityState.Added;
            int flag = _db.SaveChanges();
            return flag;
        }

        public IEnumerable<EmployCheckIn> GetList()
        {
            return _db.EmployCheckIn;
        }
        public IEnumerable<T> QueryBySql<T>(string sql)
        {
            return _db.Database.SqlQuery<T>(sql);
        }

        public bool Verify(int empId)
        {
            DateTime currentDate = DateTime.Today;

            // 将当前日期转换为数据库中存储时间戳的格式
            DateTime currentDateStartOfDay = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day);

            // 获取明天的日期的零时零分零秒
            DateTime nextDayStart = currentDateStartOfDay.AddDays(1);

            // 检查数据库中是否存在与 empId 和当前日期相对应的记录
            bool alreadySignedIn = _db.EmployCheckIn.Any(s => s.emp_Id == empId && s.create_time >= currentDateStartOfDay && s.create_time < nextDayStart);

            return alreadySignedIn;
        }
    }
}
