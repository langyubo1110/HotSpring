using HotSpringProject.Entity;
using HotSpringProjectRepository.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpringProjectRepository
{
    public class EmployMessageRepository : IEmployMessageRepository
    {
        private readonly HotSpringDbContext _db;

        public EmployMessageRepository(HotSpringDbContext hotSpringDbContext)
        {
            _db = hotSpringDbContext;
        }
        public List<EmployMessage> GetList()
        {
            return _db.EmployMessage.ToList();
        }

        
    }
}
