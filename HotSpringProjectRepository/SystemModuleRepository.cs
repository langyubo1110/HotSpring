using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository
{
    public class SystemModuleRepository:ISystemModuleRepository
    {
        private readonly HotSpringDbContext _db;
        public SystemModuleRepository(HotSpringDbContext hotSpringDbContext)
        {
            _db = hotSpringDbContext;
        }


        public List<SystemModule> GetList()
        {
            return _db.SystemModules.ToList();
        }

    }
}
