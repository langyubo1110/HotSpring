using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<EmployEmp> GetList()
        {
            return _db.EmployEmps.ToList();
        }

        public EmployEmp GetModel(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(EmployEmp employemp)
        {
            throw new NotImplementedException();
        }
    }
}
