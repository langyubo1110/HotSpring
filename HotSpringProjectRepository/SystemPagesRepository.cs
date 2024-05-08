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
    public class SystemPagesRepository:ISystemPagesRepository
    {
        private readonly HotSpringDbContext _db;

        public SystemPagesRepository(HotSpringDbContext hotSpringDbContext)
        {
            _db = hotSpringDbContext;
        }
        public int Add(SystemPages systemPages)
        {
            if (systemPages != null)
            systemPages.create_time = DateTime.Now;
            _db.Entry(systemPages).State = EntityState.Added;
            int flag = _db.SaveChanges();
            return flag;
        }

        public bool Delete(int id)
        {
            SystemPages systemPages = _db.SystemPages.Find(id);
            if (systemPages != null)
                _db.Entry(systemPages).State = EntityState.Deleted;
            int flag = _db.SaveChanges();
            return flag > 0 ? true : false;

        }

        public IEnumerable<SystemPages> GetList()
        {
            return _db.SystemPages;
        }

        public SystemPages GetModel(int id)
        {
            return _db.SystemPages.Find(id);
        }

        public bool Update(SystemPages systemPages)
        {
            if (systemPages != null)
            systemPages.create_time= DateTime.Now;
            _db.Entry(systemPages).State = EntityState.Modified;
            int flag = _db.SaveChanges();
            return flag > 0 ? true : false;
        }
        public IEnumerable<(int Id, string Name)> GetModuleList()
        {
            return _db.SystemModules.Select(m => new { m.id, m.module_name }).ToList()
                                     .Select(x => (x.id, x.module_name));
        }

        public List<SystemPages> GetPagesByModuleId(int moduleId)
        {
            return _db.systemPages.Where(p => p.module_id == moduleId).ToList();
        }
    }
}
