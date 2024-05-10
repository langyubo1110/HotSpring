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
            return _db.EmployCheckIns;
        }
        public IEnumerable<T> QueryBySql<T>(string sql)
        {
            return _db.Database.SqlQuery<T>(sql);
        }
    }
}
