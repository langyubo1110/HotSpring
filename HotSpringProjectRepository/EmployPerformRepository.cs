using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository
{
    public class EmployPerformRepository:IEmployPerformRepository
    {
        private readonly HotSpringDbContext _Db;

        public EmployPerformRepository(HotSpringDbContext hotSpringDbContext)
        { 
            _Db= hotSpringDbContext;
        }

        public bool Add(EmployPerform employPerform)
        {
            _Db.Entry(employPerform).State = System.Data.Entity.EntityState.Added;
            int flag = _Db.SaveChanges();
            return flag>0?true:false;
        }

        public int Delete(int id)
        {
            EmployPerform employPerform = _Db.EmployPerform.Find(id);
            _Db.Entry(employPerform).State = System.Data.Entity.EntityState.Deleted;
            int flag = _Db.SaveChanges();
            return flag;
        }
        public IEnumerable<EmployPerform> GetList()
        {
            return _Db.EmployPerform;
        }

        public EmployPerform GetModel(int id)
        {
            EmployPerform employPerform = _Db.EmployPerform.Find(id);
            return employPerform;
        }

        public bool Update(EmployPerform employPerform)
        {
            _Db.Entry(employPerform).State=System.Data.Entity.EntityState.Modified;
            int flag = _Db.SaveChanges();
            return flag>0?true:false;
        }
    }
}
