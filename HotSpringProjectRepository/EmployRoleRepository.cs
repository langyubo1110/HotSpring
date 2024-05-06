using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public EmployRole GetModelById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmployRole> GetList()
        {
            return _db.EmployRole;
        }

        public bool Update(EmployRole employRole)
        {
            throw new NotImplementedException();
        }
    }
}
