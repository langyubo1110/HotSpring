using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository
{
    public class EmployAllsalaryRepository:IEmployAllsalaryRepository
    {
        private readonly HotSpringDbContext _Db;

        public EmployAllsalaryRepository(HotSpringDbContext hotSpringDbContext)
        {
            _Db= hotSpringDbContext;
        }

        public bool Add(EmployAllsalary employAllsalary)
        {
            _Db.Entry(employAllsalary).State = System.Data.Entity.EntityState.Added;
            int flag = _Db.SaveChanges();
            return flag > 0 ? true : false;
        }

        public int Delete(int id)
        {
            EmployAllsalary employAllsalary = _Db.EmployAllsalary.Find(id);
            _Db.Entry(employAllsalary).State = System.Data.Entity.EntityState.Deleted;
            int flag = _Db.SaveChanges();
            return flag;
        }

        public IEnumerable<EmployAllsalary> GetList()
        {
            return _Db.EmployAllsalary;
        }

        public EmployAllsalary GetModel(int id)
        {
            EmployAllsalary employAllsalary = _Db.EmployAllsalary.Find(id);
            return employAllsalary;
        }

        public bool Update(EmployAllsalary employAllsalary)
        {
            _Db.Entry(employAllsalary).State = System.Data.Entity.EntityState.Modified;
            int flag = _Db.SaveChanges();
            return flag > 0 ? true : false;
        }
        public IEnumerable<T> QueryBySql<T>(string sql)
        {
            return _Db.Database.SqlQuery<T>(sql);
        }
    }
}
