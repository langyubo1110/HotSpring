using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using System.Xml.Linq;

namespace HotSpringProjectRepository
{
    public class EmployEmpRepository : IEmployEmpRepository
    {
        private readonly HotSpringDbContext _db;

        public EmployEmpRepository(HotSpringDbContext hotSpringDbContext)
        {
            _db = hotSpringDbContext;
        }
        public int Add(EmployEmp employemp)
        {
            if (employemp != null)
                employemp.onboarding_time = DateTime.Now;
            employemp.create_time = DateTime.Now;
            employemp.account_status = 1;
            employemp.last_log_time = DateTime.Now;
            _db.Entry(employemp).State = EntityState.Added;
            int flag = _db.SaveChanges();
            return flag;
        }

        public bool Delete(int id)
        {
            EmployEmp employEmp = _db.EmployEmps.Find(id);
            if (employEmp != null)
                _db.Entry(employEmp).State = EntityState.Deleted;
            int flag = _db.SaveChanges();
            return flag > 0 ? true : false;

        }

        public IEnumerable<EmployEmp> GetList()
        {
                return _db.EmployEmps;
        }
        //public IEnumerable<EmployEmp> GetListByID(int id)
        //{
        //    return _db.EmployEmps.Where(e => e.role_id == id).ToList();
        //}

        public EmployEmp GetModel(int id)
        {
            return _db.EmployEmps.Find(id);
        }

        public bool Update(EmployEmp employemp)
        {
            if (employemp != null)

            _db.Entry(employemp).State = EntityState.Modified;
            int flag = _db.SaveChanges();
            return flag > 0 ? true : false;

        }


        public bool Varfy(string number, string password)
        {

            var user = _db.EmployEmps.FirstOrDefault(u => u.job_number == number && u.password == password);
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
