using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace HotSpringProjectRepository
{
    public class EmployRoleRepository : IEmployRoleRepository
    {
        private readonly HotSpringDbContext _db;

        public EmployRoleRepository(HotSpringDbContext hotSpringDbContext)
        { 
            _db=hotSpringDbContext;
        }
        public int Add(EmployRole employRole)
        {
            int flag;
            if (employRole != null)

                employRole.create_time= DateTime.Now;
                _db.Entry(employRole).State = EntityState.Added;
                flag=_db.SaveChanges();
                return flag;
        }

        public bool Delete(int id)
        {
           EmployRole role=_db.EmployRole.Find(id);
            if (role != null)

            _db.Entry(role).State = EntityState.Deleted;
            int flag=_db.SaveChanges();
            return flag>0?true:false;
        }

        public EmployRole GetModel(int id)
        {
            return _db.EmployRole.Find(id);
        }

        public IEnumerable<EmployRole> GetList()
        {
            return _db.EmployRole;
        }

        public bool Update(EmployRole employRole)
        {
            if(employRole != null)

            _db.Entry(employRole).State= EntityState.Modified;
            int flag = _db.SaveChanges();
            return flag > 0 ? true : false;
        }
    }
}
